using javax.microedition.lcdui;
using java.util;
using java.lang;

namespace aeii
{

    public  class D_Menu : A_MenuBase
    {

        public bool[] menuActionsMb = { false, false };
        public int m_bgColorMb = 13553358; // #CECECE light gray
        public static  int someXPadding = E_MainCanvas.canvasHeight <= 143 ? 1 : 2;
        public static  int var_fcd = someXPadding * 2 + 1;
        public byte var_fd5 = 2;
        public short var_fdd = 3;
        public static I_Game gameVar;
        public String[] menuItemsNamesMb;
        public H_ImageExt[] menuItemsImages;
        public int menuLocX;
        public int menuLocY;
        public int menuWidth;
        public int menuHeight;
        public int var_101d;
        public Font var_1025 = E_MainCanvas.font8;
        public int activeItemPositionMb;
        public int menuItemsCount;
        public int var_103d;
        public int var_1045;
        public int var_104d;
        public int var_1055;
        public byte menuType;
        public int someBorderMb;
        public bool var_106d = false;
        public bool var_1075 = false;
        public C_Unit[] buyUnits;
        public int var_1085;
        public int var_108d;
        public sbyte portraitSpriteIndex = -1;
        public int var_109d;
        public int var_10a5;
        public int unitPortraitWidth;
        public int var_10b5;
        public String[] wrappedHeaderMb;
        public int var_10c5 = -1;
        public bool var_10cd = true;
        public int var_10d5;
        public C_Unit menuUnit;
        public bool var_10e5 = true;
        public A_MenuBase parentMenu;
        public int var_10f5;
        public F_Sprite[] smallSparksMenuSprites;
        public int var_1105;
        public bool var_110d = false;
        public Vector childrenMenuList;
        public int var_111d = -1;
        public bool var_1125;
        public bool updateAllChildrenBoolMb;
        public int var_1135;
        public int var_113d;
        public int var_1145;
        public int var_114d;
        public byte[][] mapTilesData;
        public Vector mapUnitsList;
        public int var_1165;
        public int var_116d;
        public int titleGradientColor = 2370117;
        public int var_117d = 2370117;
        public H_ImageExt menuTitleIcon;
        public D_Menu somedescMenu;
        public int[] var_1195;
        public int var_119d;
        public int var_11a5;
        public int[] var_11ad;
        public int var_11b5;
        public int var_11bd = -1;
        public short[] wheelItemDegree;
        public int var_11cd;
        public int var_11d5;
        public int wheelMenuRadius;
        public int var_11e5;
        public int var_11ed;
        public int wheelSectorDegree;
        public int var_11fd;
        public int var_1205;
        public int menuItemFrameWidthMb;
        public int var_1215;
        public byte var_121d;
        public F_Sprite wheelItemBgImage;

        public D_Menu(byte inMenuType, int paramInt)
        {
            this.menuType = inMenuType;
            this.someBorderMb = paramInt;
            if (inMenuType == 15)
            {
                this.var_11b5 = (gameVar.someGHeight - gameVar.buttonsSprite.frameHeight);
                this.var_10cd = true;
            }
            else if ((inMenuType == 0) || (inMenuType == 11))
            {
                this.menuActionsMb[0] = true;
                this.menuActionsMb[1] = true;
            }
            else if (inMenuType == 3)
            {
                this.wheelItemBgImage = gameVar.bigCircleSprite;
                createMenuItemSparks();
                this.var_10cd = false;
                this.var_1125 = true;
                this.var_1085 = (E_MainCanvas.someMenuShiftHeight - E_MainCanvas.font8BaselinePos);
                this.menuWidth = gameVar.someGWidth;
                this.menuHeight = (gameVar.bigCircleSprite.frameHeight + var_fcd);
                if ((paramInt & 0x2) == 0)
                {
                    this.menuHeight += 5;
                }
                this.buyUnits = C_Unit.getSomUnitsList(gameVar.playerId);
                this.menuItemsCount = this.buyUnits.Length;
                int j = this.menuWidth - gameVar.sideArrowSprite.frameWidth * 2;
                if ((paramInt & 0x4) == 0)
                {
                    j -= 8;
                }
                if ((paramInt & 0x8) == 0)
                {
                    j -= 8;
                }
                this.var_10a5 = (j / (gameVar.bigCircleSprite.frameWidth + 3));
                if (this.var_10a5 > this.menuItemsCount)
                {
                    this.var_10a5 = this.menuItemsCount;
                }
                this.var_101d = (j / this.var_10a5);
                this.var_10f5 = ((j - this.var_101d * this.var_10a5) / 2);
                this.var_fd5 = 2;
            }
            else if ((inMenuType == 2) || (inMenuType == 5))
            {
                this.var_10cd = false;
                this.menuHeight = (5 + someXPadding + 24 + var_fcd
                        + gameVar.smallCircleSprite.frameHeight * 2 + someXPadding
                        + someXPadding + 1);
                if (inMenuType == 5)
                {
                    this.menuHeight += var_fcd + E_MainCanvas.font8BaselinePos;
                    this.menuUnit = gameVar.getSomeUnit(gameVar.someCursorXPos,
                            gameVar.someCursorYPos, (byte)0);
                    this.activeItemPositionMb = this.menuUnit.unitTypeId;
                    this.menuWidth = gameVar.someGWidth;
                }
                else
                {
                    this.menuWidth = gameVar.someGWidth;
                }
            }
            else if ((inMenuType != 7) && (inMenuType == 8))
            {
                this.var_fdd = 8;
                this.menuActionsMb[0] = true;
            }
            this.var_106d = true;
        }

        public override void onLoad()
        {
            initMenu();
        }

        //this is on load
        public  void initMenu()
        {
            this.var_1205 = 0;
            if (this.smallSparksMenuSprites != null)
            {
                initMenuItemSparks();
            }
            this.var_1105 = 4;
            this.var_106d = true;
            this.var_1075 = true;
            if (gameVar != null)
            {
                gameVar.sub_4d3f();
            }
            if (this.somedescMenu != null)
            {
                this.somedescMenu.var_106d = true;
            }
            if (this.menuType == 15)
            {
                for (int i = 0; i < this.childrenMenuList.size(); i++)
                {
                    D_Menu childMenu = (D_Menu)this.childrenMenuList.elementAt(i);
                    childMenu.initMenu();
                    childMenu.var_1075 = false;
                }
            }
        }

        /***
         * index == 0 - left action index == 2 - right action
         * 
         * @param index
         * @param isEnabled
         */
        public  void setMenuActionEnabled(byte index, bool isEnabled)
        {
            this.menuActionsMb[index] = isEnabled;
        }

        public  void setParentMenu(A_MenuBase parMenu)
        {
            this.parentMenu = parMenu;
            // return to parent action
            this.menuActionsMb[1] = (parMenu != null ? true : false);
        }

        public  D_Menu createTitleMenu(String header)
        {
            this.somedescMenu = new D_Menu((byte)10, 0);
            this.somedescMenu.createDescDialogMb(null, header, gameVar.someGWidth,
                    -1);
            return this.somedescMenu;
        }

        public  void addChildMenu(D_Menu childMenu, int locX, int locY,
                int paramInt3)
        {
            if (this.childrenMenuList == null)
            {
                this.childrenMenuList = new Vector();
            }
            if (this.var_11ad == null)
            {
                this.var_11ad = new int[5];
                for (int i = 0; i < 5; i++)
                {
                    this.var_11ad[i] = this.var_11b5;
                    if (i > 0)
                    {
                        this.var_11ad[i] -= gameVar.buttonsSprite.frameHeight;
                    }
                }
            }
            childMenu.setMenuLoc(locX, locY, paramInt3);
            int sY = childMenu.menuLocY;
            for (int j = 0; j < 5; j++)
            {
                if (sY < this.var_11ad[j])
                {
                    if (sY + childMenu.menuHeight <= this.var_11ad[j])
                    {
                        break;
                    }
                    this.var_11ad[j] = sY;
                    if (j + 1 <= this.var_11a5)
                    {
                        break;
                    }
                    this.var_11a5 = (j + 1);
                    break;
                }
                sY -= this.var_11ad[j];
            }
            childMenu.setMenuActionEnabled((byte)0, false);
            childMenu.setMenuActionEnabled((byte)1, false);
            this.childrenMenuList.addElement(childMenu);
        }

        public  void initMapPreviewMenu(int inWidth, int inHeight,
                byte[][] mapData, Vector unitsMb)
        {
            this.someBorderMb = 15;
            this.mapTilesData = mapData;
            this.mapUnitsList = unitsMb;
            this.var_fdd = 8;
            this.menuActionsMb[0] = true;
            this.var_10cd = true;
            this.var_1165 = mapData.Length;
            this.var_116d = mapData[0].Length;
            this.menuWidth = (this.var_1165
                    * gameVar.smallTilesImages[0].imageWidth + 8);
            this.menuHeight = (this.var_116d
                    * gameVar.smallTilesImages[0].imageHeight + 8);
            int j;
            if (this.menuWidth > inWidth)
            {
                j = gameVar.smallTilesImages[0].imageWidth;
                this.var_1145 = ((inWidth - 8) / j);
                this.menuWidth = (j * this.var_1145 + 8);
            }
            else
            {
                this.var_1145 = this.var_1165;
            }
            if (this.menuHeight > inHeight)
            {
                j = gameVar.smallTilesImages[0].imageHeight;
                this.var_114d = ((inHeight - 8) / j);
                this.menuHeight = (j * this.var_114d + 8);
            }
            else
            {
                this.var_114d = this.var_116d;
            }
            this.menuType = 8;
        }

        public  void setMenuLoc(int inX, int inY, int paramInt3)
        {
            this.menuLocX = inX;
            this.menuLocY = inY;
            if ((paramInt3 & 0x1) != 0)
            {
                this.menuLocX -= (this.menuWidth >> 1);
            }
            else if ((paramInt3 & 0x8) != 0)
            {
                this.menuLocX -= this.menuWidth;
            }
            if ((paramInt3 & 0x2) != 0)
            {
                this.menuLocY -= (this.menuHeight >> 1);
            }
            else if ((paramInt3 & 0x20) != 0)
            {
                this.menuLocY -= this.menuHeight;
            }
            this.var_104d = this.menuLocX;
            this.var_1055 = this.menuLocY;
        }

        public  void initPortraitDialog(String msg, int inW,
                int inH, sbyte portraintIndex, byte unused)
        {
            this.portraitSpriteIndex = portraintIndex;
            if (portraintIndex == -1)
            {
                this.someBorderMb = 14;
            }
            else
            {
                this.unitPortraitWidth = (gameVar.portraitsSprite.frameWidth - 8);
            }
            int textWidth = inW - this.unitPortraitWidth - 16;
            this.menuItemsNamesMb = A_MenuBase.wrapText(msg, textWidth, E_MainCanvas.font8);
            sub_1a9a(null, this.menuItemsNamesMb, inW, inH);
            this.var_110d = false;
            this.menuType = 7;
        }

        public  void sub_1a9a(String header, String[] strLines,
                int paramInt1, int inHeight)
        {
            this.var_10cd = false;
            this.menuWidth = paramInt1;
            this.menuHeight = inHeight;
            this.menuItemsCount = strLines.Length;
            this.activeItemPositionMb = 0;
            this.var_109d = 0;
            this.var_10b5 = 0;
            this.var_110d = false;
            int i = paramInt1 - this.unitPortraitWidth - 16;
            if (header != null)
            {
                this.wrappedHeaderMb = A_MenuBase.wrapText(header, i, E_MainCanvas.font8);
            }
            this.menuItemsNamesMb = strLines;
            this.var_101d = E_MainCanvas.someMenuShiftHeight;
            this.var_1085 = (E_MainCanvas.someMenuShiftHeight - E_MainCanvas.font8BaselinePos);
            this.var_108d = (this.var_1085 / 2);
            int sY;
            if (inHeight <= 0)
            {
                sY = this.someCanHeight;
            }
            else
            {
                sY = inHeight;
            }
            if ((this.someBorderMb & 0x1) == 0)
            {
                sY -= 5;
            }
            if ((this.someBorderMb & 0x2) == 0)
            {
                sY -= 5;
            }
            if (header != null)
            {
                sY -= this.wrappedHeaderMb.Length * this.var_101d;
            }
            this.var_10a5 = ((sY - 2) / this.var_101d);
            if (this.var_10a5 > this.menuItemsNamesMb.Length)
            {
                this.var_10a5 = this.menuItemsNamesMb.Length;
            }
            else if (this.var_10a5 < this.menuItemsNamesMb.Length)
            {
                this.var_110d = true;
            }
            if (inHeight < 0)
            {
                if (this.wrappedHeaderMb != null)
                {
                    this.menuHeight = (this.wrappedHeaderMb.Length * this.var_101d);
                }
                this.menuHeight += this.var_10a5 * this.var_101d;
                if ((this.someBorderMb & 0x1) == 0)
                {
                    this.menuHeight += 5;
                }
                if ((this.someBorderMb & 0x2) == 0)
                {
                    this.menuHeight += 5;
                }
            }
            else
            {
                this.var_10f5 = ((sY - this.var_10a5 * this.var_101d) / 2);
            }
            this.menuType = 10;
            this.var_fd5 = 2;
        }

        public  void createDescDialogMb(String paramString1,
                String paramString2, int paramInt1, int paramInt2)
        {
            int i = paramInt1 - this.unitPortraitWidth;
            if ((this.someBorderMb & 0x4) == 0)
            {
                i -= 8;
            }
            if ((this.someBorderMb & 0x8) == 0)
            {
                i -= 8;
            }
            this.menuItemsNamesMb = A_MenuBase.wrapText(paramString2, i,
                    E_MainCanvas.font8);
            sub_1a9a(paramString1, this.menuItemsNamesMb, paramInt1, paramInt2);
            if (this.var_110d)
            {
                i -= gameVar.arrowSprite.frameWidth;
                this.menuItemsNamesMb = A_MenuBase.wrapText(paramString2, i,
                        E_MainCanvas.font8);
                sub_1a9a(paramString1, this.menuItemsNamesMb, paramInt1, paramInt2);
            }
        }

        private  void createMenuItemSparks()
        {
            this.smallSparksMenuSprites = new F_Sprite[3];
            for (int i = 0; i < this.smallSparksMenuSprites.Length; i++)
            {
                this.smallSparksMenuSprites[i] = new F_Sprite(gameVar.smallSparkSprite);
            }
            initMenuItemSparks();
        }

        public  void initMenuItemSparks()
        {
            for (int i = 0; i < this.smallSparksMenuSprites.Length; i++)
            {
                this.smallSparksMenuSprites[i].var_7ec = true;
                this.smallSparksMenuSprites[i]
                        .setSpritePosition(
                                E_MainCanvas
                                        .getRandomMax(this.wheelItemBgImage.frameWidth),
                                E_MainCanvas
                                        .getRandomMax(this.wheelItemBgImage.frameHeight));
                this.smallSparksMenuSprites[i].setCurrentFrameIndex(E_MainCanvas
                        .getRandomMax(this.smallSparksMenuSprites[i].getFramesCount()));
            }
        }

        public  void sub_1e97(String[] names, H_ImageExt[] images,
                int paramInt1, int paramInt2, int paramInt3)
        {
            this.someBorderMb = 15;
            this.menuItemsNamesMb = names;
            this.menuItemsImages = images;
            this.menuItemsCount = this.menuItemsNamesMb.Length;
            this.menuWidth = 0;
            for (int i = 0; i < this.menuItemsCount; i++)
            {
                int itemStrWidth = E_MainCanvas.font8
                        .stringWidth(this.menuItemsNamesMb[i]);
                if (itemStrWidth > this.menuWidth)
                {
                    this.menuWidth = itemStrWidth;
                }
            }
            this.var_1085 = (E_MainCanvas.someMenuShiftHeight - E_MainCanvas.font8BaselinePos);
            this.var_108d = (this.var_1085 / 2);
            this.menuItemFrameWidthMb = gameVar.smallCircleSprite.frameWidth;
            this.var_101d = (this.menuItemFrameWidthMb + this.var_1085);
            this.menuWidth += this.menuItemsCount * this.var_101d;
            this.menuWidth += 32;
            if (this.menuWidth > this.someCanWidth)
            {
                this.menuWidth = this.someCanWidth;
            }
            this.menuHeight = this.menuItemFrameWidthMb;
            setMenuLoc(paramInt1, paramInt2, paramInt3);
            this.menuType = 13;
            this.var_fd5 = 2;
        }

        public  void setMenuItemsNames(String[] names, int inWidth,
                int inHeight)
        {
            this.menuItemsNamesMb = names;
            this.menuItemsCount = this.menuItemsNamesMb.Length;
            this.var_1125 = true;
            this.var_10cd = false;
            this.menuHeight = inHeight;
            int maxStrWidth = 0;
            for (int j = 0; j < this.menuItemsNamesMb.Length; j++)
            {
                int strWidth = E_MainCanvas.font8
                        .stringWidth(this.menuItemsNamesMb[j]);
                if (strWidth > maxStrWidth)
                {
                    maxStrWidth = strWidth;
                }
            }
            this.menuWidth = (maxStrWidth + 16 + gameVar.sideArrowSprite.frameWidth * 2);
            if (this.menuWidth < inWidth)
            {
                this.menuWidth = inWidth;
            }
            if (this.menuHeight < 0)
            {
                this.menuHeight = E_MainCanvas.someMenuShiftHeight;
                if (gameVar.sideArrowSprite.frameHeight > this.menuHeight)
                {
                    this.menuHeight = gameVar.sideArrowSprite.frameHeight;
                }
                if ((this.someBorderMb & 0x1) == 0)
                {
                    this.menuHeight += 5;
                }
                if ((this.someBorderMb & 0x2) == 0)
                {
                    this.menuHeight += 5;
                }
            }
            this.menuType = 14;
            this.var_fd5 = 2;
        }

        public  void createMenuListItems(String[] itemNames, int paramInt1,
                int paramInt2, int paramInt3, int paramInt4, int paramInt5,
                int paramInt6)
        {
            this.menuItemsNamesMb = itemNames;
            this.menuItemsCount = this.menuItemsNamesMb.Length;
            this.var_1085 = (E_MainCanvas.someMenuShiftHeight - E_MainCanvas.font8BaselinePos);
            this.var_101d = E_MainCanvas.someMenuShiftHeight;
            int maxStrWidth = 0;
            for (int j = 0; j < this.menuItemsNamesMb.Length; j++)
            {
                int strWid = E_MainCanvas.font8
                        .stringWidth(this.menuItemsNamesMb[j]);
                if (strWid > maxStrWidth)
                {
                    maxStrWidth = strWid;
                }
            }
            this.menuWidth = (maxStrWidth + 4 + 16);
            if (this.menuWidth > this.someCanWidth)
            {
                this.menuWidth = this.someCanWidth;
            }
            else if (this.menuWidth < paramInt3)
            {
                if (paramInt6 == 4)
                {
                    this.var_11bd = ((paramInt3 - this.menuWidth) / 2);
                }
                this.menuWidth = paramInt3;
            }
            this.menuHeight = (this.var_101d * this.menuItemsNamesMb.Length
                    + this.var_1085 + 16);
            if (this.menuHeight > paramInt4)
            {
                this.menuHeight = paramInt4;
            }
            sub_1a9a(null, this.menuItemsNamesMb, this.menuWidth, this.menuHeight);
            if ((this.menuWidth < this.someCanWidth) && (this.var_110d))
            {
                this.menuWidth += gameVar.arrowSprite.frameWidth;
            }
            this.menuType = 11;
            setMenuLoc(paramInt1, paramInt2, paramInt5);
        }

        public  void initWheelMenu(String[] itemString,
                H_ImageExt[] itemImages, int paramInt1, int inX, int inY,
                int paramInt4, byte someType)
        {
            this.var_121d = someType;
            this.menuItemsNamesMb = itemString;
            this.menuItemsImages = itemImages;
            this.menuItemsCount = this.menuItemsNamesMb.Length;
            if (someType == 1)
            {
                this.wheelItemBgImage = gameVar.bigCircleSprite;
            }
            else if (someType == 2)
            {
                this.wheelItemBgImage = gameVar.smallCircleSprite;
                if (this.menuItemsCount < 4)
                {
                    String[] names = new String[4];
                    JavaSystem.arraycopy(this.menuItemsNamesMb, 0, names, 0,
                            this.menuItemsCount);
                    this.menuItemsNamesMb = names;
                    this.menuItemsCount = 4;
                }
            }
            this.var_1085 = (E_MainCanvas.someMenuShiftHeight - E_MainCanvas.font8BaselinePos);
            this.someBorderMb = 15;
            this.menuItemFrameWidthMb = this.wheelItemBgImage.frameWidth;
            this.var_1215 = (this.menuItemFrameWidthMb >> 1);
            createMenuItemSparks();
            this.wheelItemDegree = new short[this.menuItemsCount];
            this.wheelSectorDegree = (360 / this.menuItemsCount);
            this.var_11ed = (this.wheelSectorDegree / 2);
            this.var_11e5 = this.var_11ed;
            for (int i = 0; i < this.menuItemsCount; i++)
            {
                this.wheelItemDegree[i] = ((short)(this.wheelSectorDegree * i));
            }
            if (this.menuItemsCount == 1)
            {
                this.var_11d5 = 0;
            }
            else if (inY <= 0)
            {
                this.var_11d5 = ((this.wheelItemBgImage.frameWidth << 10) / (2 * A_MenuBase
                        .getSin1024(45)));
                this.wheelMenuRadius = (this.var_11d5 + this.wheelItemBgImage.frameWidth / 2);
                inY = this.wheelMenuRadius * 2 + E_MainCanvas.someMenuShiftHeight + 2;
            }
            else
            {
                int i = (this.wheelItemBgImage.frameWidth << 10)
                        / A_MenuBase.getSin1024(this.wheelSectorDegree / 2)
                        + this.wheelItemBgImage.frameHeight / 2;
                this.wheelMenuRadius = ((inY - E_MainCanvas.someMenuShiftHeight) / 2 - 2);
                if (this.wheelMenuRadius > i)
                {
                    this.wheelMenuRadius = i;
                }
                this.var_11d5 = (this.wheelMenuRadius - this.wheelItemBgImage.frameHeight / 2);
            }
            this.var_11cd = 0;
            this.menuWidth = (this.wheelMenuRadius * 2);
            this.menuHeight = inY;
            this.var_fd5 = 0;
            setMenuLoc(paramInt1, inX, paramInt4);
        }

        public  int sub_254b(int step)
        {
            int i = this.var_111d;
            int j = i;
            int countItems = this.childrenMenuList.size();
            do
            {
                i += step;
                if (i < 0)
                {
                    i = countItems - 1;
                }
                else if (i >= this.childrenMenuList.size())
                {
                    if (j < 0)
                    {
                        return -1;
                    }
                    i = 0;
                }
            } while (!((D_Menu)this.childrenMenuList.elementAt(i)).var_1125);
            return i;
        }

        // @Override
        public override void onUpdate()
        {
            updateMenu(true);
        }

        public  void updateMenu(bool paramBoolean)
        {
            if (this.var_fd5 == 3)
            {
                return;
            }
            if ((this.menuType == 10) && (this.var_10c5 > 0)
                    && (this.var_10c5 <= 250))
            {
                this.var_1105 += 1;
                this.var_106d = true;
            }
            else if (this.var_1105 > 0)
            {
                this.var_1105 -= 1;
                this.var_106d = true;
            }
            int i;
            if ((paramBoolean) && (this.var_fd5 == 2))
            {
                i = 0;
                if ((this.menuActionsMb[0] != false)
                        && ((A_MenuBase.mainCanvas
                                .invertActionCode(I_Game.m_actionApply)) || (A_MenuBase.mainCanvas
                                .invertActionCode(16))))
                {
                    i = 1;
                    A_MenuBase.mainCanvas.clearActionCode(I_Game.m_actionApply);
                    A_MenuBase.mainCanvas.clearActionCode(16);
                }
                if ((this.menuType == 0) || (this.menuType == 3))
                {
                    for (int j = 0; j < this.smallSparksMenuSprites.Length; j++)
                    {
                        if (this.smallSparksMenuSprites[j].currentFrameIndex == this.smallSparksMenuSprites[j]
                                .getFramesCount() - 1)
                        {
                            if (this.var_1205 == 0)
                            {
                                this.smallSparksMenuSprites[j]
                                        .setSpritePosition(
                                                E_MainCanvas
                                                        .getRandomMax(this.wheelItemBgImage.frameWidth
                                                                - this.smallSparksMenuSprites[j].frameWidth),
                                                E_MainCanvas
                                                        .getRandomMax(this.wheelItemBgImage.frameHeight
                                                                - this.smallSparksMenuSprites[j].frameHeight));
                            }
                            else
                            {
                                this.smallSparksMenuSprites[j].var_7ec = false;
                            }
                        }
                        this.smallSparksMenuSprites[j].nextFrame();
                    }
                    this.var_106d = true;
                }
                int k;
                if (this.menuType == 15)
                {
                    if ((!this.updateAllChildrenBoolMb) && (this.var_111d >= 0))
                    {
                        D_Menu someMenu;
                        if (A_MenuBase.mainCanvas.invertActionCode(1))
                        {
                            ((D_Menu)this.childrenMenuList
                                    .elementAt(this.var_111d)).var_106d = true;
                            this.var_111d = sub_254b(-1);
                            someMenu = (D_Menu)this.childrenMenuList.elementAt(this.var_111d);
                            someMenu.var_106d = true;
                            int m = someMenu.menuLocY;
                            for (int n = 0; n < 5; n++)
                            {
                                if (m < this.var_11ad[n])
                                {
                                    if (this.var_119d == n)
                                    {
                                        break;
                                    }
                                    initMenu();
                                    this.var_119d = n;
                                    break;
                                }
                                m -= this.var_11ad[n];
                            }
                        }
                        if (A_MenuBase.mainCanvas.invertActionCode(2))
                        {
                            D_Menu dmenu = (D_Menu)this.childrenMenuList.elementAt(this.var_111d);
                            dmenu.var_106d = true;
                            this.var_111d = sub_254b(1);
                            someMenu = (D_Menu)this.childrenMenuList.elementAt(this.var_111d);
                            someMenu.var_106d = true;
                            int m = someMenu.menuLocY;
                            for (int n = 0; n < 5; n++)
                            {
                                if (m < this.var_11ad[n])
                                {
                                    if (this.var_119d == n)
                                    {
                                        break;
                                    }
                                    initMenu();
                                    this.var_119d = n;
                                    break;
                                }
                                m -= this.var_11ad[n];
                            }
                        }
                    }
                    if (i != 0)
                    {
                        gameVar.sub_6260(this, this.var_111d, "", (byte)0);
                        return;
                    }
                    for (k = 0; k < this.childrenMenuList.size(); k++)
                    {
                        D_Menu childMenu = (D_Menu)this.childrenMenuList
                                .elementAt(k);
                        if (this.updateAllChildrenBoolMb)
                        {
                            childMenu.updateMenu(true);
                        }
                        else
                        {
                            childMenu.updateMenu(k == this.var_111d);
                        }
                    }
                    this.var_106d = true;
                }
                else if (this.menuType == 0)
                {
                    if (this.var_fd5 == 2)
                    {
                        this.var_106d = true;
                        if (A_MenuBase.mainCanvas.invertActionCode(4))
                        {
                            this.var_1205 -= this.wheelSectorDegree;
                            this.var_11fd += this.wheelSectorDegree;
                            this.activeItemPositionMb -= 1;
                            if (this.activeItemPositionMb < 0)
                            {
                                this.activeItemPositionMb = (this.menuItemsCount - 1);
                            }
                        }
                        else if (A_MenuBase.mainCanvas.invertActionCode(8))
                        {
                            this.var_1205 += this.wheelSectorDegree;
                            this.var_11fd -= this.wheelSectorDegree;
                            if (this.var_11fd < 0)
                            {
                                this.var_11fd += 360;
                            }
                            this.activeItemPositionMb += 1;
                            if (this.activeItemPositionMb >= this.menuItemsCount)
                            {
                                this.activeItemPositionMb = 0;
                            }
                        }
                        if (this.var_1205 != 0)
                        {
                            if ((k = -this.var_1205 / 2) == 0)
                            {
                                this.var_1205 = 0;
                            }
                            else
                            {
                                this.var_1205 += k;
                            }
                            if (this.var_1205 == 0)
                            {
                                initMenuItemSparks();
                            }
                        }
                        else if (A_MenuBase.mainCanvas.isActionLongPressed(4))
                        {
                            this.var_1205 -= this.wheelSectorDegree;
                            this.var_11fd += this.wheelSectorDegree;
                            this.activeItemPositionMb -= 1;
                            if (this.activeItemPositionMb < 0)
                            {
                                this.activeItemPositionMb = (this.menuItemsCount - 1);
                            }
                        }
                        else if (A_MenuBase.mainCanvas.isActionLongPressed(8))
                        {
                            this.var_1205 += this.wheelSectorDegree;
                            this.var_11fd -= this.wheelSectorDegree;
                            if (this.var_11fd < 0)
                            {
                                this.var_11fd += 360;
                            }
                            this.activeItemPositionMb += 1;
                            if (this.activeItemPositionMb >= this.menuItemsCount)
                            {
                                this.activeItemPositionMb = 0;
                            }
                        }
                        if (i != 0)
                        {
                            gameVar.sub_6260(
                                    this,
                                    this.activeItemPositionMb,
                                    this.menuItemsNamesMb[this.activeItemPositionMb],
                                    (byte)0);
                        }
                    }
                }
                else if ((this.menuType == 13) || (this.menuType == 14))
                {
                    if (A_MenuBase.mainCanvas.invertActionCode(4))
                    {
                        this.activeItemPositionMb -= 1;
                        if (this.activeItemPositionMb < 0)
                        {
                            this.activeItemPositionMb = (this.menuItemsCount - 1);
                        }
                        if (this.menuType == 14)
                        {
                            gameVar.sub_6260(this, this.activeItemPositionMb, null,
                                    (byte)2);
                        }
                        this.var_106d = true;
                    }
                    else if (A_MenuBase.mainCanvas.invertActionCode(8))
                    {
                        this.activeItemPositionMb += 1;
                        if (this.activeItemPositionMb >= this.menuItemsCount)
                        {
                            this.activeItemPositionMb = 0;
                        }
                        if (this.menuType == 14)
                        {
                            gameVar.sub_6260(this, this.activeItemPositionMb, null,
                                    (byte)2);
                        }
                        this.var_106d = true;
                    }
                    else if (i != 0)
                    {
                        gameVar.sub_6260(this, this.activeItemPositionMb,
                                this.menuItemsNamesMb[this.activeItemPositionMb],
                                (byte)0);
                    }
                }
                else if (this.menuType == 3)
                {
                    if (this.var_10b5 != 0)
                    {
                        if (Math.abs(this.var_10b5) < 2)
                        {
                            this.var_10b5 = 0;
                        }
                        else
                        {
                            this.var_10b5 -= this.var_10b5 / 2;
                        }
                        this.var_106d = true;
                    }
                    if (i != 0)
                    {
                        gameVar.sub_6260(this, this.activeItemPositionMb,
                                this.menuItemsNamesMb[this.activeItemPositionMb],
                                (byte)0);
                        return;
                    }
                    if (A_MenuBase.mainCanvas.invertActionCode(4))
                    {
                        if (this.activeItemPositionMb < this.var_109d)
                        {
                            this.activeItemPositionMb += this.menuItemsCount;
                        }
                        this.activeItemPositionMb -= 1;
                        if (this.activeItemPositionMb < this.var_109d)
                        {
                            if (this.activeItemPositionMb < 0)
                            {
                                this.activeItemPositionMb += this.menuItemsCount;
                            }
                            this.var_109d = this.activeItemPositionMb;
                            this.var_10b5 = (-this.var_101d);
                        }
                        this.activeItemPositionMb %= this.menuItemsCount;
                        gameVar.sub_6260(this, this.activeItemPositionMb, null,
                                (byte)2);
                        initMenuItemSparks();
                        this.var_106d = true;
                    }
                    else if (A_MenuBase.mainCanvas.invertActionCode(8))
                    {
                        if (this.activeItemPositionMb < this.var_109d)
                        {
                            this.activeItemPositionMb += this.menuItemsCount;
                        }
                        this.activeItemPositionMb += 1;
                        if (this.activeItemPositionMb >= this.var_109d
                                + this.var_10a5)
                        {
                            this.var_10b5 = this.var_101d;
                            this.var_109d = ((this.var_109d + 1) % this.menuItemsCount);
                        }
                        this.activeItemPositionMb %= this.menuItemsCount;
                        gameVar.sub_6260(this, this.activeItemPositionMb, null,
                                (byte)3);
                        initMenuItemSparks();
                        this.var_106d = true;
                    }
                }
                else if ((this.menuType == 10) || (this.menuType == 7)
                      || (this.menuType == 11))
                {
                    if (this.var_10c5 != -1)
                    {
                        if (this.var_10c5 > 0)
                        {
                            this.var_10c5 -= 50;
                        }
                        else
                        {
                            A_MenuBase.mainCanvas.showMenu(this.parentMenu);
                        }
                    }
                    if (this.var_10b5 > 0)
                    {
                        this.var_10b5 -= this.var_101d / 3 + 1;
                        if (this.var_10b5 < 0)
                        {
                            this.var_10b5 = 0;
                        }
                        this.var_106d = true;
                    }
                    else if (this.var_10b5 < 0)
                    {
                        this.var_10b5 += this.var_101d / 3 + 1;
                        if (this.var_10b5 > 0)
                        {
                            this.var_10b5 = 0;
                        }
                        this.var_106d = true;
                    }
                    if (this.var_10b5 == 0)
                    {
                        if (((this.menuType == 11) || (this.menuType == 10))
                                && (i != 0))
                        {
                            gameVar.sub_6260(
                                    this,
                                    this.activeItemPositionMb,
                                    this.menuItemsNamesMb[this.activeItemPositionMb],
                                    (byte)0);
                            return;
                        }
                        if ((this.menuType != 7)
                                && (A_MenuBase.mainCanvas.invertActionCode(1)))
                        {
                            if (this.menuType == 11)
                            {
                                this.activeItemPositionMb -= 1;
                                if (this.activeItemPositionMb < 0)
                                {
                                    this.activeItemPositionMb = (this.menuItemsCount - 1);
                                    this.var_109d = (this.menuItemsCount - this.var_10a5);
                                    if (this.menuType == 3)
                                    {
                                        this.var_109d = this.activeItemPositionMb;
                                    }
                                }
                                else if (this.activeItemPositionMb < this.var_109d)
                                {
                                    this.var_10b5 = (-this.var_101d);
                                    this.var_109d -= 1;
                                }
                                gameVar.sub_6260(this, this.activeItemPositionMb,
                                        null, (byte)2);
                                this.var_106d = true;
                            }
                            else if (this.var_109d > 0)
                            {
                                this.var_10b5 = (-this.var_101d);
                                this.var_109d -= 1;
                                this.var_106d = true;
                            }
                            A_MenuBase.mainCanvas.clearActions();
                        }
                        if ((A_MenuBase.mainCanvas.invertActionCode(2))
                                || ((this.menuType == 7) && (A_MenuBase.mainCanvas
                                        .invertActionCode(2048))))
                        {
                            if (this.menuType == 11)
                            {
                                this.activeItemPositionMb += 1;
                                if (this.activeItemPositionMb >= this.menuItemsCount)
                                {
                                    this.activeItemPositionMb = 0;
                                    this.var_109d = 0;
                                }
                                else if (this.activeItemPositionMb >= this.var_109d
                                      + this.var_10a5)
                                {
                                    this.var_10b5 = this.var_101d;
                                    this.var_109d += 1;
                                }
                                gameVar.sub_6260(this, this.activeItemPositionMb,
                                        null, (byte)3);
                                this.var_106d = true;
                            }
                            else if (this.var_109d + this.var_10a5 < this.menuItemsNamesMb.Length)
                            {
                                this.var_10b5 = this.var_101d;
                                this.var_109d += 1;
                                this.var_106d = true;
                            }
                            else if (this.menuType == 7)
                            {
                                gameVar.sub_6260(this, 0, null, (byte)0);
                                return;
                            }
                            A_MenuBase.mainCanvas.clearActions();
                        }
                    }
                }
                else if (this.menuType == 8)
                {
                    if (A_MenuBase.mainCanvas.invertActionCode(1))
                    {
                        if (this.var_113d > 0)
                        {
                            this.var_113d -= 1;
                            this.var_106d = true;
                        }
                    }
                    else if ((A_MenuBase.mainCanvas.invertActionCode(2))
                          && (this.var_113d + this.var_114d < this.var_116d))
                    {
                        this.var_113d += 1;
                        this.var_106d = true;
                    }
                    if (A_MenuBase.mainCanvas.invertActionCode(4))
                    {
                        if (this.var_1135 > 0)
                        {
                            this.var_1135 -= 1;
                            this.var_106d = true;
                        }
                    }
                    else if ((A_MenuBase.mainCanvas.invertActionCode(8))
                          && (this.var_1135 + this.var_1145 < this.var_1165))
                    {
                        this.var_1135 += 1;
                        this.var_106d = true;
                    }
                }
                if ((this.var_fd5 == 2)
                        && (this.menuActionsMb[1] != false)
                        && (A_MenuBase.mainCanvas.invertActionCode(I_Game.m_actionCancel)))
                {
                    A_MenuBase.mainCanvas.clearActionCode(I_Game.m_actionCancel);
                    A_MenuBase.mainCanvas.clearActions();
                    if (this.parentMenu != null)
                    {
                        A_MenuBase.mainCanvas.showMenu(this.parentMenu);
                    }
                    if (this.menuItemsNamesMb != null)
                    {
                        gameVar.sub_6260(this, this.activeItemPositionMb,
                                this.menuItemsNamesMb[this.activeItemPositionMb],
                                (byte)1);
                        return;
                    }
                    gameVar.sub_6260(this, -1, null, (byte)1);
                    return;
                }
            }
            if ((this.var_10cd) && (++this.var_1045 >= this.var_fdd))
            {
                if (this.var_103d == 0)
                {
                    this.var_103d = 2;
                }
                else
                {
                    this.var_103d = 0;
                }
                this.var_1045 = 0;
                this.var_106d = true;
            }
            switch (this.var_fd5)
            {
                case 0:
                    if (this.menuType == 0)
                    {
                        if (this.var_11cd < this.var_11d5)
                        {
                            if (this.var_121d == 2)
                            {
                                i = this.var_11d5 / 2;
                            }
                            else
                            {
                                i = this.var_11d5 / 5;
                            }
                            if (i < 1)
                            {
                                i = 1;
                            }
                            this.var_11cd += i;
                            if (this.var_11cd > this.var_11d5)
                            {
                                this.var_11cd = this.var_11d5;
                            }
                        }
                        else
                        {
                            this.var_11e5 = (Math.abs(360 - this.wheelItemDegree[0]) / 2);
                            if (this.var_11e5 < 1)
                            {
                                this.var_11e5 = 1;
                            }
                            else if (this.var_11e5 > this.var_11ed)
                            {
                                this.var_11e5 = this.var_11ed;
                            }
                        }
                        if (this.var_121d == 1)
                        {
                            for (i = 0; i < this.wheelItemDegree.Length; i++)
                            {
                                this.wheelItemDegree[i] = ((short)((this.wheelItemDegree[i] + this.var_11e5) % 360));
                            }
                        }
                        if ((A_MenuBase.mainCanvas.isAnyActionPressed())
                                || ((this.var_11cd >= this.var_11d5) && (this.wheelItemDegree[0] == 0)))
                        {
                            this.var_11cd = this.var_11d5;
                            for (i = 0; i < this.wheelItemDegree.Length; i++)
                            {
                                this.wheelItemDegree[i] = ((short)(this.wheelSectorDegree * i));
                            }
                            this.var_fd5 = 2;
                            if (this.var_10e5)
                            {
                                A_MenuBase.mainCanvas.clearActions();
                            }
                        }
                    }
                    else if (this.menuType == 13)
                    {
                        this.var_104d += (this.menuLocX - this.var_104d) / 2;
                        this.var_10d5 += 1;
                        if (this.var_10d5 == 2)
                        {
                            this.var_fd5 = 2;
                            this.var_104d = this.menuLocX;
                        }
                    }
                    else
                    {
                        if ((i = (this.menuLocX - this.var_104d) / 4) <= 0)
                        {
                            i = 1;
                        }
                        this.var_104d += i;
                        if (this.var_104d == this.menuLocX)
                        {
                            this.var_fd5 = 2;
                        }
                    }
                    this.var_106d = true;
                    return;
                case 1:
                    this.var_fd5 = 3;
                    break;
            }
        }

        public static  void drawRoundedRect(Graphics gr, int inX, int inY, int inW, int inH)
        {
            if (inH <= 2)
            {
                gr.fillRect(inX, inY, inW, inH);
                return;
            }
            gr.fillRect(inX, inY + 1, inW, inH - 2);
            gr.fillRect(inX + 1, inY, inW - 2, inH);
        }

        // @Override
        public override void onPaint(Graphics paramGraphics)
        {
            paintMenu(paramGraphics, 0, 0, false);
        }

        public  void paintMenu(Graphics gr, int marginX, int marginY, bool isSelected)
        {
            if (this.var_fd5 == 3)
            {
                return;
            }
            if (!this.var_106d)
            {
                return;
            }
            this.var_106d = false;
            if (((A_MenuBase.mainCanvas.mainDrawElement == this) && (this.var_1075))
                    || (this.menuType == 0))
            {
                gameVar.onPaint(gr);
            }
            this.var_1075 = false;
            gr.setClip(0, 0, this.someCanWidth, this.someCanHeight);
            if (this.somedescMenu != null)
            {
                this.somedescMenu.onPaint(gr);
            }
            int i = this.var_104d + marginX;
            int j = this.var_1055 + marginY;
            int menuWid = 0;
            int menuHei = 0;
            if ((this.menuType != 0) && (this.menuType != 13))
            {
                sub_562e(gr, i, j, this.menuWidth, this.menuHeight,
                        this.someBorderMb, this.var_117d, this.titleGradientColor,
                        this.var_1105, 5);
                gr.setClip(0, 0, this.someCanWidth, this.someCanHeight);
            }
            menuWid = this.menuWidth;
            menuHei = this.menuHeight;
            if ((this.someBorderMb & 0x1) == 0)
            {
                menuHei -= 5;
                j += 5;
            }
            if ((this.someBorderMb & 0x2) == 0)
            {
                menuHei -= 5;
            }
            if ((this.someBorderMb & 0x4) == 0)
            {
                i += 8;
                menuWid -= 8;
            }
            if ((this.someBorderMb & 0x8) == 0)
            {
                menuWid -= 8;
            }
            gr.translate(i, j);
            gr.setFont(this.var_1025);
            if ((this.menuTitleIcon != null)
                    && ((this.menuItemsNamesMb == null) || (this.var_1025
                            .stringWidth(this.menuItemsNamesMb[0]) < menuWid
                            - this.menuTitleIcon.imageWidth * 2)))
            {
                this.menuTitleIcon.drawImageExt(gr, 0, menuHei / 2, 6);
            }
            if (isSelected)
            {
                gr.setColor(5594742); //#555E76 darkgray blue
                drawRoundedRect(gr, 0, 0, menuWid, menuHei);
            }
            int rotationDeg;
            int i3;
            int i4;
            int n;
            int i1;
            int i5 = 0;
            if (this.menuType == 0)
            { // Wheel
                gr.setColor(16777215); //#FFFFFF
                rotationDeg = this.var_11fd + this.var_1205;
                for (int wIt = this.wheelItemDegree.Length - 1; wIt >= 0; wIt--)
                {
                    int angle = (this.wheelItemDegree[wIt] + rotationDeg) % 360;
                    if (angle < 0)
                    {
                        angle += 360;
                    }
                    int nnX = this.wheelMenuRadius + (A_MenuBase.getSin1024(angle) * this.var_11cd >> 10);
                    int nnY = this.wheelMenuRadius + 2 - (A_MenuBase.getCos2014(angle) * this.var_11cd >> 10);
                    nnY += E_MainCanvas.someMenuShiftHeight;
                    if ((this.var_fd5 == 2) && (wIt == this.activeItemPositionMb))
                    {
                        this.wheelItemBgImage.drawFrameAt(gr, 1, nnX, nnY, 3); // active item
                        if (this.var_1205 == 0)
                        {
                            i5 = 0;
                        }
                        int itt = 0;
                        while (itt < this.smallSparksMenuSprites.Length)
                        {
                            int fx = nnX - this.var_1215;
                            int fy = nnY - this.var_1215;
                            this.smallSparksMenuSprites[itt].drawCurrentFrame(gr, fx, fy, 20);
                            itt++;
                            // continue; @todo
                        }
                    }
                    else
                    {
                        this.wheelItemBgImage.drawFrameAt(gr, 0, nnX, nnY, 3); // passive item
                    }
                    if ((this.menuItemsNamesMb[wIt] != null)
                            && (this.menuItemsImages != null)
                            && (this.menuItemsImages[wIt] != null))
                    {
                        this.menuItemsImages[wIt].drawImageExt(gr, nnX, nnY, 3);
                    }
                }
                if (this.var_fd5 == 2)
                {
                    for (i3 = 0; i3 < this.smallSparksMenuSprites.Length; i3++)
                    {
                        this.smallSparksMenuSprites[i3].drawCurrentFrame(gr,
                                (this.menuWidth - this.menuItemFrameWidthMb) / 2,
                                E_MainCanvas.someMenuShiftHeight, 3);
                    }
                }
            }
            int i6;
            int i7;
            int i9;
            int i10;
            int i12;
            int i13;
            int i14;
            int sTileWidth;
            int sTileHeight;
            int i17;
            int i18;
            int i19;
            int i20;
            int i21 = 0;
            int mapUnitsCount = 0;
            switch (this.menuType)
            {
                case 15:
                    i3 = 0;
                    i4 = 0;
                    i5 = this.var_11ad[0];
                    for (i6 = 1; i6 <= this.var_119d; i6++)
                    {
                        i3 = i5;
                        i5 += this.var_11ad[i6];
                    }
                    if (this.var_119d > 0)
                    {
                        i4 = -i3 + gameVar.buttonsSprite.frameHeight;
                    }
                    for (i6 = 0; i6 < this.childrenMenuList.size(); i6++)
                    {
                        D_Menu localClass_d_023;
                        if (((localClass_d_023 = (D_Menu)this.childrenMenuList
                                .elementAt(i6)).menuLocY >= i3)
                                && (localClass_d_023.menuLocY < i5))
                        {
                            localClass_d_023.paintMenu(gr, 0, i4, i6 == this.var_111d);
                        }
                    }
                    gr.setClip(0, 0, gameVar.someGWidth, gameVar.someGHeight);
                    if (this.var_119d > 0)
                    {
                        gr.setColor(2370117);
                        gr.fillRect(0, 0, this.someCanWidth,
                                gameVar.buttonsSprite.frameHeight);
                        gameVar.arrowSprite.drawFrameAt(gr, 0, gameVar.someGWidth / 2,
                                -this.var_103d, 17);
                    }
                    if (this.var_119d < this.var_11a5)
                    {
                        gr.setColor(2370117);
                        i6 = this.var_11ad[this.var_119d];
                        if (this.var_119d > 0)
                        {
                            i6 += gameVar.buttonsSprite.frameHeight;
                        }
                        gr.fillRect(0, i6, this.someCanWidth, gameVar.someGHeight - i6);
                        gameVar.arrowSprite.drawFrameAt(gr, 1, gameVar.someGWidth / 2,
                                gameVar.someGHeight + this.var_103d, 33);
                    }
                    break;
                case 0:
                    if (this.var_fd5 == 2)
                    {
                        gr.setColor(1645370); // dark blue
                        if (this.var_121d == 2)
                        {
                            i6 = this.menuWidth;
                            if (this.menuItemsNamesMb[this.activeItemPositionMb] != null)
                            {
                                i7 = E_MainCanvas.font8
                                        .stringWidth(this.menuItemsNamesMb[this.activeItemPositionMb]) + 2;
                                if (i6 < i7)
                                {
                                    i6 = i7;
                                }
                            }
                            drawRoundedRect(gr, (this.menuWidth - i6) / 2, 1, i6,
                                    E_MainCanvas.someMenuShiftHeight);
                        }
                        else
                        {
                            drawRoundedRect(gr, 2 - this.menuLocX, 1, this.someCanWidth - 4,
                                    E_MainCanvas.someMenuShiftHeight);
                        }
                        if (this.menuItemsNamesMb[this.activeItemPositionMb] != null)
                        {
                            gr.setColor(16777215);
                            E_MainCanvas.drawString(gr,
                                    this.menuItemsNamesMb[this.activeItemPositionMb],
                                    this.wheelMenuRadius, (this.var_1085 >> 1) + 1, 17);
                        }
                    }
                    break;
                case 3:
                    gr.setClip(0, 0, menuWid, menuHei);
                    i1 = someXPadding;
                    gr.setColor(11515819); // #AFB7AB light green blue
                    gr.drawLine(someXPadding, i1, menuWid - someXPadding * 2, i1);
                    i1 = i1 += 1 + someXPadding + gameVar.bigCircleSprite.frameHeight
                            / 2;
                    n = gameVar.sideArrowSprite.frameWidth + this.var_10b5
                            + this.var_10f5;
                    i6 = this.var_109d;
                    i7 = this.var_109d + this.var_10a5;
                    if (this.var_10b5 > 0)
                    {
                        i6--;
                        n -= this.var_101d;
                    }
                    else if (this.var_10b5 < 0)
                    {
                        i7++;
                    }
                    for (int i8 = i6; i8 < i7; i8++)
                    {
                        if ((i9 = i8 % this.menuItemsCount) < 0)
                        {
                            i9 += this.menuItemsCount;
                        }
                        i10 = n + this.var_101d / 2;
                        if (i9 == this.activeItemPositionMb)
                        {
                            gameVar.bigCircleSprite.drawFrameAt(gr, 1, i10, i1, 3);
                        }
                        else
                        {
                            gameVar.bigCircleSprite.drawFrameAt(gr, 0, i10, i1, 3);
                        }
                        C_Unit cUnit = this.buyUnits[i9];
                        i12 = i10 - cUnit.posXPixel - cUnit.frameWidth / 2;
                        i13 = i1 - cUnit.posYPixel - cUnit.frameHeight / 2;
                        cUnit.sub_2551(gr, i12, i13,
                                cUnit.cost > gameVar.playersMoney[gameVar.playerId]);
                        if (i9 == this.activeItemPositionMb)
                        {
                            i14 = i10 - this.wheelItemBgImage.frameWidth / 2;
                            sTileWidth = i1 - this.wheelItemBgImage.frameWidth / 2;
                            for (sTileHeight = 0; sTileHeight < this.smallSparksMenuSprites.Length; sTileHeight++)
                            {
                                this.smallSparksMenuSprites[sTileHeight].drawCurrentFrame(gr, i14, sTileWidth, 20);
                            }
                        }
                        n += this.var_101d;
                    }
                    gameVar.sideArrowSprite.drawFrameAt(gr, 0, 0, i1, 6);
                    gameVar.sideArrowSprite.drawFrameAt(gr, 1, menuWid, i1, 10);
                    break;
                case 2:
                case 5:
                    gr.setClip(0, 0, menuWid, menuHei);
                    i6 = someXPadding;
                    i7 = E_MainCanvas.charsSprites[0].frameWidth;
                    i1 = i6;
                    this.menuUnit.sub_252e(gr, -this.menuUnit.posXPixel + i6,
                            -this.menuUnit.posYPixel + i1);
                    rotationDeg = i1 + this.menuUnit.frameHeight / 2;
                    String str = null;
                    gr.setFont(E_MainCanvas.font8);
                    gr.setColor(this.m_bgColorMb);
                    E_MainCanvas.drawString(gr, this.menuUnit.unitName, i6
                            + this.menuUnit.frameWidth + i6, rotationDeg
                            - E_MainCanvas.font8BaselinePos / 2, 20);
                    if (this.menuType == 2)
                    {
                        str = "" + this.menuUnit.cost;
                        gameVar.hudIcons2Sprite.drawFrameAt(gr, 1, menuWid - i6
                                - E_MainCanvas.getCharedStringWidth((byte)1, str), rotationDeg,
                                10);
                    }
                    else
                    {
                        str = "" + this.menuUnit.unitHealthMb;
                    }
                    E_MainCanvas.drawCharedString(gr, str, menuWid - i6, rotationDeg, 1, 10);
                    this.var_1085 = (E_MainCanvas.someMenuShiftHeight - E_MainCanvas.font8BaselinePos);
                    i1 += this.menuUnit.frameHeight + someXPadding;
                    gr.setColor(this.m_bgColorMb);
                    gr.drawLine(i6, i1, menuWid - i6 - i6, i1);
                    i1 += 1 + someXPadding;
                    int i11;
                    if (this.menuType == 5)
                    {
                        i9 = E_MainCanvas.font8BaselinePos;
                        rotationDeg = i1 + i9 / 2;
                        n = i6;
                        E_MainCanvas.drawString(gr, A_MenuBase.getLangString(97), n,
                                i1, 20);
                        i10 = E_MainCanvas.font8.stringWidth(A_MenuBase
                                .getLangString(97));
                        n += i10 + i6;
                        i11 = menuWid - n - i6 - gameVar.hudIconsSprite.frameWidth - i7
                                - i6;
                        gr.setColor(this.m_bgColorMb);
                        drawRoundedRect(gr, n, i1, i11, i9);
                        gr.setColor(2370117);
                        if ((i12 = i11 * this.menuUnit.experience
                                / this.menuUnit.getLevelExpMax()) <= 0)
                        {
                            i12 = 1;
                        }
                        gr.fillRect(n + 1, i1 + 1, i12, i9 - 2);
                        n = menuWid - i6 - i7;
                        gameVar.hudIconsSprite.drawFrameAt(gr, 2, n, rotationDeg, 10);
                        E_MainCanvas.drawCharedString(gr, "" + this.menuUnit.level, n,
                                rotationDeg, 0, 6);
                        i1 += i9 + someXPadding;
                        gr.setColor(this.m_bgColorMb);
                        gr.drawLine(i6, i1, menuWid - i6 - i6, i1);
                        i1 += 1 + someXPadding;
                    }
                    i9 = (menuWid - i6 * 3) / 2;
                    i10 = gameVar.hudIconsSprite.frameHeight;
                    i12 = (i11 = gameVar.smallCircleSprite.frameHeight) / 2;
                    for (int i141 = 0; i141 < 2; i141++)
                    {
                        i13 = i1 + i12 - i10 / 2;
                        n = i6;
                        for (int i151 = 0; i151 < 2; i151++)
                        {
                            if ((i141 == 0) || (i151 == 0))
                            {
                                sTileHeight = n + i12;
                                drawRoundedRect(gr, sTileHeight, i13, i9 - i12, i10);
                                gameVar.smallCircleSprite.onSpritePaint(gr, n, i1);
                                if (((i17 = i141 * 2 + i151) == 0) || (i17 == 1))
                                {
                                    gameVar.hudIconsSprite.drawFrameAt(gr, i17, sTileHeight, i1
                                            + i12, 3);
                                }
                                i18 = 0;
                                if (i17 == 0)
                                {
                                    if (this.menuType == 5)
                                    {
                                        i18 = this.menuUnit.getUnitExtraAttack(null);
                                    }
                                    str = this.menuUnit.unitAttackMin + i18 + "-"
                                            + (this.menuUnit.unitAttackMax + i18);
                                }
                                else if (i17 == 1)
                                {
                                    if (this.menuType == 5)
                                    {
                                        i18 = this.menuUnit.getUnitResistance(null);
                                    }
                                    str = "" + (this.menuUnit.unitDefence + i18);
                                }
                                else if (i17 == 2)
                                {
                                    gameVar.actionIconsFrames[5].drawImageExt(gr, sTileHeight,
                                            i1 + i12, 3);
                                    str = "" + C_Unit.unitsMoveRanges[this.menuUnit.unitTypeId];
                                }
                                E_MainCanvas.drawCharedString(gr, str, n + i11 + 1, i1
                                        + i12, 0, 6);
                                if (i18 > 0)
                                {
                                    gameVar.arrowIconsSprite.drawFrameAt(gr, 1, sTileHeight
                                            + i9 - i12 - 1, i1 + i12, 10);
                                }
                                else if (i18 < 0)
                                {
                                    gameVar.arrowIconsSprite.drawFrameAt(gr, 2, sTileHeight
                                            + i9 - i12 - 1, i1 + i12, 10);
                                }
                                n += i9 + someXPadding;
                            }
                        }
                        i1 += i11;
                    }
                    i1 += someXPadding;
                    gr.setColor(this.m_bgColorMb);
                    gr.drawLine(i6, i1, menuWid - i6 - i6, i1);
                    break;
                case 8:
                    gameVar.drawMenuBorderRect(gr, 0, 0, this.menuWidth, this.menuHeight);
                    sTileWidth = gameVar.smallTilesImages[0].imageWidth;
                    sTileHeight = gameVar.smallTilesImages[0].imageHeight;
                    i17 = this.var_114d + this.var_113d;
                    i18 = this.var_1145 + this.var_1135;
                    i1 = 4;
                    for (i19 = this.var_113d; i19 < i17; i19++)
                    {
                        n = 4;
                        for (i20 = this.var_1135; i20 < i18; i20++)
                        {
                            i21 = gameVar.tilesProps[this.mapTilesData[i20][i19]];
                            if (this.mapTilesData[i20][i19] >= gameVar.houseTileIdStartIndex)
                            {
                                mapUnitsCount = (this.mapTilesData[i20][i19] - gameVar.houseTileIdStartIndex) / 2;
                                i21 = 2 * mapUnitsCount + 8 + i21 - 8;
                            }
                            gameVar.smallTilesImages[i21].drawImageExt(gr, n, i1);
                            n += sTileWidth;
                        }
                        i1 += sTileHeight;
                    }
                    if ((this.mapUnitsList != null) && (this.var_103d == 0))
                    {
                        i19 = -this.var_1135 * sTileWidth + 4;
                        i20 = -this.var_113d * sTileHeight + 4;
                        int uInd = 0;
                        mapUnitsCount = gameVar.mapUnitsSprites.size();
                        while (uInd < mapUnitsCount)
                        {
                            C_Unit miniUnit = (C_Unit)gameVar.mapUnitsSprites.elementAt(uInd);
                            if ((miniUnit.positionX >= this.var_1135)
                                    && (miniUnit.positionX < i18)
                                    && (miniUnit.positionY >= this.var_113d)
                                    && (miniUnit.positionY < i17))
                            {
                                int unitPlayerFrameIndex = gameVar.playersIndexes[miniUnit.playerId] - 1;
                                gameVar.miniIconsSprite
                                        .drawFrameAt(
                                                gr,
                                                unitPlayerFrameIndex,
                                                miniUnit.positionX * sTileWidth + i19,
                                                miniUnit.positionY * sTileHeight + i20,
                                                0);
                            }
                            uInd++;
                        }
                    }
                    if (this.var_103d == 0)
                    {
                        if (this.var_113d > 0)
                        {
                            gameVar.arrowSprite.drawFrameAt(gr, 0, menuWid / 2, 0, 17);
                        }
                        if (this.var_113d + this.var_114d < this.var_116d)
                        {
                            gameVar.arrowSprite.drawFrameAt(gr, 1, menuWid / 2,
                                    menuHei, 33);
                        }
                        if (this.var_1135 > 0)
                        {
                            gameVar.sideArrowSprite.drawFrameAt(gr, 0, 0, menuHei / 2,
                                    6);
                        }
                        if (this.var_1135 + this.var_1145 < this.var_1165)
                        {
                            gameVar.sideArrowSprite.drawFrameAt(gr, 1, menuWid,
                                    menuHei / 2, 10);
                        }
                    }
                    break;
                case 14:
                    gr.setFont(E_MainCanvas.font8);
                    gr.setColor(I_Game.someColorMethod2(16777215, 1645370, this.var_1105, 5));
                    rotationDeg = menuHei / 2;
                    E_MainCanvas.drawString(gr,
                            this.menuItemsNamesMb[this.activeItemPositionMb],
                            menuWid / 2, (menuHei - E_MainCanvas.font8BaselinePos) / 2,
                            17);
                    gameVar.sideArrowSprite.drawFrameAt(gr, 0, 0, rotationDeg, 6);
                    gameVar.sideArrowSprite.drawFrameAt(gr, 1, menuWid, rotationDeg, 10);
                    break;
                case 13:
                case 7:
                case 10:
                case 11:

                    if (this.menuType == 13)
                    {
                        i19 = E_MainCanvas.someMenuShiftHeight;
                        i20 = (this.menuHeight - i19) / 2;
                        gr.setColor(I_Game.someColorMethod2(1645370, 16777215, this.var_1105, 5));
                        drawRoundedRect(gr, 0, i20, this.menuWidth, i19);
                        gr.setFont(E_MainCanvas.font8);
                        gr.setColor(16777215);
                        E_MainCanvas.drawString(gr,
                                this.menuItemsNamesMb[this.activeItemPositionMb], 16, i20
                                        + this.var_108d, 20);
                        i21 = this.menuWidth - this.var_101d;
                        mapUnitsCount = this.menuItemsCount - 1;

                        try
                        {
                            //@todo if(what?)
                            while (mapUnitsCount >= 0)
                            {
                                if (mapUnitsCount == this.activeItemPositionMb)
                                {
                                    gameVar.smallCircleSprite.drawFrameAt(gr, 1, i21, 0, 20);
                                }
                                else
                                {
                                    gameVar.smallCircleSprite.drawFrameAt(gr, 0, i21, 0, 20);
                                }
                                this.menuItemsImages[mapUnitsCount].drawImageExt(gr, i21
                                        + gameVar.smallCircleSprite.frameWidth / 2,
                                        this.menuHeight / 2, 3);
                                i21 -= this.var_101d;
                                mapUnitsCount--;
                            }
                        }
                        catch (Exception exx)
                        {
                            //
                        }
                    }

                    gr.setFont(E_MainCanvas.font8);
                    //show portrait
                    if (this.portraitSpriteIndex != -1)
                    {
                        gameVar.portraitsSprite.drawFrameAt(gr, this.portraitSpriteIndex, -8,
                                menuHei, 36);
                    }
                    menuWid -= this.unitPortraitWidth;
                    gr.setClip(0, 0, menuWid, this.menuHeight - 10);
                    mapUnitsCount = 0;
                    i1 = 0;
                    if (this.wrappedHeaderMb != null)
                    {
                        gr.setColor(I_Game.someColorMethod2(16777215, this.var_117d,
                                this.var_1105, 5));
                        for (int i23 = 0; i23 < this.wrappedHeaderMb.Length; i23++)
                        {
                            E_MainCanvas.drawString(gr, this.wrappedHeaderMb[i23],
                                    this.unitPortraitWidth + menuWid / 2,
                                    i1 + this.var_108d, 17);
                            i1 += this.var_101d;
                        }
                        gr.setColor(10463131);
                        gr.drawLine(0, i1, menuWid - 1, i1);
                        mapUnitsCount = i1;
                    }
                    int i231 = i1 + this.var_10f5;
                    gr.setColor(this.m_bgColorMb);
                    int i24 = this.var_109d;
                    int i25 = this.var_109d + this.var_10a5;
                    if (this.var_10b5 > 0)
                    {
                        i24--;
                        i1 -= this.var_101d;
                    }
                    else if (this.var_10b5 < 0)
                    {
                        i25++;
                    }
                    i1 += this.var_10b5 + this.var_10f5;
                    gr.setClip(this.unitPortraitWidth, mapUnitsCount, menuWid, menuHei - mapUnitsCount);
                    int i26 = menuWid;
                    if (this.var_110d)
                    {
                        i26 -= gameVar.arrowSprite.frameWidth;
                    }
                    int i27 = this.unitPortraitWidth + i26 / 2;
                    int i29;
                    int i30;
                    for (int sIt = i24; sIt < i25; sIt++)
                    {
                        i29 = 0;
                        if (i1 < i231)
                        {
                            i29 = i231 - i1;
                        }
                        else if (i1 + this.var_101d > menuHei - this.var_10f5)
                        {
                            i29 = i1 + this.var_101d - menuHei + this.var_10f5;
                        }
                        if ((this.menuType == 11)
                                && (sIt == this.activeItemPositionMb))
                        {
                            gr.setColor(5594742);
                            drawRoundedRect(gr, 0, i1, i26, this.var_101d);
                            i30 = I_Game.someColorMethod2(this.var_117d, 16777215,
                                    this.var_101d - i29, this.var_101d);
                        }
                        else
                        {
                            i30 = I_Game.someColorMethod2(this.var_117d, this.m_bgColorMb,
                                    this.var_101d - i29, this.var_101d);
                        }
                        i30 = I_Game.someColorMethod2(i30, this.var_117d, this.var_1105, 5);
                        gr.setColor(i30);
                        if (this.var_11bd >= 0)
                        {
                            E_MainCanvas.drawString(gr, this.menuItemsNamesMb[sIt],
                                    this.var_11bd, i1 + this.var_108d, 20);
                        }
                        else
                        {
                            E_MainCanvas.drawString(gr, this.menuItemsNamesMb[sIt],
                                    i27, i1 + this.var_108d, 17);
                        }
                        i1 += this.var_101d;
                    }
                    int i28;
                    if (this.var_110d)
                    {
                        i28 = gameVar.arrowSprite.frameHeight;
                        i29 = gameVar.arrowSprite.frameWidth;
                        i30 = gameVar.arrowSprite.frameWidth / 2;
                        int i31 = menuHei - i28 * 2 - 2;
                        int i32 = menuWid - (i29 + i30) / 2;
                        if (i31 > 2)
                        {
                            gr.setColor(this.m_bgColorMb);
                            drawRoundedRect(gr, i32, i28 + 1, i30, i31);
                            int i33;
                            if ((i33 = (i31 - 2) * this.var_10a5
                                    / this.menuItemsCount) < 1)
                            {
                                i33 = 1;
                            }
                            gr.setColor(2370117);
                            drawRoundedRect(gr, i32 + 1, i28 + (i31 - 2) * this.var_109d
                                    / this.menuItemsCount + 2, i30 - 2, i33);
                            gameVar.arrowSprite.drawFrameAt(gr, 0, menuWid - i29,
                                    0, 20);
                            gameVar.arrowSprite.drawFrameAt(gr, 1, menuWid - i29,
                                    menuHei, 36);
                        }
                        else
                        {
                            if (this.var_109d > 0)
                            {
                                gameVar.arrowSprite.drawFrameAt(gr, 0, menuWid
                                        - i29, 0, 20);
                            }
                            if (this.var_109d + this.var_10a5 < this.menuItemsCount)
                            {
                                gameVar.arrowSprite.drawFrameAt(gr, 1, menuWid
                                        - i29, menuHei, 36);
                            }
                        }
                    }
                    if (this.menuType == 7)
                    {
                        gr.setClip(0, 0, this.someCanWidth, this.someCanHeight);
                        i28 = menuHei;
                        if ((this.someBorderMb & 0x2) == 0)
                        {
                            i28 += 5;
                        }
                        gameVar.arrowSprite.drawFrameAt(gr, 1, menuWid
                                + this.unitPortraitWidth, i28, 40);
                    }
                    break;
            }
            gr.translate(-i, -j);
            gr.setClip(0, 0, this.someCanWidth, this.someCanHeight);
            if ((A_MenuBase.mainCanvas.mainDrawElement == this) && (this.var_fd5 == 2))
            {
                if (this.menuActionsMb[0] != false)
                {
                    gameVar.drawActionButton(gr, I_Game.m_actionApply, 0, gameVar.someGHeight);
                }
                if (this.menuActionsMb[1] != false)
                {
                    gameVar.drawActionButton(gr, I_Game.m_actionCancel, 1, gameVar.someGHeight);
                }
            }
        }

        public static  void sub_5602(Graphics paramGraphics, int paramInt1,
                int paramInt2, int paramInt3, int paramInt4, int paramInt5)
        {
            sub_562e(paramGraphics, paramInt1, paramInt2, paramInt3, paramInt4,
                    paramInt5, 2370117, 2370117, 0, 0);
        }

        public static  void sub_562e(Graphics paramGraphics, int paramInt1,
                int paramInt2, int paramInt3, int paramInt4, int paramInt5,
                int paramInt6, int paramInt7, int paramInt8, int paramInt9)
        {
            F_Sprite localClass_f_045 = gameVar.menuSprite;
            paramGraphics.setClip(paramInt1, paramInt2, paramInt3, paramInt4);
            paramGraphics.setColor(paramInt6);
            paramGraphics.fillRect(paramInt1, paramInt2, paramInt3, paramInt4);
            int i;
            int j;
            int m;
            int k;
            if (paramInt7 != paramInt6)
            {
                i = paramInt4 / 4;
                j = paramInt2 + 5;
                for (m = 0; m < i; m++)
                {
                    k = I_Game.someColorMethod2(paramInt7, paramInt6, m, i);
                    paramGraphics.setColor(I_Game.someColorMethod2(k, paramInt6, paramInt8,
                            paramInt9));
                    paramGraphics.fillRect(paramInt1, j, paramInt3, 1);
                    j++;
                }
            }
            if (paramInt5 != 15)
            {
                i = (paramInt5 & 0x4) == 0 ? 1 : 0;
                j = (paramInt5 & 0x8) == 0 ? 1 : 0;
                k = (paramInt5 & 0x1) == 0 ? 1 : 0;
                m = (paramInt5 & 0x2) == 0 ? 1 : 0;
                int n = paramInt3 / localClass_f_045.frameWidth - 2;
                if (paramInt3 % localClass_f_045.frameWidth != 0)
                {
                    n++;
                }
                if (i == 0)
                {
                    n++;
                }
                if (j == 0)
                {
                    n++;
                }
                int i1 = paramInt4 / localClass_f_045.frameHeight - 2;
                if (paramInt4 % localClass_f_045.frameHeight != 0)
                {
                    i1++;
                }
                if (k == 0)
                {
                    i1++;
                }
                if (m == 0)
                {
                    i1++;
                }
                int i2 = paramInt1;
                if (i != 0)
                {
                    i2 += localClass_f_045.frameWidth;
                }
                int i3 = paramInt2 + paramInt4 - localClass_f_045.frameHeight;
                for (int i4 = 0; i4 < n; i4++)
                {
                    if (k != 0)
                    {
                        localClass_f_045.drawFrameAt(paramGraphics, 1, i2,
                                paramInt2, 0);
                    }
                    if (m != 0)
                    {
                        localClass_f_045.drawFrameAt(paramGraphics, 6, i2, i3, 0);
                    }
                    i2 += localClass_f_045.frameWidth;
                }
                int i45 = paramInt2;
                if (k != 0)
                {
                    i45 += localClass_f_045.frameHeight;
                }
                int i5 = paramInt1 + paramInt3 - localClass_f_045.frameWidth;
                for (int i6 = 0; i6 < i1; i6++)
                {
                    if (i != 0)
                    {
                        localClass_f_045.drawFrameAt(paramGraphics, 3, paramInt1,
                                i45, 0);
                    }
                    if (j != 0)
                    {
                        localClass_f_045.drawFrameAt(paramGraphics, 4, i5, i45, 0);
                    }
                    i45 += localClass_f_045.frameHeight;
                }
                if ((i != 0) && (k != 0))
                {
                    localClass_f_045.drawFrameAt(paramGraphics, 0, paramInt1,
                            paramInt2, 0);
                }
                if ((j != 0) && (k != 0))
                {
                    localClass_f_045
                            .drawFrameAt(paramGraphics, 2, i5, paramInt2, 0);
                }
                if ((i != 0) && (m != 0))
                {
                    localClass_f_045
                            .drawFrameAt(paramGraphics, 5, paramInt1, i3, 0);
                }
                if ((j != 0) && (m != 0))
                {
                    localClass_f_045.drawFrameAt(paramGraphics, 7, i5, i3, 0);
                }
            }
        }
    }

}