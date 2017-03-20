using java.io;
using java.lang;
using java.csharp;

namespace AE2.Tools.Loaders
{
    public class Map
    {
        public int mapWidth;
        public int mapHeight;
        public byte[][] mapTilesIds;
        private byte[][] someMapData;
        private byte[][] someMData;
        private int mapCastlesCount;
        private byte houseTileIdStartIndex;
        private int mapModeCampIf0;
        public sbyte[] somePlayersData = new sbyte[5];
        public byte[] playersIndexes = new byte[4];
        public byte[] playersTeams = new byte[4];
        private sbyte mapMaxPlayersMb;
        private int[] var_3acb;
        private sbyte[][] housesDataArr;
        private byte[][] var_373b;
        private int mapWidthPixel;
        private int mapHeightPixel;
        private byte mapStartMoney;
        public short[] playersMoney = new short[4];
        private int mapStartUnitCap;
        public byte[] mapPlayersTypes = new byte[4];

        private byte[] tilesDefs;

        public void loadMap(InputStream stream)
        {
            this.houseTileIdStartIndex = 37;
            //
            var dis = new DataInputStream(stream);
            this.mapWidth = dis.readInt();
            this.mapHeight = dis.readInt();
            this.mapTilesIds = JavaArray.New<byte>(this.mapWidth, this.mapHeight);
            this.someMapData = JavaArray.New<byte>(this.mapWidth, this.mapHeight);
            this.someMData = JavaArray.New<byte>(this.mapWidth, this.mapHeight);
            this.mapCastlesCount = 0;
            int countHouses = 0;
            sbyte[][] housesArr = JavaArray.New<sbyte>(30, 3);
            byte[][] mapCastlesPositions = JavaArray.New<byte>(30, 2);
            int m;
            for (short i = 0; i < this.mapWidth; i = (short)(i + 1))
            {
                for (short j = 0; j < this.mapHeight; j = (short)(j + 1))
                {
                    this.mapTilesIds[i][j] = dis.readByte();
                    this.someMapData[i][j] = 0;
                    if ((this.mapTilesIds[i][j] >= this.houseTileIdStartIndex)
                            || (this.mapTilesIds[i][j] == 27))
                    { 
                        // is house
                        m = tileOwnerPlayerIndex(i, j);
                        housesArr[countHouses][0] = ((sbyte)i);
                        housesArr[countHouses][1] = ((sbyte)j);
                        housesArr[countHouses][2] = ((sbyte)m);
                        countHouses++;
                        if (getTileType(i, j) == 9)
                        { 
                            // castle
                            if ((this.mapModeCampIf0 == 1) && (m != 0)
                                    && (this.somePlayersData[m] == -1))
                            {
                                this.playersIndexes[this.mapMaxPlayersMb] = ((byte)m);
                                this.somePlayersData[m] = this.mapMaxPlayersMb;
                                this.mapMaxPlayersMb = (sbyte)(this.mapMaxPlayersMb + 1);
                            }
                            mapCastlesPositions[this.mapCastlesCount][0] = ((byte)i);
                            mapCastlesPositions[this.mapCastlesCount][1] = ((byte)j);
                            this.mapCastlesCount += 1;
                        }
                    }
                }
            }
            this.var_3acb = new int[countHouses];
            this.housesDataArr = new sbyte[countHouses][];
            for (short i = 0; i < countHouses; i = (short)(i + 1))
            {
                this.housesDataArr[i] = housesArr[i];
            }
            this.var_373b = JavaArray.New<byte>(this.mapCastlesCount, 2);
            JavaSystem.arraycopy(mapCastlesPositions, 0, this.var_373b, 0, this.mapCastlesCount);
            this.mapWidthPixel = (this.mapWidth * 24);
            this.mapHeightPixel = (this.mapHeight * 24);

            if (this.mapModeCampIf0 == 1)
            {
                for (short i = 0; i < this.mapMaxPlayersMb; i = (short)(i + 1))
                {
                    this.playersMoney[i] = (byte)this.mapStartMoney;
                }
            }
            else
            {
                this.mapMaxPlayersMb = 2;
                this.playersMoney[0] = 0;
                this.playersMoney[1] = 0;
                this.somePlayersData[1] = 0;
                this.somePlayersData[2] = 1;
                this.playersIndexes[0] = 1;
                this.playersIndexes[1] = 2;
                this.playersTeams[0] = 0;
                this.playersTeams[1] = 1;
                this.mapStartUnitCap = 100;
            }

            for (short i = 0; i < this.housesDataArr.Length; i = (short)(i + 1))
            {
                m = this.housesDataArr[i][2];
                if ((m > 0) && (this.mapPlayersTypes[sub_e276(m)] == 2))
                { //NONE
                    occupyHouse(this.housesDataArr[i][0], this.housesDataArr[i][1], 0);
                }
            }
            int m1 = dis.readInt();
            dis.skip(m1 * 4);
            int sLength = dis.readInt();
            //this.playersKings = new C_Unit[this.mapMaxPlayersMb];
            //this.playerKingsMb = new C_Unit[this.mapMaxPlayersMb][4];
            //this.playerUnitsCount = new int[this.mapMaxPlayersMb];
            for (short i = 0; i < sLength; i = (short)(i + 1))
            {
                byte uType = dis.readByte();
                int posX = dis.readShort() / 24;
                int posY = dis.readShort() / 24;
                byte unitType = (byte)(uType % 12);
                byte playerId = (byte)sub_e276(1 + uType / 12);
                if (this.mapPlayersTypes[playerId] != 2)
                { //not NONE
                    //C_Unit unit1 = C_Unit.createUnitOnMap(unitType, playerID, posX, posY);
                    if (unitType == 9)
                    {
                        //this.playersKings[playerID] = unit1;
                    }
                }
            }
            dis.close();
        }

        public void repairDestroyedHouse(byte paramByte, int inX, int inY)
        {
            this.mapTilesIds[inX][inY] = paramByte;
        }

        public void occupyHouse(int inX, int inY, int playerId)
        {
            if (this.mapTilesIds[inX][inY] >= this.houseTileIdStartIndex)
            {
                var var = (byte)(this.houseTileIdStartIndex + playerId * 2 + (this.mapTilesIds[inX][inY] - this.houseTileIdStartIndex) % 2);
                repairDestroyedHouse(var, inX, inY);
            }
        }

        public int sub_e276(int playerId)
        {
            if ((playerId != -1) && (playerId != 0))
            {
                return this.somePlayersData[playerId];
            }
            return -1;
        }

        public byte getTileType(int paramInt1, int paramInt2)
        {
            return this.tilesDefs[this.mapTilesIds[paramInt1][paramInt2]];
        }

        public int tileOwnerPlayerIndex(int inX, int inY)
        {
            return houseOwnerPlayerIndex(inX, inY, this.mapTilesIds);
        }

        public int houseOwnerPlayerIndex(int inX, int inY, byte[][] mapData)
        {
            if (mapData[inX][inY] >= this.houseTileIdStartIndex)
            {
                return (mapData[inX][inY] - this.houseTileIdStartIndex) / 2;
            }
            return -1;
        }

        public void readTilesData(InputStream tilesStream0)
        {
            var tilesStream = new DataInputStream(tilesStream0);
            int tilesStreamLength = tilesStream.readShort();
            tilesStream.readShort();
            this.tilesDefs = new byte[tilesStreamLength];
            for (var k = 0; k < tilesStreamLength; k++)
            {
                this.tilesDefs[k] = tilesStream.readByte();
            }
            tilesStream.close();            
        }

    
    }
    
}
