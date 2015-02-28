using java.lang;
using javax.microedition.lcdui;


namespace aeii
{
    public sealed override class G_FightAnimation
    {

        public static sealed override String[] bgTypeNames = { "road", "grass", "woods", "hill",
			"mountain", "water", "bridge", "town" };
        public static sealed override byte[] var_afd = { 0, 1, 1, 1, 4, 5, 6, 7, 7, 7 };
        public static sealed override byte[] var_b05 = { 0, 1, 2, 3, 4, 5, 6, 7, 7, 7 };
        public static sealed override String[] unitTypeNames = { "soldier", "archer", "lizard",
			"wizard", "wisp", "spider", "golem", "catapult", "wyvern", "king",
			"skeleton" };
        public I_Game m_game;
        public C_Unit m_unit;
        public byte unitType;
        public bool var_b2d = false;
        public bool var_b35 = false;
        public bool var_b3d;
        public byte unitStartHealth;
        public byte unitHealth3;
        public byte unitStartCharsCount;
        public byte unitChars3;
        public byte unitCharsCount;
        public byte var_b6d;
        public bool var_b75;
        public int var_b7d = 0;
        public static sealed override byte[] var_b85 = { 3, -3 };
        public F_Sprite var_b8d;
        public F_Sprite kingWaveSprite;
        public F_Sprite archerArrowSprite;
        public F_Sprite slashSprite;
        public int var_bad;
        public int var_bb5;
        public long var_bbd;
        public long someStartTime7;
        public long someStartTime3;
        public F_Sprite[] unitCharsSprites;
        public H_ImageExt[] multipleBgImages;
        public H_ImageExt var_be5;
        public G_FightAnimation otherFightAnim;
        public int tileType;
        public int var_bfd;
        public int var_c05;
        public int var_c0d;
        public bool var_c15 = false;
        public int unitHealth;
        public int var_c25;
        public int[][] var_c2d;
        public int[] unitsRotationAngles;
        public int var_c3d;
        public int var_c45 = 20;
        public int var_c4d;
        public int var_c55;
        public byte[][] var_c5d;
        public F_Sprite kingHeadsSprite;
        public F_Sprite kingHeadsBackSprite;
        public F_Sprite[] var_c75;
        public int var_c7d;
        public int var_c85;

        public G_FightAnimation(I_Game aGame, C_Unit aUnit, G_FightAnimation faInst)
			{
		this.m_game = aGame;
		this.m_unit = aUnit;
		this.unitType = aUnit.unitTypeId;
		this.otherFightAnim = faInst;
		this.unitStartHealth = ((byte) aUnit.unitHealthMb);
		this.unitHealth = this.unitStartHealth;
		this.unitStartCharsCount = ((byte) aUnit.getAliveCharactersCount());
		this.unitCharsCount = this.unitStartCharsCount;
		int i = 0;
		if (faInst == null) {
			this.var_b6d = 0;
			this.var_bad = 0;
			this.var_b75 = true;
		} else {
			i = aGame.viewportWidth;
			this.var_b6d = 1;
			this.var_b7d = 0;
		}
		this.tileType = aGame.getTileType(aUnit.positionX, aUnit.positionY);
		int j = var_b05[this.tileType];
		int k = var_afd[this.tileType];
		if ((faInst != null) && (k == var_afd[faInst.tileType])) {
			this.multipleBgImages = new H_ImageExt[faInst.multipleBgImages.Length];
			System.Array.Copy(faInst.multipleBgImages, 0, this.multipleBgImages, 0,
					this.multipleBgImages.Length);
		} else {
			this.multipleBgImages = new F_Sprite(bgTypeNames[k]).frameImages;
		}
		if (this.var_b6d == 1) {
			for (int m = 0; m < this.multipleBgImages.Length; m++) {
				this.multipleBgImages[m] = new H_ImageExt(this.multipleBgImages[m], 1);
			}
		}
		try {
			if ((faInst != null)
					&& (j == var_b05[faInst.tileType])) {
				this.var_be5 = faInst.var_be5;
			} else {
				this.var_be5 = new H_ImageExt(bgTypeNames[j] + "_bg");
			}
			if (this.var_b6d == 1) {
				this.var_be5 = new H_ImageExt(this.var_be5, 1);
			}
		} catch (Exception localException) {
		}
		if (this.var_be5 != null) {
			this.var_bfd = this.var_be5.imageHeight;
		}
		this.var_c05 = (aGame.viewportWidth / this.multipleBgImages[0].imageWidth);
		if (aGame.viewportWidth % this.multipleBgImages[0].imageWidth != 0) {
			this.var_c05 += 1;
		}
		this.var_c0d = ((aGame.var_3bfb - this.var_bfd) / this.multipleBgImages[0].imageHeight);
		if ((aGame.var_3bfb - this.var_bfd)
				% this.multipleBgImages[0].imageHeight != 0) {
			this.var_c0d += 1;
		}
		this.var_c5d = new byte[this.var_c05][];
            for(int i23=0;i23<this.var_c05;i23++){
                this.var_c5d[i23]=new byte[this.var_c0d];
            }
		for (int m = 0; m < this.var_c05; m++) {
			for (int n = 0; n < this.var_c0d; n++) {
				this.var_c5d[m][n] = ((byte) Math.abs(E_MainCanvas.random
						.nextInt() % this.multipleBgImages.Length));
			}
		}
		this.var_b8d = new F_Sprite(unitTypeNames[this.unitType],
				aGame.playersIndexes[aUnit.playerId]);
		if ((faInst != null)
				&& (faInst.unitType == this.unitType)) {
			if (faInst.slashSprite != null) {
				this.slashSprite = new F_Sprite(faInst.slashSprite);
			}
			if (faInst.kingWaveSprite != null) {
				this.kingWaveSprite = new F_Sprite(faInst.kingWaveSprite);
			}
			if (faInst.archerArrowSprite != null) {
				this.archerArrowSprite = new F_Sprite(faInst.archerArrowSprite);
			}
			if (faInst.kingHeadsSprite != null) {
				this.kingHeadsSprite = new F_Sprite(faInst.kingHeadsSprite);
				this.kingHeadsBackSprite = new F_Sprite(faInst.kingHeadsBackSprite);
			}
		} else if ((this.unitType == 0) || (this.unitType == 10)
				|| (this.unitType == 5)) {
			this.slashSprite = new F_Sprite("slash");
		} else if (this.unitType == 1) {
			this.archerArrowSprite = new F_Sprite("archer_arrow");
		} else if (this.unitType == 9) {
			this.kingWaveSprite = new F_Sprite("kingwave");
			this.slashSprite = new F_Sprite("kingslash");
			this.kingHeadsSprite = new F_Sprite("king_heads");
			this.kingHeadsBackSprite = new F_Sprite("king_heads_back");
		} else if (this.unitType == 2) {
			this.kingWaveSprite = new F_Sprite("watermagic");
			this.archerArrowSprite = new F_Sprite("fish");
		} else if ((this.unitType == 7) || (this.unitType == 6)) {
			this.archerArrowSprite = new F_Sprite("crater");
		} else if (this.unitType == 3) {
			this.kingWaveSprite = new F_Sprite("spell");
			this.archerArrowSprite = this.kingWaveSprite;
		}
		if (this.kingWaveSprite != null) {
			this.kingWaveSprite.startAnimation(0, this.var_b75);
		}
		if (this.archerArrowSprite != null) {
			this.archerArrowSprite.startAnimation(0, this.var_b75);
		}
		if (this.slashSprite != null) {
			this.slashSprite.startAnimation(0, this.var_b75);
		}
		if (this.kingHeadsSprite != null) {
			this.kingHeadsSprite.startAnimation(aUnit.kingIndex, this.var_b75);
			this.kingHeadsSprite.setCurrentFrameIndex(aUnit.kingIndex);
			this.kingHeadsBackSprite.startAnimation(0, this.var_b75);
			this.kingHeadsBackSprite.setCurrentFrameIndex(aUnit.kingIndex);
		}
		this.var_c2d = new int[aUnit.charsData.Length][];
        for (int i5 = 0; i5 < aUnit.charsData.Length; i5++)
                this.var_c2d[i5] = new int[2];
		for (int m = 0; m < this.var_c2d.Length; m++) {
			this.var_c2d[m][0] = (aUnit.charsData[m][0]
					* aGame.someCanWidth / 128);
			if (this.var_b6d == 1) {
				this.var_c2d[m][0] = (aGame.viewportWidth
						- this.var_c2d[m][0] - this.var_b8d.frameWidth + i);
			}
			this.var_c2d[m][1] = (aUnit.charsData[m][1]
					* aGame.var_3bfb / 128 - this.var_b8d.frameHeight);
		}
		this.unitCharsSprites = new F_Sprite[this.unitStartCharsCount];
		if ((this.unitType == 4) || (this.unitType == 6)) {
			this.unitsRotationAngles = new int[this.unitStartCharsCount];
		}
		for (int m = 0; m < this.unitStartCharsCount; m++) {
			this.unitCharsSprites[m] = new F_Sprite(this.var_b8d);
			aGame.addSpriteTo(this.unitCharsSprites[m]);
			this.unitCharsSprites[m].setSpritePosition(this.var_c2d[m][0], this.var_c2d[m][1]);
			this.unitCharsSprites[m].startAnimation(0, this.var_b75);
			this.unitCharsSprites[m].var_854 = false;
			this.unitCharsSprites[m].someYOffset3 = 0;
			if (this.unitType == 6) { // golem
				this.unitCharsSprites[m].m_isRotating = true;
				this.unitsRotationAngles[m] = E_MainCanvas.getRandomMax(360);
				this.unitCharsSprites[m].someYVal1 = (-6 + 4
						* A_MenuBase.getSin1024(this.unitsRotationAngles[m]) >> 10);
			} else if (this.unitType == 4) { //wisp
				this.unitCharsSprites[m].m_isRotating = true;
				this.unitCharsSprites[m].someYVal1 = (-5 - E_MainCanvas.getRandomMax(10));
				this.unitsRotationAngles[m] = E_MainCanvas.getRandomMax(360);
			} else if (this.unitType == 9) { //king
				this.unitCharsSprites[m].kingHeadSprite = this.kingHeadsSprite;
				this.unitCharsSprites[m].kingBackSprite = this.kingHeadsBackSprite;
			}
		}
	}

        public sealed override int sub_1673(F_Sprite sprite, int paramInt)
        {
            if (this.var_b6d == 1)
            {
                return this.var_b8d.frameWidth - paramInt - sprite.frameWidth;
            }
            return paramInt;
        }

        public sealed override void sub_16bb()
        {
            this.var_b2d = true;
            this.var_b7d = 1;
            this.someStartTime7 = this.m_game.time;
        }

        public sealed override void sub_16eb()
        {
            int i;
            int i3;
            int m;
            F_Sprite localClass_f_0455;
            switch (this.var_b7d)
            {
                case 1:
                    F_Sprite localClass_f_0454;
                    if (this.unitType == 6)
                    {
                        if (this.var_c4d == 0)
                        {
                            if (this.m_game.time - this.someStartTime7 >= 200L)
                            {
                                if (this.var_c85 < this.unitCharsCount)
                                {
                                    this.unitCharsSprites[this.var_c85].var_87c = -1;
                                    this.unitCharsSprites[this.var_c85].m_isRotating = false;
                                    this.unitCharsSprites[this.var_c85].someYVal3 = 0;
                                }
                                if (++this.var_c85 >= this.unitCharsCount)
                                {
                                    this.var_c85 = 0;
                                    this.var_c4d = 1;
                                    this.someStartTime7 = this.m_game.time;
                                }
                            }
                        }
                        else if (this.var_c4d == 1)
                        {
                            if (this.m_game.time - this.someStartTime7 >= 200L)
                            {
                                if (this.var_c85 < this.unitCharsCount)
                                {
                                    this.unitCharsSprites[this.var_c85].someYVal3 = -1;
                                }
                                if (++this.var_c85 >= this.unitCharsCount)
                                {
                                    this.var_c85 = 0;
                                    this.var_c4d = 2;
                                    this.someStartTime7 = this.m_game.time;
                                }
                            }
                        }
                        else if (this.var_c4d == 2)
                        {
                            i = 1;
                            if (this.m_game.time - this.someStartTime7 >= 200L)
                            {
                                if (this.var_c85 < this.unitCharsCount)
                                {
                                    this.unitCharsSprites[this.var_c85].var_87c = 0;
                                    this.unitCharsSprites[this.var_c85].someYVal3 = 0;
                                    this.unitCharsSprites[this.var_c85]
                                            .startAnimation(2, this.var_b75);
                                    this.unitCharsSprites[this.var_c85].var_81c = 1;
                                }
                                if (++this.var_c85 < this.unitCharsCount)
                                {
                                    i = 0;
                                }
                            }
                            else
                            {
                                i = 0;
                            }
                            for (int j = 0; j < this.unitCharsCount; j++)
                            {
                                if (this.unitCharsSprites[j].var_87c == 0)
                                {
                                    if (this.unitCharsSprites[j].currentFrameIndex == 1)
                                    {
                                        this.unitCharsSprites[j].someYVal1 = 0;
                                        this.unitCharsSprites[j].var_87c = 1;
                                        E_MainCanvas.vibrate(200);
                                        this.m_game.startShakingScreen(1200);
                                        E_MainCanvas.playMusicLooped(14, 1);
                                        for (int n = 0; n < 2; n++)
                                        {
                                            F_Sprite localClass_f_0456;
                                            (localClass_f_0456 = F_Sprite.someSpriteCopy(
                                                    this.m_game.smokeSprite, 0, 0, -1, 1,
                                                    E_MainCanvas.getRandomMax(4) * 50,
                                                    (byte)0))
                                                    .setSpritePosition(
                                                            this.unitCharsSprites[j].posXPixel
                                                                    + E_MainCanvas
                                                                            .getRandomMax(this.var_b8d.frameWidth
                                                                                    - localClass_f_0456.frameWidth),
                                                            this.unitCharsSprites[j].posYPixel
                                                                    + this.var_b8d.frameHeight
                                                                    - localClass_f_0456.frameHeight
                                                                    + 2);
                                            localClass_f_0456.var_85c = true;
                                            this.m_game.addSpriteTo(localClass_f_0456);
                                        }
                                        (localClass_f_0454 = F_Sprite.someSpriteCopy(
                                                this.m_game.smokeSprite, -1, 0, -1, 1,
                                                E_MainCanvas.getRandomMax(4) * 50, (byte)0))
                                                .setSpritePosition(
                                                        this.unitCharsSprites[j].posXPixel,
                                                        this.unitCharsSprites[j].posYPixel
                                                                + this.var_b8d.frameHeight
                                                                - localClass_f_0454.frameHeight
                                                                + 2);
                                        localClass_f_0454.var_85c = true;
                                        this.m_game.addSpriteTo(localClass_f_0454);
                                        (localClass_f_0454 = F_Sprite.someSpriteCopy(
                                                this.m_game.smokeSprite, 1, 0, -1, 1,
                                                E_MainCanvas.getRandomMax(4) * 50, (byte)0))
                                                .setSpritePosition(
                                                        this.unitCharsSprites[j].posXPixel
                                                                + this.var_b8d.frameWidth
                                                                - localClass_f_0454.frameWidth,
                                                        this.unitCharsSprites[j].posYPixel
                                                                + this.var_b8d.frameHeight
                                                                - localClass_f_0454.frameHeight
                                                                + 2);
                                        localClass_f_0454.var_85c = true;
                                        this.m_game.addSpriteTo(localClass_f_0454);
                                    }
                                    i = 0;
                                }
                                else if (this.unitCharsSprites[j].var_87c != -1)
                                {
                                    if (this.unitCharsSprites[j].var_81c > 0)
                                    {
                                        i = 0;
                                    }
                                    else if (this.unitCharsSprites[j].var_81c != -1)
                                    {
                                        if (this.unitCharsSprites[j].var_87c == 1)
                                        {
                                            this.unitCharsSprites[j].startAnimation(3, this.var_b75);
                                            this.unitCharsSprites[j].var_81c = 1;
                                            this.unitCharsSprites[j].var_87c = 2;
                                            i = 0;
                                        }
                                        else if (this.unitCharsSprites[j].var_87c == 2)
                                        {
                                            this.unitCharsSprites[j].someYVal1 = -6;
                                            this.unitCharsSprites[j].m_isRotating = true;
                                            this.unitCharsSprites[j].someYVal3 = E_MainCanvas
                                                    .getRandomWithin(-2, 3);
                                            this.unitCharsSprites[j].startAnimation(0, this.var_b75);
                                            this.unitCharsSprites[j].var_81c = -1;
                                            this.unitCharsSprites[j].var_87c = 3;
                                            i = 0;
                                        }
                                    }
                                }
                            }
                            if (i != 0)
                            {
                                this.var_c4d = 0;
                                this.someStartTime7 = this.m_game.time;
                                this.var_b7d = 4;
                            }
                        }
                    }
                    else if (this.var_c4d == 0)
                    {
                        if (this.m_game.time - this.someStartTime7 >= 200L)
                        {
                            if (this.unitType == 9)
                            {
                                this.var_c75 = new F_Sprite[this.unitCharsCount * 2];
                            }
                            else if ((this.unitType != 7) && (this.unitType != 1))
                            {
                                this.var_c75 = new F_Sprite[this.unitCharsCount];
                            }
                            this.var_c4d = 1;
                            this.var_c85 = 0;
                            this.someStartTime7 = this.m_game.time;
                        }
                    }
                    else if (this.var_c4d == 1)
                    {
                        i = 1;
                        if (this.var_c85 < this.unitCharsCount)
                        {
                            this.unitCharsSprites[this.var_c85].startAnimation(2, this.var_b75);
                            this.unitCharsSprites[this.var_c85].var_81c = 1;
                            if ((this.unitType == 3) || (this.unitType == 2))
                            {
                                this.var_c75[this.var_c85] = F_Sprite.someSpriteCopy(
                                        this.kingWaveSprite, 0, 0, 0, 1, 50, (byte)0);
                                this.var_c75[this.var_c85]
                                        .setSpritePosition(
                                                this.unitCharsSprites[this.var_c85].posXPixel
                                                        + sub_1673(
                                                                this.var_c75[this.var_c85],
                                                                this.unitCharsSprites[this.var_c85].frameWidth),
                                                this.unitCharsSprites[this.var_c85].posYPixel);
                                this.m_game.addSpriteTo(this.var_c75[this.var_c85]);
                            }
                            else if (this.unitType == 1)
                            {
                                F_Sprite localClass_f_0451;
                                (localClass_f_0451 = F_Sprite.someSpriteCopy(this.archerArrowSprite,
                                        0, 0, 0, 1, 0, (byte)0)).startAnimation(1,
                                        this.var_b75);
                                localClass_f_0451
                                        .setSpritePosition(
                                                this.unitCharsSprites[this.var_c85].posXPixel
                                                        + sub_1673(
                                                                localClass_f_0451,
                                                                this.unitCharsSprites[this.var_c85].frameWidth),
                                                this.unitCharsSprites[this.var_c85].posYPixel);
                                this.m_game.addSpriteTo(localClass_f_0451);
                            }
                            else
                            {
                                if (this.unitType == 7)
                                {
                                    this.unitCharsSprites[this.var_c85].randPosCounter = 5;
                                    for (int k = 0; k < 3; k++)
                                    {
                                        (localClass_f_0454 = F_Sprite.someSpriteCopy(
                                                this.m_game.bigSmokeSprite,
                                                E_MainCanvas.getRandomWithin(-1, 2), 0, 0, 1,
                                                E_MainCanvas.getRandomMax(4) * 50, (byte)0))
                                                .setSpritePosition(
                                                        this.unitCharsSprites[this.var_c85].posXPixel
                                                                + this.var_b8d.frameWidth
                                                                / 2,
                                                        this.unitCharsSprites[this.var_c85].posYPixel);
                                        localClass_f_0454.var_85c = true;
                                        this.m_game.addSpriteTo(localClass_f_0454);
                                    }
                                }
                                if (this.unitType == 9)
                                {
                                    F_Sprite localClass_f_0452;
                                    (localClass_f_0452 = F_Sprite.someSpriteCopy(
                                            this.slashSprite, 0, 0, 0, 1, 200, (byte)0))
                                            .setSpritePosition(this.unitCharsSprites[0].posXPixel,
                                                    this.unitCharsSprites[0].posYPixel
                                                            + this.var_b8d.frameHeight);
                                    localClass_f_0452.someYVal1 = (-this.var_b8d.frameHeight);
                                    this.m_game.addSpriteTo(localClass_f_0452);
                                    this.var_c75[0] = F_Sprite.someSpriteCopy(
                                            this.kingWaveSprite, var_b85[this.var_b6d] * 3,
                                            -2, 0, -1, 100, (byte)0);
                                    int i1 = this.unitCharsSprites[this.var_c85].posXPixel
                                            + sub_1673(
                                                    this.var_c75[this.var_c85],
                                                    this.unitCharsSprites[this.var_c85].frameWidth / 2);
                                    i3 = this.unitCharsSprites[this.var_c85].posYPixel
                                            + this.var_b8d.frameHeight
                                            - this.var_c75[this.var_c85].frameHeight + 2;
                                    this.var_c75[0].setSpritePosition(i1, i3);
                                    this.var_c75[1] = F_Sprite.someSpriteCopy(
                                            this.kingWaveSprite, var_b85[this.var_b6d] * 3, 1,
                                            0, -1, 100, (byte)0);
                                    this.var_c75[1].setSpritePosition(i1, i3);
                                    this.m_game.addSpriteTo(this.var_c75[1]);
                                    this.var_c75[1].var_86c = this.var_b6d;
                                    this.m_game.addSpriteTo(this.var_c75[0]);
                                    this.var_c75[0].var_86c = this.var_b6d;
                                }
                                else if (this.unitType == 8)
                                {
                                    this.var_c75[this.var_c85] = F_Sprite.someSpriteCopy(
                                            null, var_b85[this.var_b6d], 0, 0, -1,
                                            2000, (byte)6);
                                    this.var_c75[this.var_c85]
                                            .setSpritePosition(
                                                    this.unitCharsSprites[this.var_c85].posXPixel
                                                            + sub_1673(
                                                                    this.var_c75[this.var_c85],
                                                                    this.unitCharsSprites[this.var_c85].frameWidth + 2),
                                                    this.unitCharsSprites[this.var_c85].posYPixel + 30);
                                    this.var_c75[this.var_c85].var_85c = true;
                                    E_MainCanvas.vibrate(200);
                                    this.m_game.startShakingScreen(1200);
                                    E_MainCanvas.playMusicLooped(14, 1);
                                    this.m_game.addSpriteTo(this.var_c75[this.var_c85]);
                                }
                            }
                        }
                        if (++this.var_c85 < this.unitCharsCount)
                        {
                            i = 0;
                        }
                        for (m = 0; m < this.unitCharsCount; m++)
                        {
                            if (this.unitCharsSprites[m].var_81c > 0)
                            {
                                i = 0;
                            }
                            else if ((this.unitType != 7)
                                  && (this.unitCharsSprites[m].var_81c != -1))
                            {
                                if (this.unitCharsSprites[m].var_87c == 0)
                                {
                                    this.unitCharsSprites[m].startAnimation(3, this.var_b75);
                                    this.unitCharsSprites[m].var_81c = 1;
                                    this.unitCharsSprites[m].var_87c = 1;
                                    if (this.unitType == 8)
                                    {
                                        this.var_c75[m].isUpdatingMb = false;
                                    }
                                    i = 0;
                                }
                                else if (this.unitCharsSprites[m].var_87c == 1)
                                {
                                    this.unitCharsSprites[m].startAnimation(0, this.var_b75);
                                    this.unitCharsSprites[m].var_81c = -1;
                                    this.unitCharsSprites[m].var_87c = 2;
                                    i = 0;
                                }
                            }
                            if ((this.unitType == 8) && (this.var_c75[m] != null)
                                    && (this.var_c75[m].isUpdatingMb))
                            {
                                (localClass_f_0455 = F_Sprite.someSpriteCopy(
                                        this.m_game.bigSmokeSprite, var_b85[this.var_b6d]
                                                * E_MainCanvas.getRandomWithin(1, 4),
                                        E_MainCanvas.getRandomWithin(-2, 3), 0, 1,
                                        50 * E_MainCanvas.getRandomMax(4), (byte)0))
                                        .setSpritePosition(
                                                this.unitCharsSprites[m].posXPixel
                                                        + sub_1673(localClass_f_0455,
                                                                this.var_b8d.frameWidth),
                                                this.var_c75[m].posYPixel
                                                        + E_MainCanvas
                                                                .getRandomMax(30 - this.var_c75[m].frameHeight)
                                                        - 15);
                                localClass_f_0455.var_85c = true;
                                this.m_game.addSpriteTo(localClass_f_0455);
                            }
                        }
                        if (i != 0)
                        {
                            if (this.m_unit.unitTypeId == 9)
                            {
                                this.var_b7d = 6;
                            }
                            else
                            {
                                this.var_b7d = 4;
                            }
                            this.var_c55 = 400;
                            if (this.m_unit.unitTypeId == 8)
                            {
                                this.var_c55 = 0;
                            }
                            this.someStartTime7 = this.m_game.time;
                            this.var_c4d = 0;
                            return;
                        }
                    }
                    break;
                case 6:
                    i = 1;
                    for (m = 0; m < this.var_c75.Length; m++)
                    {
                        if ((this.unitType == 9) && (E_MainCanvas.getRandomMax(2) == 0))
                        {
                            (localClass_f_0455 = F_Sprite.someSpriteCopy(
                                    this.m_game.bigSmokeSprite, E_MainCanvas.getRandomWithin(-2, 1),
                                    0, -1, 1, 100, (byte)0)).setSpritePosition(
                                    this.var_c75[m].posXPixel
                                            + sub_1673(this.var_c75[m], 0),
                                    this.var_c75[m].posYPixel + this.var_c75[m].frameHeight
                                            - localClass_f_0455.frameHeight);
                            if (this.var_c75[m].var_87c == 1)
                            {
                                localClass_f_0455.var_86c = this.otherFightAnim.var_b6d;
                            }
                            else
                            {
                                localClass_f_0455.var_86c = this.var_b6d;
                            }
                            this.m_game.addSpriteTo(localClass_f_0455);
                        }
                        if (this.unitType == 9)
                        {
                            if (this.var_b6d == 0)
                            {
                                if (this.var_c75[m].posXPixel >= this.m_game.someGWidth)
                                {
                                    if (this.var_c75[m].var_87c == 0)
                                    {
                                        this.var_c75[m].setSpritePosition(this.m_game.viewportWidth
                                                - this.var_c75[m].frameWidth,
                                                this.var_c75[m].posYPixel);
                                        this.var_c75[m].var_86c = this.otherFightAnim.var_b6d;
                                        this.var_c75[m].var_87c = 1;
                                        i = 0;
                                    }
                                    else if (this.var_c75[m].var_87c == 1)
                                    {
                                        this.m_game.removeSpriteFrom(this.var_c75[m]);
                                        this.var_c75[m].var_87c = 2;
                                    }
                                }
                                else
                                {
                                    i = 0;
                                }
                            }
                            else if (this.var_b6d == 1)
                            {
                                if (this.var_c75[m].posXPixel + this.var_c75[m].frameWidth < 0)
                                {
                                    if (this.var_c75[m].var_87c == 0)
                                    {
                                        this.var_c75[m].setSpritePosition(this.m_game.viewportWidth,
                                                this.var_c75[m].posYPixel);
                                        this.var_c75[m].var_86c = this.otherFightAnim.var_b6d;
                                        this.var_c75[m].var_87c = 1;
                                        i = 0;
                                    }
                                    else if (this.var_c75[m].var_87c == 1)
                                    {
                                        this.m_game.removeSpriteFrom(this.var_c75[m]);
                                        this.var_c75[m].var_87c = 2;
                                    }
                                }
                                else
                                {
                                    i = 0;
                                }
                            }
                        }
                    }
                    if (i != 0)
                    {
                        this.var_c4d = 0;
                        this.var_b7d = 4;
                        this.someStartTime7 = this.m_game.time;
                        return;
                    }
                    break;
                case 4:
                    if (this.var_c4d == 0)
                    {
                        if (this.m_game.time - this.someStartTime7 >= this.var_c55)
                        {
                            this.otherFightAnim.sub_3363();
                            if (this.unitType != 1)
                            {
                                this.m_game.startShakingScreen(200);
                            }
                            E_MainCanvas.vibrate(200);
                            E_MainCanvas.playMusicLooped(14, 1);
                            if (this.archerArrowSprite != null)
                            {
                                this.var_c7d = C_Unit.unitsChars[this.unitType].Length;
                            }
                            this.var_c4d = 1;
                        }
                    }
                    else if (this.var_c4d == 1)
                    {
                        if (--this.var_c7d >= 0)
                        {
                            F_Sprite localClass_f_0453;
                            if ((this.unitType == 3) || (this.unitType == 2)
                                    || (this.unitType == 1))
                            {
                                localClass_f_0453 = F_Sprite.someSpriteCopy(this.archerArrowSprite,
                                        0, 0, 0, 1, 0, (byte)0);
                            }
                            else
                            {
                                localClass_f_0453 = F_Sprite.someSpriteCopy(this.archerArrowSprite,
                                        0, 0, 0, -1, 0, (byte)0);
                            }
                            if ((this.unitType == 2) || (this.unitType == 1))
                            {
                                localClass_f_0453.var_854 = false;
                            }
                            int i2 = E_MainCanvas.getRandomMax(this.m_game.someGWidth / 2
                                    - localClass_f_0453.frameWidth);
                            i3 = 0;
                            if (this.otherFightAnim.var_be5 != null)
                            {
                                i3 = 0 + (this.otherFightAnim.var_be5.imageHeight - localClass_f_0453.frameHeight);
                            }
                            int i4 = (this.m_game.var_3bfb - i3)
                                    * (this.var_c7d * 2 + 1)
                                    / (C_Unit.unitsChars[this.unitType].Length * 2)
                                    - localClass_f_0453.frameHeight / 2 + i3;
                            if (this.var_b6d == 0)
                            {
                                i2 += this.m_game.viewportWidth;
                            }
                            if ((this.unitType == 7) || (this.unitType == 6))
                            {
                                localClass_f_0453.someYVal1 = i4;
                                i4 = 0;
                            }
                            localClass_f_0453.setSpritePosition(i2, i4);
                            this.m_game.addSpriteTo(localClass_f_0453);
                            for (int i5 = 0; i5 < 3; i5++)
                            {
                                F_Sprite localClass_f_0457;
                                if ((this.unitType == 7) || (this.unitType == 6))
                                {
                                    (localClass_f_0457 = F_Sprite.someSpriteCopy(
                                            this.m_game.smokeSprite,
                                            E_MainCanvas.getRandomWithin(-1, 2), 0,
                                            E_MainCanvas.getRandomWithin(-2, 0), 1,
                                            E_MainCanvas.getRandomMax(4) * 50, (byte)0))
                                            .setSpritePosition(
                                                    i2
                                                            + E_MainCanvas
                                                                    .getRandomMax(localClass_f_0453.frameWidth
                                                                            - localClass_f_0457.frameWidth),
                                                    localClass_f_0453.someYVal1
                                                            + localClass_f_0453.frameHeight
                                                            - localClass_f_0457.frameHeight
                                                            + 1);
                                    localClass_f_0457.someYVal1 = (-localClass_f_0453.frameHeight / 2);
                                }
                                else
                                {
                                    (localClass_f_0457 = F_Sprite.someSpriteCopy(
                                            this.m_game.bigSmokeSprite,
                                            E_MainCanvas.getRandomWithin(-1, 2), 0, -1, 1, 100,
                                            (byte)0))
                                            .setSpritePosition(
                                                    i2
                                                            + E_MainCanvas
                                                                    .getRandomMax(localClass_f_0453.frameWidth
                                                                            - localClass_f_0457.frameWidth),
                                                    i4 + localClass_f_0453.frameHeight
                                                            - localClass_f_0457.frameHeight
                                                            + 1);
                                }
                                this.m_game.addSpriteTo(localClass_f_0457);
                            }
                        }
                        this.var_b7d = 7;
                        this.someStartTime7 = this.m_game.time;
                        return;
                    }
                    break;
                case 7:
                    if (this.m_game.time - this.someStartTime7 >= 1000L)
                    {
                        this.var_b7d = 0;
                        this.var_b35 = true;
                    }
                    break;
            }
        }

        public sealed override void sub_2730()
        {
            for (int i = 0; i < this.unitCharsCount; i++)
            {
                this.unitCharsSprites[i].randPosCounter = 6;
            }
        }

        public sealed override void sub_2782()
        {
            switch (this.var_b7d)
            {
                case 1:
                    if (this.var_c4d == 0)
                    {
                        if (this.m_game.time - this.someStartTime7 >= 200L)
                        {
                            this.var_c4d = 1;
                            this.someStartTime7 = this.m_game.time;
                        }
                    }
                    else
                    {
                        F_Sprite localClass_f_0451;
                        if (this.var_c4d == 1)
                        {
                            this.var_c45 += 5;
                            if (this.var_c45 >= 90)
                            {
                                this.var_c4d += 1;
                                this.someStartTime7 = this.m_game.time;
                            }
                            if ((this.var_c45 - 20) % 15 == 0)
                            {
                                for (int i = 0; i < this.unitCharsSprites.Length; i++)
                                {
                                    (localClass_f_0451 = F_Sprite.someSpriteCopy(
                                            this.m_game.bigSmokeSprite,
                                            E_MainCanvas.getRandomWithin(-1, 2), 0, 0, 1, 100,
                                            (byte)0))
                                            .setSpritePosition(
                                                    this.unitCharsSprites[i].posXPixel
                                                            + E_MainCanvas
                                                                    .getRandomMax(this.var_b8d.frameWidth
                                                                            - localClass_f_0451.frameWidth),
                                                    this.unitCharsSprites[i].posYPixel
                                                            + this.var_b8d.frameHeight
                                                            - localClass_f_0451.frameHeight
                                                            + 1);
                                    this.m_game.addSpriteTo(localClass_f_0451);
                                }
                            }
                        }
                        else if ((this.var_c4d == 2)
                              && (this.m_game.time - this.someStartTime7 >= 400L))
                        {
                            this.var_c45 = 20;
                            this.otherFightAnim.sub_3363();
                            this.var_b7d = 4;
                            this.otherFightAnim.var_c15 = false;
                            this.someStartTime7 = this.m_game.time;
                        }
                        if (++this.var_c3d >= 2)
                        {
                            for (int i = 0; i < this.otherFightAnim.unitCharsSprites.Length; i++)
                            {
                                (localClass_f_0451 = F_Sprite
                                        .someSpriteCopy(this.m_game.redsparkSprite, 0, 0, 0, 1,
                                                50, (byte)0))
                                        .setSpritePosition(
                                                this.otherFightAnim.unitCharsSprites[i].posXPixel
                                                        + E_MainCanvas
                                                                .getRandomMax(this.otherFightAnim.unitCharsSprites[i].frameWidth
                                                                        - localClass_f_0451.frameWidth),
                                                this.otherFightAnim.unitCharsSprites[i].posYPixel
                                                        + E_MainCanvas
                                                                .getRandomMax(this.otherFightAnim.unitCharsSprites[i].frameHeight
                                                                        - localClass_f_0451.frameHeight));
                                localClass_f_0451.var_85c = true;
                                this.m_game.addSpriteTo(localClass_f_0451);
                            }
                            this.var_c3d = 0;
                            this.otherFightAnim.sub_2730();
                        }
                    }
                    break;
                case 4:
                    if (this.m_game.time - this.someStartTime7 >= 800L)
                    {
                        this.var_b35 = true;
                        this.var_b7d = 0;
                    }
                    break;
            }
            int i22 = 0;
            if (this.m_game.time - this.var_bbd >= 300L)
            {
                i22 = 1;
                this.var_bbd = this.m_game.time;
            }
            for (int j = 0; j < this.unitCharsSprites.Length; j++)
            {
                if (i22 != 0)
                {
                    F_Sprite lcSprite1 = F_Sprite.someSpriteCopy(null, 0, 0, 0, 1, 500, (byte)4);
                    lcSprite1.setSpritePosition(this.unitCharsSprites[j].posXPixel
                                    + (this.unitCharsSprites[j].frameWidth >> 1),
                                    this.unitCharsSprites[j].posYPixel
                                            + (this.unitCharsSprites[j].frameHeight >> 1)
                                            + this.var_bb5);
                    this.m_game.addSpriteTo(lcSprite1);
                }
                int k = this.unitCharsSprites[j].frameWidth / 3;
                this.unitCharsSprites[j]
                        .setSpritePosition(
                                this.var_c2d[j][0]
                                        + (k * A_MenuBase.getCos2014(this.unitsRotationAngles[j]) >> 10),
                                this.var_c2d[j][1]
                                        + (k * A_MenuBase.getSin1024(this.unitsRotationAngles[j])
                                                / 3 >> 10));
                this.unitsRotationAngles[j] = ((this.unitsRotationAngles[j] + this.var_c45) % 360);
            }
        }

        public sealed override void sub_2ae8()
        {
            if (this.var_c15)
            {
                if (this.m_game.time - this.someStartTime3 >= 300L)
                {
                    this.var_c15 = false;
                    this.var_bad = 0;
                    this.var_bb5 = 0;
                }
                else
                {
                    if (this.var_bad > 0)
                    {
                        this.var_bad = -2;
                    }
                    else
                    {
                        this.var_bad = 2;
                    }
                    this.var_bb5 = (E_MainCanvas.random.nextInt() % 1);
                }
            }
            if ((this.var_b3d) && (this.unitHealth > this.unitHealth3))
            {
                this.unitHealth -= 2;
                if (this.unitHealth < this.unitHealth3)
                {
                    this.unitHealth = this.unitHealth3;
                }
                this.m_game.var_3bf3 = true;
            }
            int i;
            if ((this.unitType == 8) || (this.unitType == 9) || (this.unitType == 7)
                    || (this.unitType == 1) || (this.unitType == 3)
                    || (this.unitType == 2) || (this.unitType == 6))
            {
                sub_16eb();
            }
            else if (this.unitType == 4)
            {
                sub_2782();
            }
            else
            {
                i = 0;
                int j;
                int k;
                switch (this.var_b7d)
                {
                    case 1:
                        if (this.m_game.time - this.someStartTime7 >= 200L)
                        {
                            this.var_b7d = 3;
                        }
                        break;
                    case 3:
                        j = 1;
                        F_Sprite fSprite1;
                        if (this.var_c4d < this.unitCharsCount)
                        {
                            if ((this.unitType == 0) || (this.unitType == 5))
                            {
                                this.unitCharsSprites[this.var_c4d].someYVal3 = -6;
                            }
                            if (this.unitType == 5)
                            {
                                this.unitCharsSprites[this.var_c4d].someXVal3 = (2 * var_b85[this.var_b6d]);
                                for (int kIt = 0; kIt < 3; kIt++)
                                {
                                    fSprite1 = F_Sprite.someSpriteCopy(
                                            this.m_game.bigSmokeSprite,
                                            E_MainCanvas.getRandomWithin(-1, 2), 0, -1, 1, 100,
                                            (byte)0);
                                    fSprite1.setSpritePosition(
                                                    this.unitCharsSprites[this.var_c4d].posXPixel
                                                            + E_MainCanvas
                                                                    .getRandomMax(this.var_b8d.frameWidth
                                                                            - fSprite1.frameWidth),
                                                    this.unitCharsSprites[this.var_c4d].posYPixel
                                                            + this.var_b8d.frameHeight
                                                            - fSprite1.frameHeight
                                                            + 1);
                                    this.m_game.addSpriteTo(fSprite1);
                                }
                            }
                            this.unitCharsSprites[this.var_c4d].someXVal3 = var_b85[this.var_b6d];
                            this.unitCharsSprites[this.var_c4d].startAnimation(1, this.var_b75);
                            this.var_c4d += 1;
                            j = 0;
                        }
                        for (int kIt = 0; kIt < this.var_c4d; kIt++)
                        {
                            if (this.unitType == 10)
                            { //skeleton
                                if (this.unitCharsSprites[kIt].var_87c != -1)
                                {
                                    this.unitCharsSprites[kIt].var_87c += 1;
                                    if (this.unitCharsSprites[kIt].var_87c >= 16)
                                    {
                                        this.unitCharsSprites[kIt].startAnimation(2, this.var_b75);
                                        this.unitCharsSprites[kIt].someXVal3 = 0;
                                        this.unitCharsSprites[kIt].var_87c = -1;
                                        fSprite1 = F_Sprite.someSpriteCopy(null,
                                                0, 0, 0, 1, 800, (byte)2);
                                        fSprite1.setSpritePosition(
                                                this.unitCharsSprites[kIt].posXPixel
                                                        + sub_1673(fSprite1,
                                                                this.var_b8d.frameWidth),
                                                this.unitCharsSprites[kIt].posYPixel
                                                        + this.var_b8d.frameHeight);
                                        this.m_game.addSpriteTo(fSprite1);
                                        fSprite1 = F_Sprite.someSpriteCopy(this.slashSprite, 0, 0, 0, 1,
                                                150, (byte)0);
                                        fSprite1.setSpritePosition(
                                                        this.unitCharsSprites[kIt].posXPixel
                                                                + sub_1673(fSprite1, 24),
                                                        this.unitCharsSprites[kIt].posYPixel + 3);
                                        this.m_game.addSpriteTo(fSprite1);
                                    }
                                    else
                                    {
                                        j = 0;
                                    }
                                }
                            }
                            else if (this.unitCharsSprites[kIt].someYVal1 < 0)
                            {
                                this.unitCharsSprites[kIt].someYVal3 += 1;
                                j = 0;
                            }
                            else if (this.unitCharsSprites[kIt].someYVal3 >= 6)
                            {
                                this.unitCharsSprites[kIt].someYVal1 = 0;
                                this.unitCharsSprites[kIt].someYVal3 = 0;
                                this.unitCharsSprites[kIt].someXVal3 = 0;
                                if ((this.unitType == 0) || (this.unitType == 5))
                                {
                                    this.unitCharsSprites[kIt].startAnimation(2, this.var_b75);
                                    if (this.unitType == 0)
                                    {
                                        fSprite1 = F_Sprite.someSpriteCopy(this.slashSprite, 0, 0, 0, 1,
                                                150, (byte)0);
                                        fSprite1.setSpritePosition(
                                                        this.unitCharsSprites[kIt].posXPixel
                                                                + sub_1673(fSprite1, 14),
                                                        this.unitCharsSprites[kIt].posYPixel
                                                                + this.unitCharsSprites[kIt].frameHeight);
                                        fSprite1.someYVal1 = (4 - this.unitCharsSprites[kIt].frameHeight);
                                        this.m_game.addSpriteTo(fSprite1);
                                    }
                                    else if (this.unitType == 5)
                                    {
                                        fSprite1 = F_Sprite.someSpriteCopy(
                                                this.m_game.redsparkSprite, 0, 0, 0, 1, 50,
                                                (byte)0);
                                        fSprite1.setSpritePosition(
                                                        this.unitCharsSprites[kIt].posXPixel
                                                                + sub_1673(
                                                                        fSprite1,
                                                                        this.var_b8d.frameWidth * 3 / 4),
                                                        this.unitCharsSprites[kIt].posYPixel
                                                                + this.unitCharsSprites[kIt].frameHeight);
                                        fSprite1.someYVal1 = (-fSprite1.frameHeight);
                                        this.m_game.addSpriteTo(fSprite1);
                                    }
                                }
                            }
                        }
                        if (j != 0)
                        {
                            this.var_c4d = 0;
                            this.var_b7d = 6;
                            this.otherFightAnim.sub_3363();
                            this.m_game.startShakingScreen(200);
                            E_MainCanvas.vibrate(200);
                            E_MainCanvas.playMusicLooped(14, 1);
                            this.someStartTime7 = this.m_game.time;
                        }
                        break;
                    case 6:
                        if (((this.unitType == 10) && (this.m_game.time
                                - this.someStartTime7 >= 400L))
                                || (((this.unitType == 0) || (this.unitType == 2) || (this.unitType == 5)) && (this.m_game.time
                                        - this.someStartTime7 >= 50L)))
                        {
                            this.var_b7d = 4;
                        }
                        break;
                    case 4:
                        j = 1;
                        if (this.var_c4d < this.unitCharsCount)
                        {
                            if ((this.unitType == 0) || (this.unitType == 2)
                                    || (this.unitType == 5))
                            {
                                this.unitCharsSprites[this.var_c4d].someYVal3 = -6;
                            }
                            else if (this.unitType == 10)
                            {
                                this.unitCharsSprites[this.var_c4d].var_87c = 0;
                            }
                            if (this.unitType == 5)
                            {
                                this.unitCharsSprites[this.var_c4d].someXVal3 = (-2 * var_b85[this.var_b6d]);
                            }
                            else
                            {
                                this.unitCharsSprites[this.var_c4d].someXVal3 = (-var_b85[this.var_b6d]);
                            }
                            this.unitCharsSprites[this.var_c4d].startAnimation(3, this.var_b75);
                            this.var_c4d += 1;
                            j = 0;
                        }
                        for (k = 0; k < this.var_c4d; k++)
                        {
                            if (this.unitType == 10)
                            {
                                if (this.unitCharsSprites[k].var_87c != -1)
                                {
                                    this.unitCharsSprites[k].var_87c += 1;
                                    if (this.unitCharsSprites[k].var_87c >= 16)
                                    {
                                        this.unitCharsSprites[k].startAnimation(0, this.var_b75);
                                        this.unitCharsSprites[k].someXVal3 = 0;
                                        this.unitCharsSprites[k].var_87c = -1;
                                    }
                                    else
                                    {
                                        j = 0;
                                    }
                                }
                            }
                            else if (this.unitCharsSprites[k].someYVal1 < 0)
                            {
                                this.unitCharsSprites[k].someYVal3 += 1;
                                j = 0;
                            }
                            else if (this.unitCharsSprites[k].someYVal3 >= 6)
                            {
                                this.unitCharsSprites[k].someYVal3 = 0;
                                this.unitCharsSprites[k].someXVal3 = 0;
                                this.unitCharsSprites[k].someYVal1 = 0;
                                this.unitCharsSprites[k].startAnimation(0, this.var_b75);
                            }
                        }
                        if (j != 0)
                        {
                            this.var_b35 = true;
                            this.var_b7d = 0;
                            this.someStartTime7 = this.m_game.time;
                        }
                        break;
                }
            }
            if (this.unitType == 6)
            { //golem
                for (i = 0; i < this.unitCharsSprites.Length; i++)
                {
                    if (this.unitCharsSprites[i].m_isRotating)
                    {
                        this.unitCharsSprites[i].someYVal1 = (-6 + 4
                                * A_MenuBase.getSin1024(this.unitsRotationAngles[i]) >> 10);
                        this.unitsRotationAngles[i] = ((this.unitsRotationAngles[i] + 10) % 360);
                    }
                }
            }
        }

        public sealed override void sub_3363()
        {
            this.var_b3d = true;
            this.var_c25 = (this.unitStartCharsCount - this.unitChars3);
            this.unitCharsCount = this.unitChars3;
            int k;
            for (int i = 0; i < this.var_c25; i++)
            {
                this.m_game.removeSpriteFrom(this.unitCharsSprites[i]);
                F_Sprite redSparksSprite = F_Sprite.someSpriteCopy(this.m_game.redsparkSprite, 0,
                        0, 0, 1, 0, (byte)0);
                redSparksSprite.setSpritePosition(
                                this.unitCharsSprites[i].posXPixel
                                        + (this.unitCharsSprites[i].frameWidth - redSparksSprite.frameWidth)
                                        / 2, this.m_game.someCanHeight);
                redSparksSprite.someYVal1 = (this.unitCharsSprites[i].posYPixel
                        + (this.unitCharsSprites[i].frameHeight - redSparksSprite.frameHeight) / 2 - this.m_game.someCanHeight);
                this.m_game.addSpriteTo(redSparksSprite);
                F_Sprite sprite1;
                for (k = 0; k < 3; k++)
                {
                    sprite1 = F_Sprite.someSpriteCopy(
                            this.m_game.bigSmokeSprite, -1 + k, 0,
                            E_MainCanvas.getRandomWithin(-4, -1), 1,
                            E_MainCanvas.getRandomMax(4) * 50, (byte)0);
                    sprite1.setSpritePosition(
                                    this.unitCharsSprites[i].posXPixel
                                            + (this.unitCharsSprites[i].frameWidth - sprite1.frameWidth)
                                            / 2, this.unitCharsSprites[i].posYPixel
                                            + this.unitCharsSprites[i].frameHeight
                                            - sprite1.frameHeight + 3);
                    this.m_game.addSpriteTo(sprite1);
                }
                sprite1 = F_Sprite.someSpriteCopy(this.m_game.smokeSprite, 0,
                        0, -1, 1, 200, (byte)0);
                sprite1.setSpritePosition(
                                this.unitCharsSprites[i].posXPixel
                                        + (this.unitCharsSprites[i].frameWidth - sprite1.frameWidth)
                                        / 2, this.unitCharsSprites[i].posYPixel
                                        + this.unitCharsSprites[i].frameHeight
                                        - sprite1.frameHeight + 3);
                this.m_game.addSpriteTo(sprite1);
            }
            F_Sprite[] someSprites = new F_Sprite[this.unitCharsCount];
            System.Array.Copy(this.unitCharsSprites, this.var_c25, someSprites, 0,
                    this.unitCharsCount);
            this.unitCharsSprites = someSprites;
            int damageDone = this.unitHealth3 - this.unitStartHealth;
            F_Sprite someSprite = F_Sprite.createBouncingText("" + damageDone, 0, -4, (byte)1);
            int j;
            if (this.unitChars3 == 1)
            {
                j = this.unitCharsSprites[0].posXPixel + this.unitCharsSprites[0].frameWidth / 2;
                k = this.unitCharsSprites[0].posYPixel + this.unitCharsSprites[0].frameHeight + 1;
            }
            else
            {
                j = this.m_game.viewportWidth / 2;
                if (this.var_b6d == 1)
                {
                    j += this.m_game.viewportWidth;
                }
                k = (this.m_game.var_3bfb + this.var_bfd) / 2;
            }
            someSprite.setSpritePosition(j, k);
            someSprite.var_85c = true;
            this.m_game.addSpriteTo(someSprite);
        }

        public sealed override void sub_35fd(Graphics gr, int inX, int inY)
        {
            gr.translate(inX, inY);
            int x = 0;
            int it = 0;
            int Length = this.var_c05;
            while (it < Length)
            {
                int y = this.var_bfd + inY;
                int n = 0;
                int i1 = this.var_c0d;
                while (n < i1)
                {
                    this.multipleBgImages[this.var_c5d[it][n]].drawImageExt(gr, x, y);
                    y += 24;
                    n++;
                }
                x += 24;
                it++;
            }
            if (this.var_be5 != null)
            {
                int imWidth = this.var_be5.imageWidth;
                int vX = 0;
                int it2 = 0;
                int Length2 = this.m_game.viewportWidth / imWidth;
                while (it2 < Length2)
                {
                    this.var_be5.drawImageExt(gr, vX, 0);
                    vX += imWidth;
                    it2++;
                }
            }
            gr.translate(-inX, -inY);
        }

        public sealed override void drawUnitHealth(Graphics gr)
        {
            int hY = this.m_game.someCanHeight - I_Game.someUnkHeight1 / 2;
            E_MainCanvas.drawCharedString(gr, this.unitHealth + "/" + 100,
                    this.m_game.viewportWidth / 2, hY, 1, 3);
        }

        public sealed override void sub_3788(Graphics gr)
        {
            gr.setColor(4210752);
            for (int i = 0; i < this.unitCharsCount; i++)
            {
                F_Sprite sprite = this.unitCharsSprites[i];
                if ((this.unitType == 0) || (this.unitType == 4)
                        || (this.unitType == 5) || (this.unitType == 6))
                {
                    gr.fillArc(sprite.posXPixel,
                            sprite.posYPixel + sprite.frameHeight * 4
                                    / 5, sprite.frameWidth,
                            sprite.frameHeight / 4, 0, 360);
                }
            }
        }

    }

}