using java.lang;

using javax.microedition.midlet;
using javax.microedition.lcdui;
using java.io;
using java.csharp;
using javax.microedition.rms;
using javax.microedition.media;
using java.util;

namespace aeii
{

    public  class E_MainCanvas : Canvas, Runnable, CommandListener
    {

        public static  Font font0 = Font.getFont(0, 1, 0);
        public static  Font font8 = Font.getFont(0, 1, 8);
        public static  int font8BaselinePos = font8.getBaselinePosition();
        public static  int someMenuShiftHeight = font8BaselinePos + 6;
        public static  int font0BaselinePos = font0.getBaselinePosition();
        public static  int var_139c = font0BaselinePos + 8;
        public static  short[] numericAndDelStartingChars = { 45, 43 }; //char 45='/' 43= '-' 44='.' 46='0' 57='9'
        public static  short[] numericEndChars = { 57, 57 };
        public static  sbyte[][] charFontsCharIndexes = {
			new sbyte[]{ 10, 11, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
			new sbyte[]{ 12, -1, 11, -1, 10, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 } 
        };
        private static Display display;
        private bool isRunning = false;
        private bool isLoading = true;
        public A_MenuBase mainDrawElement;
        public static int canvasWidth;
        public static int canvasHeight;
        public int someActionsSum = 0;
        public int someActionSum2;
        public int someActionCode = 0;
        public long someActionStartTime;
        public static F_Sprite[] charsSprites = new F_Sprite[2];
        public static Random random = new Random();
        public static bool[] settings = { true, true, true, true };
        public static String[] settingsNames;
        public bool m_notifyUnkFlag = false;
        public static int musicPlayerId = -1;
        public static int musicLoopCount;
        public static bool m_notifyShownMb = false;
        public static  String[] musicNames = { "main_theme", "bg_story",
			"bg_good", "bg_bad", "battle_good", "battle_bad", "victory",
			"gameover", "game_complete" };
        public static  byte[] someMusicByteArr = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static Player[] musicPlayers;
        public static Player currentMusicPlayer;
        public static bool[] musicPlayersLoaded;
        public static int currentMusicId;
        public static int currentMusicLoopCount;
        public static byte[][] resourcesData;
        public static String[] resourcesNames;

        public E_MainCanvas(MIDlet paramMIDlet)
        {
            try
            {
                setFullScreenMode(true);
                A_MenuBase.mainCanvas = this;
                A_MenuBase.loadLangStrings("/lang.dat", false);
                canvasWidth = getWidth();
                canvasHeight = getHeight();
                display = Display.getDisplay(paramMIDlet);
                display.setCurrent(this);
                new Thread(this).start();
                return;
            }
            catch (Exception ex)
            {
                showFatalError(ex.toString());
            }
        }

        public static  int getRandomMax(int max)
        {
            return getRandomWithin(0, max);
        }

        public static  int getRandomWithin(int min, int max)
        {
            return min + Math.abs(random.nextInt()) % (max - min);
        }

        public static  int getRandomInt()
        {
            return random.nextInt();
        }

        public static byte[] getRecordStoreData(String recName, int recIndex)
        {
            RecordStore store = RecordStore.openRecordStore(recName, false);
            byte[] recData = store.getRecord(recIndex + 1); //@my  + 1
            store.closeRecordStore();
            return recData;
        }

        public static void saveRecordStoreData(String recordName, int recIndex, byte[] data)
        {
            RecordStore recStore = RecordStore.openRecordStore(recordName, true);
            int numRecs = recStore.getNumRecords();
            if (numRecs <= recIndex)
            {
                while (numRecs < recIndex)
                {
                    recStore.addRecord(null, 0, 0);
                    numRecs++;
                }
                recStore.addRecord(data, 0, data.Length);
            }
            else
            {
                recStore.setRecord(recIndex + 1, data, 0, data.Length);
            }
            recStore.closeRecordStore();
        }

        public static  int saveDataToStore(String storeName, byte[] data)
        {
            RecordStore record = RecordStore.openRecordStore(storeName, true);
            int recordSize = record.addRecord(data, 0, data.Length);
            record.closeRecordStore();
            return recordSize - 1;
        }

        public static  void deleteStoreRecordByIndex(String recName, int recIndex)
        {
            RecordStore store = RecordStore.openRecordStore(recName, true);
            store.deleteRecord(recIndex + 1);
            store.closeRecordStore();
        }

        public static  int getRecordStoreAvailableSize(String recordName)
        {
            int size = 0;
            try
            {
                RecordStore locRecord = RecordStore.openRecordStore(recordName, true);
                size = locRecord.getSizeAvailable();
                locRecord.closeRecordStore();
            }
            catch (Exception ex)
            {
                //
            }
            return size;
        }

        public static  int getCharedStringWidth(byte charId, String str)
        {
            return charsSprites[charId].frameWidth * str.length();
        }

        public static  int getCharedStringHeight(byte charId)
        {
            return charsSprites[charId].frameHeight;
        }

        public static  void setColor(Graphics gr, int color)
        {
            gr.setColor(color);
        }

        public  void showNotify()
        {
            this.m_notifyUnkFlag = false;
            m_notifyShownMb = false;
            clearActions();
            if (this.mainDrawElement != null)
            {
                this.mainDrawElement.onLoad();
            }
        }

        public  void hideNotify()
        {
            clearActions();
            if (this.mainDrawElement != null)
            {
                if (!this.m_notifyUnkFlag)
                {
                    m_notifyShownMb = true;
                    if ((currentMusicPlayer != null) && (currentMusicPlayer.getState() == 400)
                            && (someMusicByteArr[currentMusicId] == 1))
                    {
                        musicPlayerId = currentMusicId;
                        musicLoopCount = currentMusicLoopCount;
                    }
                    stopMusic();
                }
                this.m_notifyUnkFlag = false;
            }
        }

        public static  void drawCharedString(Graphics gr,
                String inStr, int inX, int inY, int charInd, int paramInt4)
        {
            if ((paramInt4 & 0x8) != 0)
            {
                inX -= getCharedStringWidth((byte)charInd, inStr);
            }
            else if ((paramInt4 & 0x1) != 0)
            {
                inX -= getCharedStringWidth((byte)charInd, inStr) / 2;
            }
            if ((paramInt4 & 0x20) != 0)
            {
                inY -= getCharedStringHeight((byte)charInd);
            }
            else if ((paramInt4 & 0x2) != 0)
            {
                inY -= getCharedStringHeight((byte)charInd) / 2;
            }
            drawCharedString(gr, inStr, inX, inY, charInd);
        }

        public static  void drawCharedString(Graphics gr, String inStr, int inX, int inY, int charInt)
        {
            int mIt = 0;
            int nLength = inStr.length();
            while (mIt < nLength)
            {
                int ch = inStr.charAt(mIt);
                if ((ch >= numericAndDelStartingChars[charInt]) && (ch <= numericEndChars[charInt]))
                {
                    int frameIndex = charFontsCharIndexes[charInt][(ch - numericAndDelStartingChars[charInt])];
                    if (frameIndex != -1)
                    {
                        charsSprites[charInt].setCurrentFrameIndex(frameIndex);
                        charsSprites[charInt].onSpritePaint(gr, inX, inY);
                        inX += charsSprites[charInt].frameWidth;
                    }
                    else
                    {
                        byte[] charBytes = { (byte)ch };
                        String str = new String(charBytes);
                        gr.drawString(str, inX, inY, 20);
                        inX += gr.getFont().stringWidth(str);
                    }
                }
                mIt++;
            }
        }

        public static  void drawString(Graphics gr, String aString, int centerX, int centerY, int anchor)
        {
            gr.drawString(aString, centerX, centerY - 2, anchor);
        }

        public  void showMenu(A_MenuBase menu)
        {
            clearActions();
            menu.onLoad();
            this.mainDrawElement = menu;
        }

        public  void repaintAll()
        {
            repaint();
            serviceRepaints();
        }

        public override void paint(Graphics graphics)
        {
            if (this.isLoading)
            {
                graphics.setColor(16777215); //#FFFFFF
                graphics.fillRect(0, 0, canvasWidth, canvasHeight);
                graphics.setFont(font8);
                graphics.setColor(0);
                //LOADING...
                graphics.drawString(A_MenuBase.getLangString(58), canvasWidth / 2,
                        canvasHeight / 2 - 1, 33);
                return;
            }
            this.mainDrawElement.onPaint(graphics);
        }

        public  int getGameAction(int paramInt)
        {
            try
            {
                switch (paramInt)
                {
                    case -6:
                        return 1024;
                    case -7:
                        return 2048;
                    case 48:
                        return 32;
                    case 53:
                        return 16;
                    case 49:
                        return 64;
                    case 51:
                        return 128;
                    case 55:
                        return 256;
                    case 57:
                        return 512;
                    case 50:
                        return 1;
                    case 56:
                        return 2;
                    case 52:
                        return 4;
                    case 54:
                        return 8;
                }
                switch (base.getGameAction(paramInt))
                {
                    case 1:
                        return 1;
                    case 6:
                        return 2;
                    case 2:
                        return 4;
                    case 5:
                        return 8;
                    case 8:
                        return 16;
                }
            }
            catch (Exception ex)
            {
                //
            }
            return 4096;
        }

        public  String getKeyName2(int paramInt)
        {
            int i = 0;
            switch (paramInt)
            {
                case 32:
                    i = 48;
                    break;
                case 16:
                    i = 53;
                    break;
                case 64:
                    i = 49;
                    break;
                case 128:
                    i = 51;
                    break;
                case 256:
                    i = 55;
                    break;
                case 512:
                    i = 57;
                    break;
                case 1:
                    i = 50;
                    break;
                case 2:
                    i = 56;
                    break;
                case 4:
                    i = 52;
                    break;
                case 8:
                    i = 54;
                    break;
            }
            return base.getKeyName(i);
        }

        public override void keyPressed(int keyCode)
        {
            int actionCode = getGameAction(keyCode);
            addActionCode(actionCode);
            if (this.mainDrawElement != null)
            {
                this.mainDrawElement.onKeyAction(keyCode, actionCode);
            }
        }

        public override void pointerDragged(int x, int y)
        {
            if (mainDrawElement != null)
                mainDrawElement.onPointerDragged(x, y);
        }

        public override void pointerPressed(int x, int y)
        {
            keyPressed(KEY_NUM5);
        }

        public  bool isAnyActionPressed()
        {
            return this.someActionsSum != 0;
        }

        public  void clearActions()
        {
            this.someActionCode = 0;
            this.someActionsSum = 0;
            this.someActionSum2 = 0;
        }

        public  bool invertActionCode(int code)
        {
            bool isCodeInSum = (this.someActionSum2 & code) != 0;
            this.someActionSum2 &= (int)(code ^ 0xFFFFFFFF);
            return isCodeInSum;
        }

        public  bool someActionCodeIsSet(int actCode)
        {
            return (this.someActionsSum & actCode) != 0;
        }

        public override void keyReleased(int keyCode)
        {
            int actionCode = getGameAction(keyCode);
            clearActionCode(actionCode);
        }

        public  bool isActionLongPressed(int actCode)
        {
            long timeElapsed = JavaSystem.currentTimeMillis() - this.someActionStartTime;
            return (this.someActionCode == actCode) && (timeElapsed >= 400L);
        }

        public  void addActionCode(int code)
        {
            this.someActionCode = code;
            this.someActionStartTime = JavaSystem.currentTimeMillis();
            this.someActionsSum |= code;
            this.someActionSum2 |= code;
        }

        public  void clearActionCode(int paramInt)
        {
            if (paramInt == this.someActionCode)
            {
                this.someActionCode = 0;
            }
            this.someActionsSum &= (int)(paramInt ^ 0xFFFFFFFF);
        }

        public  void showFatalError(String errorMsg)
        {
            this.isRunning = false;
            Form errForm = new Form("Fatal error!");
            errForm.append(errorMsg);
            Command localCommand = new Command("Exit", 7, 1);
            errForm.addCommand(localCommand);
            errForm.setCommandListener(this);
            display.setCurrent(errForm);
        }

        public  void stopGame()
        {
            this.isRunning = false;
        }

        public static  void loadCharsSprites()
        {
            charsSprites[0] = new F_Sprite("chars");
            charsSprites[1] = new F_Sprite("lchars");
        }

        public void run()
        {
            try
            {
                repaintAll();
                settingsNames = new String[] { A_MenuBase.getLangString(26),
					A_MenuBase.getLangString(28), A_MenuBase.getLangString(25),
					A_MenuBase.getLangString(24) };

                I_Game aGame = new I_Game();
                repaintAll();
                this.mainDrawElement = aGame;
                this.isLoading = false;
                this.isRunning = true;
                aGame.runLoading();
                while (this.isRunning)
                {
                    long time = JavaSystem.currentTimeMillis();
                    if ((isShown()) && (!m_notifyShownMb))
                    {
                        if (musicPlayerId >= 0)
                        {
                            playMusicLooped(musicPlayerId, musicLoopCount);
                            if ((currentMusicPlayer != null) && (currentMusicPlayer.getState() == 400))
                            {
                                musicPlayerId = -1;
                            }
                        }
                        this.mainDrawElement.onUpdate();
                        repaintAll();
                    }
                    int timeElapsed = (int)(JavaSystem.currentTimeMillis() - time);
                    int delay = 65 - timeElapsed;
                    if (delay < 10)
                    {
                        delay = 10;
                    }
                    if (delay > 0)
                    {
                        try
                        {
                            Thread.sleep(delay);
                        }
                        catch (Exception ex1)
                        {
                            //
                        }
                    }
                }
                if (B_MainMIDlet.midlet != null)
                {
                    B_MainMIDlet.midlet.notifyDestroyed();
                    B_MainMIDlet.midlet.destroyApp(true);
                }
                return;
            }
            catch (Exception ex2)
            {
                ex2.printStackTrace();
                showFatalError(ex2.toString());
            }
        }

        public static  void vibrate(int val)
        {
            try
            {
                if (settings[1] != false)
                {
                    display.vibrate(val * 4);
                }
                return;
            }
            catch (Exception ex)
            {
                //
            }
        }

        public static  void initMusicVars()
        {
            musicPlayers = new Player[musicNames.Length];
            musicPlayersLoaded = new bool[musicNames.Length];
        }

        public static  void loadMusic(int musicId)
        {
            try
            {
                musicPlayersLoaded[musicId] = false;
                InputStream stream = getResourceStream(musicNames[musicId]
                        + ".mid");
                musicPlayers[musicId] = Manager.createPlayer(stream, "audio/midi");
                musicPlayers[musicId].realize();
                musicPlayers[musicId].prefetch();
                musicPlayersLoaded[musicId] = true;
                return;
            }
            catch (Exception ex)
            {
                //
            }
        }

        public static  void playMusicLooped2(int paramInt1, int paramInt2)
        {
            playMusicLooped(paramInt1, paramInt2);
        }

        public static  void stopMusic()
        {
            try
            {
                if (currentMusicPlayer != null)
                {
                    currentMusicPlayer.stop();
                    currentMusicPlayer = null;
                    currentMusicId = -1;
                }
                return;
            }
            catch (Exception ex)
            {
                //
            }
        }

        public static  void playMusicLooped(int musicId, int loopCount)
        {
            try
            {
                if (musicId >= musicPlayersLoaded.Length) throw new Exception("music index out of bound");
                if (musicPlayersLoaded[musicId] == false)
                {
                    return;
                }
                if (currentMusicPlayer != null)
                {
                    currentMusicPlayer.stop();
                }
                if ((someMusicByteArr[musicId] == 1) && (settings[0] != false))
                {
                    if (loopCount == 0)
                    {
                        loopCount = -1;
                    }
                    if (m_notifyShownMb)
                    {
                        musicPlayerId = musicId;
                        musicLoopCount = loopCount;
                    }
                    else
                    {
                        currentMusicPlayer = musicPlayers[musicId];
                        currentMusicPlayer.setLoopCount(loopCount);
                        currentMusicPlayer.start();
                        currentMusicId = musicId;
                        currentMusicLoopCount = loopCount;
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                //
            }
        }

        public static  void stopMusicPlayer(int index)
        {
            try
            {
                if (index >= musicPlayersLoaded.Length) throw new Exception("music index out of bound");
                if (musicPlayersLoaded[index] == false)
                {
                    return;
                }
                if (currentMusicPlayer == musicPlayers[index])
                {
                    currentMusicPlayer.stop();
                    currentMusicPlayer = null;
                    currentMusicId = -1;
                }
                return;
            }
            catch (Exception ex)
            {
                //
            }
        }

        public static  void loadResourcesPak(String pakFileName)
        {
            if (resourcesNames == null)
            {
                resourcesNames = null;
                int[] arrayOfInt1 = null;
                int[] arrayOfInt2 = null;
                InputStream stream = B_MainMIDlet.midlet.getClass().getResourceAsStream("/1.pak");
                DataInputStream resStream = new DataInputStream(stream);
                int i = resStream.readShort();
                int resLength = resStream.readShort();
                resourcesNames = new String[resLength];
                arrayOfInt1 = new int[resLength];
                arrayOfInt2 = new int[resLength];
                for (int k = 0; k < resLength; k++)
                {
                    resourcesNames[k] = resStream.readUTF();
                    arrayOfInt1[k] = (resStream.readInt() + i);
                    arrayOfInt2[k] = resStream.readShort();
                }
                resourcesData = new byte[resourcesNames.Length][];
                for (int m = 0; m < resourcesNames.Length; m++)
                {
                    resourcesData[m] = new byte[arrayOfInt2[m]];
                    resStream.readFully(resourcesData[m]);
                }
                resStream.close();
            }
        }

        public static  byte[] getResourceData(String resName)
        {
            for (int i = 0; i < resourcesNames.Length; i++)
            {
                if (resName.Equals(resourcesNames[i]))
                {
                    return resourcesData[i];
                }
            }
            return null;
        }

        public static  InputStream getResourceStream(String resName)
        {
            return new ByteArrayInputStream(getResourceData(resName));
        }

        public  void commandAction(Command paramCommand,
                Displayable paramDisplayable)
        {
            B_MainMIDlet.midlet.notifyDestroyed();
        }

        public  void showMsg(String msg, I_Game game)
        {
            D_Menu dialog = new D_Menu((byte)10, 12);
            dialog.createDescDialogMb(null, msg, E_MainCanvas.canvasWidth, -1);
            dialog.parentMenu = game;
            dialog.var_10c5 = 500;
            dialog.setMenuLoc(E_MainCanvas.canvasWidth >> 1, E_MainCanvas.canvasHeight >> 1, 3);
            this.showMenu(dialog);
        }

    }

}