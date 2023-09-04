using java.lang;
using java.util;
using javax.microedition.lcdui;
using java.io;
using AE2CSharp.Enums;

namespace aeii
{
    public class C_Unit : F_Sprite
    {
        private const byte MaxLevel = 9;

        public static byte m_cpuUnitSpeed = 12; //be careful here, 16,18 causes unit move lock 
        public static byte m_defaultSpeed = 6;
        public static byte m_speed = m_defaultSpeed;
        public static I_Game sGame;
        public String unitName;
        public byte level;
        public short experience;
        public byte[][] charsData;
        public Vector unitMovePathPositions;
        public short movePathPosIndex;
        public long unitFrameStartTime;
        public sbyte unitTypeId;
        public sbyte playerId;
        public short positionX;
        public short positionY;
        public int m_startPosX;
        public int m_startPosY;
        public int unitAttackMin;
        public int unitAttackMax;
        public int unitDefence;
        public byte unitHealthMb;
        public byte m_state;
        public byte status;
        public short movementStatusBonus;
        public short defenceStatusBonus;
        public short attackStatusBonus;
        public bool isUnitShaking;
        public bool m_shakeDirection = true;
        public int shakingMaxTime;
        public long shakingStartTime;
        public byte m_tombMaxTurns;
        public sbyte someStatusPlayerId;
        public int m_smokeMoveStepIndex;
        public C_Unit followerUnitMb;
        public byte kingIndex = 0;
        public int unitId;
        public short cost;
        public int m_aiPriority;
        public static byte[] unitsMoveRanges = new byte[12];
        public static byte[][] unitsAttackValues = new byte[12][];
        public static byte[] unitsDefenceValues = new byte[12];
        public static byte[] maxUnitRanges = new byte[12];
        public static byte[] minUnitRanges = new byte[12];
        public static byte[][][] unitsChars = new byte[12][][];
        public static short[] unitsCosts = new short[12];
        public static short[] unitsProperties = new short[12];

        static C_Unit()
        {
            for (int i = 0; i < 12; i++)
                unitsAttackValues[i] = new byte[2];
        }

        private C_Unit(sbyte typeId, sbyte playerId, int posX, int posY, bool showUnit)
            : base(sGame.getSomePosUnitSprite(playerId, typeId))
        {
            this.unitTypeId = typeId;
            this.m_state = UnitState.Default;
            this.positionX = ((short)posX);
            this.positionY = ((short)posY);
            setSpritePosition(posX * 24, posY * 24);
            setUnitLevel((byte)0);
            if (showUnit)
            {
                sGame.mapUnitsSprites.addElement(this);
            }
        }

        public void setUnitLevel(byte lvl)
        {
            this.level = (lvl);
            int lvlBonus = lvl * 2;
            this.unitAttackMin = (unitsAttackValues[this.unitTypeId][0] + lvlBonus);
            this.unitAttackMax = (unitsAttackValues[this.unitTypeId][1] + lvlBonus);
            this.unitDefence = (unitsDefenceValues[this.unitTypeId] + lvlBonus);
            if (this.unitTypeId != UnitType.Commander)
            {
                int j = this.level / 2;
                if (j > 3)
                {
                    j = 3;
                }
                this.unitName = A_MenuBase.getLangString(139 + this.unitTypeId * 4 + j);
            }
        }

        public void shakeUnit(int val)
        {
            this.isUnitShaking = true;
            this.shakingStartTime = sGame.time;
            this.shakingMaxTime = val;
        }

        public static C_Unit createUnitOnMap(sbyte type, sbyte playerId, int pX, int pY)
        {
            return createUnit(type, playerId, pX, pY, true);
        }

        public static C_Unit createUnit(sbyte type, sbyte playerId,
                int pX, int pY, bool showUnit)
        {
            C_Unit unit = new C_Unit(type,
                    sGame.playersIndexes[playerId], pX, pY,
                    showUnit);
            unit.unitTypeId = type;
            unit.playerId = playerId;
            unit.unitHealthMb = 100;
            unit.charsData = unitsChars[type];
            unit.cost = unitsCosts[type];
            if (type == UnitType.Commander)
            {
                unit.setKingName(sGame.playersIndexes[playerId] - 1);
                unit.unitId = sGame.playerUnitsCount[playerId];
                sGame.playerKingsMb[playerId][unit.unitId] = unit;
                sGame.playerUnitsCount[playerId] += 1;
            }

            return unit;
        }

        public void removeFromMap()
        {
            sGame.mapUnitsSprites.removeElement(this);
        }

        public void setKingName(int index)
        {
            this.kingIndex = ((byte)index);
            this.unitName = A_MenuBase.getLangString(index + 93);
        }

        public int getUnitExtraAttack(C_Unit unit)
        {
            return getUnitExtraAttack(unit, this.positionX, this.positionY);
        }

        public int getUnitExtraAttack(C_Unit unit, int inX, int inY)
        {
            int extraAtt = this.defenceStatusBonus;
            if (unit != null)
            {
                if ((hasProperty(UnitProperty.Archer))
                        && (unit.hasProperty(UnitProperty.Flying)))
                { //archer & dragon
                    extraAtt += 15;
                }

                if ((this.unitTypeId == UnitType.Wisp) && (unit.unitTypeId == UnitType.Skeleton))
                { //wisp & skeleton
                    extraAtt += 15;
                }
            }
            
            if ((hasProperty(UnitProperty.Aquatic)) && (sGame.getTileType(inX, inY) == TileType.Water))
            { //water
                extraAtt += 10;
            }

            if (sGame.mapTilesIds[inX][inY] == TileType.SaethCitadel)
            { //saeth
                extraAtt += 25;
            }

            return extraAtt;
        }

        public int getUnitResistance(C_Unit unit)
        {
            return getUnitResistance(unit, this.positionX, this.positionY);
        }

        public int getUnitResistance(C_Unit unit, int inX, int inY)
        {
            int tType = sGame.getTileType(inX, inY);
            int resist = this.attackStatusBonus + I_Game.tilesDefences[tType];
            if ((hasProperty(UnitProperty.Aquatic)) && (tType == TileType.Water))
            { //water
                resist += 15;
            }

            if (sGame.mapTilesIds[inX][inY] == TileType.SaethCitadel)
            { //saeth citadel
                resist += 15;
            }

            return resist;
        }

        public int getUnitAttackDamage(C_Unit oUnit)
        {
            int uDamage = E_MainCanvas.getRandomWithin(this.unitAttackMin, this.unitAttackMax)
                    + getUnitExtraAttack(oUnit);
            int uDefence = oUnit.unitDefence + oUnit.getUnitResistance(this);
            int damage = (uDamage - uDefence) * this.unitHealthMb / 100;
            if (damage < 0)
            {
                damage = 0;
            }
            else if (damage > oUnit.unitHealthMb)
            {
                damage = oUnit.unitHealthMb;
            }

            oUnit.unitHealthMb -= (byte)damage;
            this.experience += (byte)(oUnit.getExpKoef() * damage);
            return damage;
        }

        public int getExpKoef()
        {
            return this.unitAttackMin + this.unitAttackMax + this.unitDefence;
        }

        public int getLevelExpMax()
        {
            return getExpKoef() * 100 * 2 / 3;
        }

        public bool gotNewLevel()
        {
            if (this.level < MaxLevel)
            {
                int exp = getLevelExpMax();
                if (this.experience >= exp)
                {
                    this.experience -= (byte)exp;
                    setUnitLevel((byte)(this.level + 1));
                    return true;
                }
            }

            return false;
        }

        public bool isNearOtherUnit(C_Unit unit, int inX, int inY)
        {
            return (this.m_state != UnitState.Removed)
                    && (this.unitHealthMb > 0)
                    && (Math.abs(this.positionX - inX)
                            + Math.abs(this.positionY - inY) == 1)
                    && (minUnitRanges[this.unitTypeId] == 1);
        }

        public void addStatusEffect(byte statusEffect)
        {
            this.status = ((byte)(this.status | statusEffect));
            calcStatusEffect();
            if (statusEffect == StatusEffect.Poison)
            {
                this.someStatusPlayerId = sGame.playerId;
            }
        }

        public void removeStatusEffect(byte statusEffect)
        {
            this.status = ((byte)(this.status & (statusEffect ^ 0xFFFFFFFF)));
            calcStatusEffect();
        }

        public void calcStatusEffect()
        {
            this.movementStatusBonus = 0;
            this.defenceStatusBonus = 0;
            this.attackStatusBonus = 0;
            if ((this.status & StatusEffect.Poison) != 0)
            { // poison
                this.defenceStatusBonus = ((short)(this.defenceStatusBonus - 10));
                this.attackStatusBonus = ((short)(this.attackStatusBonus - 10));
            }

            if ((this.status & StatusEffect.WispAura) != 0)
            { // wisp
                this.defenceStatusBonus = ((short)(this.defenceStatusBonus + 10));
            }
        }

        public void setUnitPosition(int pX, int pY)
        {
            this.positionX = ((short)pX);
            this.positionY = ((short)pY);
            this.posXPixel = ((short)(pX * 24));
            this.posYPixel = ((short)(pY * 24));
        }

        public int getAliveCharactersCount()
        {
            int i = 100 / this.charsData.Length;
            int j = this.unitHealthMb / i;
            if ((this.unitHealthMb != 100) && (this.unitHealthMb % i > 0))
            {
                j++;
            }
            return j;
        }

        /// <summary>
        /// Some kind of power priority for AI.
        /// </summary>
        public int getSomePropSum(int inX, int inY, C_Unit unit)
        {
            var sum =
                this.unitAttackMin +
                this.unitAttackMax +
                this.unitDefence +
                getUnitExtraAttack(unit, inX, inY) +
                getUnitResistance(unit, inX, inY);
            return (sum) * this.unitHealthMb / 100;
        }

        public void fillAttackRangeData(byte[][] mapdata, int inX,
                int inY)
        {
            int minRange = minUnitRanges[this.unitTypeId];
            int maxRange = maxUnitRanges[this.unitTypeId];
            int minX = inX - maxRange;
            if (minX < 0)
            {
                minX = 0;
            }
            int minY = inY - maxRange;
            if (minY < 0)
            {
                minY = 0;
            }
            int maxX = inX + maxRange;
            if (maxX >= sGame.mapWidth)
            {
                maxX = sGame.mapWidth - 1;
            }
            int maxY = inY + maxRange;
            if (maxY >= sGame.mapHeight)
            {
                maxY = sGame.mapHeight - 1;
            }
            for (int xx = minX; xx <= maxX; xx++)
            {
                for (int yy = minY; yy <= maxY; yy++)
                {
                    int dist = Math.abs(xx - inX) + Math.abs(yy - inY);
                    if ((dist >= minRange) && (dist <= maxRange) && (mapdata[xx][yy] <= 0))
                    {
                        mapdata[xx][yy] = 127;
                    }
                }
            }
        }

        public void showWhereUnitCanAttack(byte[][] mapRangeData)
        {
            if (hasProperty(UnitProperty.CatapultRange))
            { //catapult
                fillAttackRangeData(mapRangeData, this.positionX, this.positionY);
                return;
            }

            fillWhereUnitCanMove(mapRangeData);
            for (int i = 0; i < sGame.mapWidth; i++)
            {
                for (int j = 0; j < sGame.mapHeight; j++)
                {
                    if ((mapRangeData[i][j] > 0) && (mapRangeData[i][j] != 127))
                    {
                        fillAttackRangeData(mapRangeData, i, j);
                    }
                }
            }
        }

        public C_Unit[] getActiveUnitsInAttackRange(int inX, int inY,
                byte paramByte)
        {
            return getPositionUnitsInAttackRange(inX, inY, minUnitRanges[this.unitTypeId],
                    maxUnitRanges[this.unitTypeId], paramByte);
        }

        public C_Unit[] getPositionUnitsInAttackRange(int inX, int inY,
                int minRange, int maxRange, byte paramByte)
        {
            Vector localVector = new Vector();
            int minX = inX - maxRange;
            if (minX < 0)
            {
                minX = 0;
            }
            int minY = inY - maxRange;
            if (minY < 0)
            {
                minY = 0;
            }
            int maxX = inX + maxRange;
            if (maxX >= sGame.mapWidth)
            {
                maxX = sGame.mapWidth - 1;
            }
            int maxY = inY + maxRange;
            if (maxY >= sGame.mapHeight)
            {
                maxY = sGame.mapHeight - 1;
            }
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    int dist = Math.abs(x - inX) + Math.abs(y - inY);
                    if ((dist >= minRange) && (dist <= maxRange))
                    {
                        C_Unit aUnit;
                        if (paramByte == 0)
                        {
                            if ((aUnit = sGame.getSomeUnit(x, y,
                                    (byte)0)) != null)
                            {
                                if (sGame.playersTeams[aUnit.playerId] != sGame.playersTeams[this.playerId])
                                {
                                    localVector.addElement(aUnit);
                                }
                            }
                            else if ((this.unitTypeId == UnitType.Catapult)
                                  && (sGame.getTileType(x, y) == TileType.Village)
                                  && (sGame.mapTilesIds[x][y] >= sGame.houseTileIdStartIndex)
                                  && (!sGame.isInSameTeam(x, y,
                                          sGame.playersTeams[this.playerId])))
                            {
                                C_Unit unit2 = createUnit((sbyte)0, (sbyte)0, x, y, false);
                                unit2.unitTypeId = UnitType.None;
                                unit2.m_state = UnitState.Removed;
                                localVector.addElement(unit2);
                            }
                        }
                        else if (paramByte == 1)
                        {
                            if ((aUnit = sGame.getSomeUnit(x, y,
                                    (byte)1)) != null)
                            {
                                localVector.addElement(aUnit);
                            }
                        }
                        else if ((paramByte == 2)
                              && ((aUnit = sGame.getSomeUnit(x, y,
                                      (byte)0)) != null)
                              && (sGame.playersTeams[aUnit.playerId] == sGame.playersTeams[this.playerId]))
                        {
                            localVector.addElement(aUnit);
                        }
                    }
                }
            }
            C_Unit[] units = new C_Unit[localVector.size()];
            localVector.copyInto(units);
            return units;
        }

        public void goToPosition(int inX, int inY,
                bool paramBoolean)
        {
            goToPosition(inX, inY, paramBoolean, false);
        }

        public void goToPosition(int inX, int inY,
                bool paramBoolean1, bool paramBoolean2)
        {
            if (paramBoolean1)
            {
                this.unitMovePathPositions = getUnitMovePathPositions(this.positionX, this.positionY, inX, inY);
            }
            else
            {
                if ((paramBoolean2)
                        && (sGame.getSomeUnit(inX, inY, (byte)0) != null))
                {
                    int i = 0;
                    for (int j = inX - 1; j <= inX + 1; j++)
                    {
                        for (int k = inY - 1; k <= inY + 1; k++)
                        {
                            if (((j == inX) && (k == inY))
                                    || (((j == inX) || (k == inY)) && (sGame
                                            .getSomeUnit(j, k, (byte)0) == null)))
                            {
                                inX = j;
                                inY = k;
                                i = 1;
                                break;
                            }
                        }
                        if (i != 0)
                        {
                            break;
                        }
                    }
                }
                this.unitMovePathPositions = new Vector();
                short[] aPos = { this.positionX, this.positionY };
                this.unitMovePathPositions.addElement(aPos);
                short j34 = this.positionX;
                int distX = Math.abs(inX - this.positionX);
                if (distX > 0)
                {
                    int m = (inX - this.positionX) / distX;
                    for (int n = 0; n < distX; n++)
                    {
                        j34 = (short)(j34 + m);
                        short[] yPos = { j34, this.positionY };
                        this.unitMovePathPositions.addElement(yPos);
                    }
                }
                short m2 = this.positionY;
                int distY = Math.abs(inY - this.positionY);
                if (distY > 0)
                {
                    int nY = (inY - this.positionY) / distY;
                    for (int i1 = 0; i1 < distY; i1++)
                    {
                        m2 = (short)(m2 + nY);
                        short[] mPos = { j34, m2 };
                        this.unitMovePathPositions.addElement(mPos);
                    }
                }
            }
            this.m_startPosX = inX;
            this.m_startPosY = inY;
            this.movePathPosIndex = 1;
            this.m_state = UnitState.Moving;
        }

        public Vector getUnitMovePathPositions(int posX, int posY, int curPx, int curPy)
        {
            Vector list = null;
            short[] somePos = { (short)curPx, (short)curPy };
            if ((posX == curPx) && (posY == curPy))
            {
                list = new Vector();
                list.addElement(somePos);
                return list;
            }
            int j = 0;
            int k = 0;
            int m = 0;
            int n = 0;
            if (curPy > 0)
            {
                j = sGame.someMapData[curPx][(curPy - 1)];
            }
            if (curPy < sGame.mapHeight - 1)
            {
                k = sGame.someMapData[curPx][(curPy + 1)];
            }
            if (curPx > 0)
            {
                m = sGame.someMapData[(curPx - 1)][curPy];
            }
            if (curPx < sGame.mapWidth - 1)
            {
                n = sGame.someMapData[(curPx + 1)][curPy];
            }
            int i;
            if ((i = Math.max(Math.max(j, k), Math.max(m, n))) == j)
            {
                list = getUnitMovePathPositions(posX, posY, curPx, curPy - 1);
            }
            else if (i == k)
            {
                list = getUnitMovePathPositions(posX, posY, curPx, curPy + 1);
            }
            else if (i == m)
            {
                list = getUnitMovePathPositions(posX, posY, curPx - 1, curPy);
            }
            else if (i == n)
            {
                list = getUnitMovePathPositions(posX, posY, curPx + 1, curPy);
            }
            list.addElement(somePos);
            return list;
        }

        public void fillWhereUnitCanMove(byte[][] data)
        {
            fillWhereUnitcanMove(data,
                this.positionX, this.positionY,
                unitsMoveRanges[this.unitTypeId] + this.movementStatusBonus,
                -1, this.unitTypeId,
                this.playerId, false);
        }

        public static bool fillWhereUnitcanMove(byte[][] mdata,
                int inX, int inY, int sTileType, int paramInt4,
                sbyte paramByte1, sbyte paramByte2, bool paramBoolean)
        {
            if (sTileType > mdata[inX][inY])
            {
                mdata[inX][inY] = ((byte)sTileType);
                if ((paramBoolean) && (sGame.getSomeUnit(inX, inY, (byte)0) == null))
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            int i;
            if ((paramInt4 != 1)
                    && ((i = sTileType
                            - getCellMoveEffort(inX, inY - 1, paramByte1,
                                    paramByte2)) >= 0)
                    && (fillWhereUnitcanMove(mdata, inX, inY - 1, i, 2,
                            paramByte1, paramByte2, paramBoolean))
                    && (paramBoolean))
            {
                return true;
            }
            if ((paramInt4 != 2)
                    && ((i = sTileType
                            - getCellMoveEffort(inX, inY + 1, paramByte1,
                                    paramByte2)) >= 0)
                    && (fillWhereUnitcanMove(mdata, inX, inY + 1, i, 1,
                            paramByte1, paramByte2, paramBoolean))
                    && (paramBoolean))
            {
                return true;
            }
            if ((paramInt4 != 4)
                    && ((i = sTileType
                            - getCellMoveEffort(inX - 1, inY, paramByte1,
                                    paramByte2)) >= 0)
                    && (fillWhereUnitcanMove(mdata, inX - 1, inY, i, 8,
                            paramByte1, paramByte2, paramBoolean))
                    && (paramBoolean))
            {
                return true;
            }
            return (paramInt4 != 8)
                    && ((i = sTileType
                            - getCellMoveEffort(inX + 1, inY, paramByte1,
                                    paramByte2)) >= 0)
                    && (fillWhereUnitcanMove(mdata, inX + 1, inY, i, 4,
                            paramByte1, paramByte2, paramBoolean))
                    && (paramBoolean);
        }

        public static int getCellMoveEffort(int inX, int inY, sbyte inUnitType, sbyte inUnitTeam)
        {
            if ((inX >= 0) && (inY >= 0) && (inX < sGame.mapWidth) && (inY < sGame.mapHeight))
            {
                C_Unit unitOnPos = sGame.getSomeUnit(inX, inY, (byte)0);
                if ((unitOnPos != null)
                        && (sGame.playersTeams[unitOnPos.playerId] != sGame.playersTeams[inUnitTeam]))
                {
                    return 1000;
                }

                int tyleType = sGame.getTileType(inX, inY);
                if (inUnitType == UnitType.Crystal)
                { //crystal
                    if (tyleType == TileType.Mountain)
                    { //mountain
                        return 1000;
                    }
                }
                else
                {
                    if (hasUnitProperty(inUnitType, UnitProperty.Flying))
                    { //fly
                        return 1;
                    }

                    if ((hasUnitProperty(inUnitType, UnitProperty.Aquatic)) && (tyleType == TileType.Water))
                    { //water
                        return 1;
                    }
                }

                return I_Game.tilesMovements[tyleType];
            }

            return 10000;
        }

        public void spriteUpdate()
        {
            base.spriteUpdate();
            //@todo
        }

        //@todo mb override
        public void unitUpdate()
        {
            if (this.isUnitShaking)
            {
                if (sGame.time - this.shakingStartTime >= this.shakingMaxTime)
                {
                    this.isUnitShaking = false;
                }
                else
                {
                    this.m_shakeDirection = (!this.m_shakeDirection);
                }
            }
            if (this.m_state == UnitState.Moving) // running
            {
                if (this.movePathPosIndex >= this.unitMovePathPositions.size())
                {
                    this.m_state = UnitState.Default; //stopped
                    this.positionX = ((short)(this.posXPixel / 24));
                    this.positionY = ((short)(this.posYPixel / 24));
                    this.unitMovePathPositions = null;
                    this.movePathPosIndex = 0;
                }
                else
                {
                    if ((this.followerUnitMb != null) && (this.posXPixel % 24 == 0)
                            && (this.posYPixel % 24 == 0))
                    {
                        this.followerUnitMb.goToPosition(this.positionX, this.positionY, false);
                    }
                    short[] curMovePathPos = (short[])this.unitMovePathPositions.elementAt(this.movePathPosIndex);
                    int cPosXPix = curMovePathPos[0] * 24;
                    int xPosYPix = curMovePathPos[1] * 24;
                    F_Sprite somSprite = null;
                    if ((this.followerUnitMb == null)
                            && (++this.m_smokeMoveStepIndex >= 24 / m_speed / 2))
                    {
                        somSprite = sGame.showSpriteOnMap(sGame.bigSmokeSprite,
                                this.posXPixel, this.posYPixel, 0, 0, 1,
                                E_MainCanvas.getRandomWithin(1, 4) * 50);
                        this.m_smokeMoveStepIndex = 0;
                    }
                    if (cPosXPix < this.posXPixel)
                    {
                        this.posXPixel -= m_speed;
                        if (somSprite != null)
                        {
                            somSprite.setSpritePosition(this.posXPixel + this.frameWidth,
                                    this.posYPixel + this.frameHeight
                                            - somSprite.frameHeight);
                        }
                    }
                    else if (cPosXPix > this.posXPixel)
                    {
                        this.posXPixel += m_speed;
                        if (somSprite != null)
                        {
                            somSprite.setSpritePosition(this.posXPixel
                                    - somSprite.frameWidth, this.posYPixel
                                    + this.frameHeight - somSprite.frameHeight);
                        }
                    }
                    else if (xPosYPix < this.posYPixel)
                    {
                        this.posYPixel -= m_speed;
                        if (somSprite != null)
                        {
                            somSprite.setSpritePosition(
                                            this.posXPixel
                                                    + (this.frameWidth - somSprite.frameWidth)
                                                    / 2, this.posYPixel
                                                    + this.frameHeight);
                        }
                    }
                    else if (xPosYPix > this.posYPixel)
                    {
                        this.posYPixel += m_speed;
                        if (somSprite != null)
                        {
                            somSprite.setSpritePosition(
                                            this.posXPixel
                                                    + (this.frameWidth - somSprite.frameWidth)
                                                    / 2, this.posYPixel
                                                    - somSprite.frameHeight);
                        }
                    }
                    if ((this.posXPixel == cPosXPix) && (this.posYPixel == xPosYPix))
                    {
                        this.positionX = curMovePathPos[0];
                        this.positionY = curMovePathPos[1];
                        this.movePathPosIndex = ((short)(this.movePathPosIndex + 1));
                    }
                }
                base.setSpritePosition(this.posXPixel, this.posYPixel);
                nextFrame();
                return;
            }
            if ((this.m_state == UnitState.Default) && (sGame.time - this.unitFrameStartTime >= 200L))
            {
                nextFrame();
                this.unitFrameStartTime = sGame.time;
            }
        }

        public static bool hasUnitProperty(sbyte uType, short prop)
        {
            return (unitsProperties[uType] & prop) != 0;
        }

        public bool hasProperty(short prop)
        {
            return hasUnitProperty(this.unitTypeId, prop);
        }

        public void endMove()
        {
            this.m_state = UnitState.Disabled;
            C_Unit unit1 = sGame.getSomeUnit(this.positionX, this.positionY, (byte)1);
            if (unit1 != null)
            {
                unit1.removeFromMap();
            }

            if (hasProperty(UnitProperty.WispAura))
            { //wisp aura
                C_Unit[] unitsInRange = getPositionUnitsInAttackRange(this.positionX, this.positionY, 1, 2, (byte)2);
                for (int i = 0; i < unitsInRange.Length; i++)
                {
                    unitsInRange[i].addStatusEffect(StatusEffect.WispAura);
                    sGame.showSpriteOnMap(sGame.sparkSprite,
                            unitsInRange[i].posXPixel,
                            unitsInRange[i].posYPixel, 0, 0, 1, 50);
                }
            }

            sGame.unitEndTurnMb = this;
        }

        public static C_Unit[] getAvailableBuyUnits(sbyte index)
        {
            C_Unit[] units = new C_Unit[sGame.playerUnitsCount[index]];
            int uCount = 0;
            for (int j = 0; j < units.Length; j++)
            {
                if ((sGame.playerKingsMb[sGame.playerId][j] != null)
                        && (sGame.playerKingsMb[sGame.playerId][j].m_state == UnitState.Dead)) //if king is dead
                {
                    units[(uCount++)] = sGame.playerKingsMb[sGame.playerId][j]; //add king to list
                }
            }

            C_Unit[] units2 = new C_Unit[sGame.unlockedUnitsTypeMax + 1 + uCount];
            for (int k = 0; k < units2.Length; k = (byte)(k + 1))
            {
                if (k < uCount)
                {
                    units2[k] = units[k];
                }
                else
                {
                    units2[k] = createUnit((sbyte)(k - uCount), index, 0, 0, false);
                }
            }

            return units2;
        }

        //@todo candidate
        public void drawUnitEnabled(Graphics gr, int inX, int inY)
        {
            drawUnit(gr, inX, inY, false);
        }

        public void drawUnit(Graphics gr, int inX, int inY, bool unitDisabled)
        {
            if (this.m_state != UnitState.Removed)
            {
                int shX;
                int shY;
                if (this.isUnitShaking)
                {
                    if (this.m_shakeDirection)
                    {
                        shX = -2;
                    }
                    else
                    {
                        shX = 2;
                    }
                    shY = E_MainCanvas.getRandomInt() % 1;
                    base.onSpritePaint(gr, inX + shX, inY + shY);
                }
                else if ((unitDisabled) || (this.m_state == UnitState.Disabled)) //end turn
                {
                    sGame.playersUnitsSprites[0][this.unitTypeId].onSpritePaint(gr,
                            this.posXPixel + inX, this.posYPixel + inY);
                }
                else
                {
                    base.onSpritePaint(gr, inX, inY);
                }

                if (this.unitTypeId == UnitType.Commander)
                {
                    shX = this.posXPixel + inX;
                    shY = this.posYPixel + inY;
                    if ((unitDisabled) || (this.m_state == UnitState.Disabled)) //end turn
                    {
                        sGame.kingHeadsSprites[1].drawFrameAt(gr, this.kingIndex
                                * 2 + this.currentFrameIndex, shX, shY, 0);
                        return;
                    }
                    sGame.kingHeadsSprites[0].drawFrameAt(gr, this.kingIndex * 2
                            + this.currentFrameIndex, shX, shY, 0);
                }
            }
        }

        public void drawUnitHealth(Graphics gr, int shiftX, int shiftY)
        {
            int hX = this.posXPixel + shiftX;
            int hY = this.posYPixel + shiftY;
            if ((this.m_state != UnitState.Dead) && (this.unitHealthMb < 100))
            {
                E_MainCanvas.drawCharedString(gr, "" + this.unitHealthMb, hX, hY
                        + this.frameHeight - 7, 0);
            }
        }

        public static void loadUnitsProps(I_Game aGame)
        {
            sGame = aGame;
            DataInputStream dis = new DataInputStream(E_MainCanvas.getResourceStream("units.bin"));
            for (int i = 0; i < 12; i++)
            {
                unitsMoveRanges[i] = dis.readByte();
                unitsAttackValues[i][0] = dis.readByte();
                unitsAttackValues[i][1] = dis.readByte();
                unitsDefenceValues[i] = dis.readByte();
                maxUnitRanges[i] = dis.readByte();
                minUnitRanges[i] = dis.readByte();
                unitsCosts[i] = dis.readShort();
                int uCharsCount = dis.readByte();
                unitsChars[i] = new byte[uCharsCount][];
                for (int k = 0; k < uCharsCount; k++)
                {
                    unitsChars[i][k] = new byte[2];
                    unitsChars[i][k][0] = dis.readByte();
                    unitsChars[i][k][1] = dis.readByte();
                }
                int sLength = dis.readByte();
                for (int m = 0; m < sLength; m++)
                {
                    int upIndex = i;
                    short[] uProps = unitsProperties;
                    byte propByte = dis.readByte();
                    uProps[upIndex] = (short)((ushort)uProps[upIndex] | (1 << propByte));
                }
            }
            dis.close();
        }
    }

}