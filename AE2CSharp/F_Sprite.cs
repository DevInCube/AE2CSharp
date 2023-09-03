using javax.microedition.lcdui;
using java.io;
using java.lang;

namespace aeii
{
    public class F_Sprite
    {
        public H_ImageExt[] frameImages;
        private byte[] frameSequence;
        public int currentFrameIndex = 0;
        public int posXPixel = 0;
        public int posYPixel = 0;
        public bool m_applyAnchorMb = true;
        public int frameWidth;
        public int frameHeight;
        public byte[][] frameAnimationsSequences;
        public int someYVal1;
        public byte m_spriteType = 0;
        public int var_81c = -1;
        public int frameTime;
        public int mapFrameTime;
        public int someXVal3;
        public int someYOffset3;
        public int someYVal3;
        public bool isUpdatingMb = true;
        public bool var_854;
        public bool var_85c;
        public int randPosCounter = -1;
        public sbyte var_86c = -1;
        public bool m_isRotating;
        public int spriteMovingStepMb;
        public String spriteString;
        public int charFontId;
        public F_Sprite kingHeadSprite;
        public F_Sprite kingBackSprite;
        public int[][] someUnusedArr2;
        public short[][] somePlayersDataS;
        public int someColor = 16769024; //#FFE000 yellow
        public byte[] someRandUnusedArr;
        public bool[] someAlwaysTrueArr;

        public F_Sprite(String spriteId)
        {
            loadSprite(spriteId, 1);
        }

        public F_Sprite(H_ImageExt[] images)
        {
            this.frameImages = images;
            this.frameSequence = new byte[this.frameImages.Length];
            for (byte index = 0; index < this.frameImages.Length; index = (byte)(index + 1))
            {
                this.frameSequence[index] = index;
            }
            this.frameWidth = this.frameImages[0].imageWidth;
            this.frameHeight = this.frameImages[0].imageHeight;
        }

        public F_Sprite(String spriteId, sbyte paramByte)
        {
            loadSprite(spriteId, paramByte);
        }

        private void loadSprite(String spriteId, int paramInt)
        {
            InputStream stream = E_MainCanvas.getResourceStream(spriteId + ".sprite");
            int framesCount = (byte)stream.read();
            this.frameWidth = ((byte)stream.read());
            this.frameHeight = ((byte)stream.read());
            this.frameImages = new H_ImageExt[framesCount];
            H_ImageExt[] images = new H_ImageExt[framesCount];
            try
            {
                H_ImageExt spriteFrame = new H_ImageExt(spriteId, paramInt);
                int numberOfFramesX = spriteFrame.imageWidth / this.frameWidth;
                int numberOfFramesY = spriteFrame.imageHeight / this.frameHeight;
                int i1 = 0;
                for (int i2 = 0; i2 < numberOfFramesY; i2++)
                {
                    for (int i3 = 0; i3 < numberOfFramesX; i3++)
                    {
                        if (i1 >= images.Length) throw new Exception(); //
                        images[i1] = new H_ImageExt(spriteFrame,
                                i3, i2, this.frameWidth, this.frameHeight);
                        i1++;
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    for (int it = 0; it < framesCount; it++)
                    {
                        StringBuffer tileName = new StringBuffer(spriteId);
                        tileName.append('_');
                        if (it < 10)
                        {
                            tileName.append('0');
                        }
                        tileName.append(it);
                        if (paramInt == 1)
                        {
                            images[it] = new H_ImageExt(
                                    tileName.toString());
                        }
                        else
                        {
                            images[it] = new H_ImageExt(
                                    tileName.toString(), paramInt);
                        }
                    }
                }
                catch (Exception ex2)
                {
                    //
                }
            }
            for (int j = 0; j < framesCount; j++)
            {
                int frameIndex = stream.read();
                int frameTransform = stream.read();
                this.frameImages[j] = new H_ImageExt(images[frameIndex], frameTransform);
            }
            sbyte framesTransformation = (sbyte)stream.read();
            if (framesTransformation > 0)
            {
                for (int it = 0; it < framesCount; it++)
                {
                    this.frameImages[it].applySomeTransformation(framesTransformation, this.frameWidth, this.frameHeight);
                }
            }
            sbyte animationsCount = (sbyte)stream.read();
            if (animationsCount > 0)
            {
                this.frameAnimationsSequences = new byte[animationsCount][];
                byte fTime = stream.read();
                this.mapFrameTime = (fTime * 50);
                for (int animId = 0; animId < animationsCount; animId++)
                {
                    int animLength = stream.read();
                    this.frameAnimationsSequences[animId] = new byte[animLength];
                    for (int it = 0; it < animLength; it++)
                    {
                        this.frameAnimationsSequences[animId][it] = ((byte)stream.read());
                    }
                }
            }
            for (int fIt = 0; fIt < framesCount; fIt++)
            {
                sbyte iX = (sbyte)stream.read();
                sbyte iY = (sbyte)stream.read();
                if ((iX == -1) || (iY == -1))
                {
                    break;
                }
                this.frameImages[fIt].translateImage(iX, iY);
            }
            stream.close();
            if (this.frameAnimationsSequences != null)
            {
                this.frameSequence = this.frameAnimationsSequences[0];
                return;
            }
            this.frameSequence = new byte[framesCount];
            for (byte n1 = 0; n1 < framesCount; n1 = (byte)(n1 + 1))
            {
                this.frameSequence[n1] = n1;
            }
        }

        public F_Sprite(F_Sprite sprite)
        {
            this.frameImages = sprite.frameImages;
            this.frameSequence = sprite.frameSequence;
            this.currentFrameIndex = sprite.currentFrameIndex;
            this.posXPixel = sprite.posXPixel;
            this.posYPixel = sprite.posYPixel;
            this.someYVal1 = sprite.someYVal1;
            this.m_applyAnchorMb = sprite.m_applyAnchorMb;
            this.frameWidth = sprite.frameWidth;
            this.frameHeight = sprite.frameHeight;
            this.mapFrameTime = sprite.mapFrameTime;
            this.frameAnimationsSequences = sprite.frameAnimationsSequences;
        }

        public F_Sprite(int width, int height)
        {
            this.frameWidth = width;
            this.frameHeight = height;
        }

        public int getFrameSequenceLength()
        {
            return this.frameSequence.Length;
        }

        public int getFramesCount()
        {
            return this.frameImages.Length;
        }

        public void setCurrentFrameIndex(int val)
        {
            if (val < this.frameSequence.Length)
            {
                this.currentFrameIndex = ((byte)val);
            }
        }

        public void setSpritePosition(int pX, int pY)
        {
            this.posXPixel = ((short)pX);
            this.posYPixel = ((short)pY);
        }

        public void nextFrame()
        {
            this.currentFrameIndex += 1;
            if (this.currentFrameIndex >= this.frameSequence.Length)
            {
                this.currentFrameIndex = 0;
            }
        }

        public void setFrameSequence(byte[] data)
        {
            this.frameSequence = data;
            this.currentFrameIndex = 0;
            this.frameTime = 0;
        }

        public void startAnimation(int animationIndex, bool inBool)
        {
            if ((this.frameAnimationsSequences != null) && (animationIndex <= this.frameAnimationsSequences.Length))
            {
                byte[] frameSeq = this.frameAnimationsSequences[animationIndex];
                if (inBool)
                {
                    byte[] arrayOfByte = new byte[frameSeq.Length];
                    for (int i = 0; i < arrayOfByte.Length; i++)
                    {
                        arrayOfByte[i] = ((byte)(frameSeq[i] + getFramesCount() / 2));
                    }
                    frameSeq = arrayOfByte;
                }
                setFrameSequence((byte[])frameSeq);
            }
        }

        public void drawFrameAt(Graphics gr, int frameIndex, int inX, int inY, int anchor)
        {
            if ((this.m_spriteType == 2) || (this.m_spriteType == 4) || (this.m_spriteType == 3))
            {
                onSpritePaint(gr, inX, inY);
                return;
            }
            if (this.m_applyAnchorMb)
            {
                int x = this.posXPixel + inX;
                int y = this.posYPixel + inY;
                this.frameImages[frameIndex].drawImageAnchored(gr, x, y, anchor);
            }
        }

        public void drawCurrentFrame(Graphics gr, int inX, int inY, int anchor)
        {
            byte index = (byte)this.frameSequence[this.currentFrameIndex];
            drawFrameAt(gr, index, inX, inY, anchor);
        }



        public static F_Sprite createBouncingText(String str, int inX, int inY, byte charId)
        {
            int strWidth = E_MainCanvas.getCharedStringWidth(charId, str);
            int strHeight = E_MainCanvas.getCharedStringHeight(charId);
            F_Sprite sprite = new F_Sprite(strWidth, strHeight);
            sprite.charFontId = charId;
            sprite.spriteString = str;
            sprite.someXVal3 = inX;
            sprite.someYVal3 = inY;
            sprite.m_spriteType = 5;
            return sprite;
        }

        public static F_Sprite spriteCopy(F_Sprite sprite,
                int paramInt1, int paramInt2, int paramInt3, int paramInt4,
                int frameTime, byte inSprType)
        {
            F_Sprite lspr = null;
            if (sprite != null)
            {
                lspr = new F_Sprite(sprite);
            }
            else
            {
                lspr = new F_Sprite(0, 0);
                if ((inSprType == 2) || (inSprType == 4))
                {
                    if (inSprType == 4)
                    {
                        lspr.someColor = 15658751;
                    }
                    lspr.someUnusedArr2 = new int[5][];
                    lspr.somePlayersDataS = new short[5][];
                    for (int i = 0; i < 5; i++)
                    {
                        lspr.someUnusedArr2[i] = new int[2];
                        lspr.somePlayersDataS[i] = new short[2];
                    }
                    lspr.someRandUnusedArr = new byte[5];
                    lspr.someAlwaysTrueArr = new bool[5];
                    for (int k = 0; k < 5; k++)
                    {
                        lspr.someAlwaysTrueArr[k] = true;
                        if (inSprType == 4)
                        {
                            lspr.somePlayersDataS[k][0] = ((short)(E_MainCanvas.random.nextInt() % 4 << 10));
                            lspr.somePlayersDataS[k][1] = ((short)(E_MainCanvas.random.nextInt() % 4 << 10));
                        }
                        else
                        {
                            lspr.somePlayersDataS[k][0] = ((short)(Math.abs(E_MainCanvas.random.nextInt()) % 8192 + -4096));
                            lspr.somePlayersDataS[k][1] = ((short)(Math.abs(E_MainCanvas.random.nextInt()) % 4096 + -2048));
                        }
                        lspr.someRandUnusedArr[k] = ((byte)(Math
                                .abs(E_MainCanvas.random.nextInt()) % 2 + 1));
                    }
                }
            }
            lspr.m_spriteType = inSprType;
            lspr.var_81c = paramInt4;
            lspr.mapFrameTime = frameTime;
            lspr.someXVal3 = paramInt1;
            lspr.someYOffset3 = paramInt2;
            lspr.someYVal3 = paramInt3;
            lspr.var_854 = true;
            return lspr;
        }

        public void sub_19ce()
        {
            if (this.m_spriteType != 4)
            {
                this.someColor += -263168;
            }
            for (int i = 0; i < 5; i++)
            {
                if (this.someAlwaysTrueArr[i] != false)
                {
                    if (this.m_spriteType == 4)
                    {
                        this.someUnusedArr2[i][0] += this.somePlayersDataS[i][0];
                        this.someUnusedArr2[i][1] += this.somePlayersDataS[i][1];
                        if (this.somePlayersDataS[i][0] < 0)
                        {
                            int tmp102_101 = 0;
                            short[] tmp102_100 = this.somePlayersDataS[i];
                            tmp102_100[tmp102_101] = ((short)(tmp102_100[tmp102_101] + 256));
                        }
                        else if (this.somePlayersDataS[i][0] > 0)
                        {
                            int tmp131_130 = 0;
                            short[] tmp131_129 = this.somePlayersDataS[i];
                            tmp131_129[tmp131_130] = ((short)(tmp131_129[tmp131_130] - 256));
                        }
                        if (this.somePlayersDataS[i][1] < 0)
                        {
                            int tmp157_156 = 1;
                            short[] tmp157_155 = this.somePlayersDataS[i];
                            tmp157_155[tmp157_156] = ((short)(tmp157_155[tmp157_156] + 256));
                        }
                        else if (this.somePlayersDataS[i][1] > 0)
                        {
                            int tmp186_185 = 1;
                            short[] tmp186_184 = this.somePlayersDataS[i];
                            tmp186_184[tmp186_185] = ((short)(tmp186_184[tmp186_185] - 256));
                        }
                    }
                    else
                    {
                        this.someUnusedArr2[i][0] += this.somePlayersDataS[i][0];
                        this.someUnusedArr2[i][1] += this.somePlayersDataS[i][1];
                        int tmp242_241 = 1;
                        short[] tmp242_240 = this.somePlayersDataS[i];
                        tmp242_240[tmp242_241] = ((short)(tmp242_240[tmp242_241] + 256));
                    }
                }
            }
            if (this.frameTime >= this.mapFrameTime)
            {
                this.isUpdatingMb = false;
            }
        }

        //@Virtual
        public void spriteUpdate()
        {
            if (this.isUpdatingMb)
            {
                this.frameTime += 50;
                if (this.randPosCounter >= 0)
                {
                    this.randPosCounter -= 1;
                }
                switch (this.m_spriteType)
                {
                    case 2:
                    case 4:
                        sub_19ce();
                        return;
                    case 3:
                        setSpritePosition(this.posXPixel + this.someXVal3, this.posYPixel + this.someYOffset3);
                        return;
                    case 6:
                        this.currentFrameIndex = ((this.currentFrameIndex + 1) % 2);
                        if (this.frameTime >= this.mapFrameTime)
                        {
                            this.isUpdatingMb = false;
                            return;
                        }
                        break;
                    case 5:
                        if (this.var_81c == -1)
                        {
                            setSpritePosition(this.posXPixel + this.someXVal3, this.posYPixel);
                            this.someYVal1 += this.someYVal3;
                            if (this.someYVal1 >= 0)
                            {
                                this.someYVal1 = 0;
                                this.someYVal3 = (-this.someYVal3 / 2);
                                if (this.someYVal3 == 0)
                                {
                                    this.var_81c = 1;
                                    this.frameTime = 0;
                                }
                            }
                            else
                            {
                                this.someYVal3 += 1;
                            }
                        }
                        else if (this.frameTime >= 400)
                        {
                            this.isUpdatingMb = false;
                            return;
                        }
                        break;
                    default:
                        setSpritePosition(this.posXPixel + this.someXVal3, this.posYPixel + this.someYOffset3);
                        this.someYVal1 += this.someYVal3;
                        if ((this.var_81c != 0) && (this.frameTime >= this.mapFrameTime))
                        {
                            nextFrame();
                            if ((this.m_spriteType == 0) && (this.currentFrameIndex == 0)
                                    && (this.var_81c > 0))
                            {
                                this.var_81c -= 1;
                                if (this.var_81c <= 0)
                                {
                                    setCurrentFrameIndex(getFrameSequenceLength() - 1);
                                    if (this.var_854)
                                    {
                                        this.isUpdatingMb = false;
                                    }
                                }
                            }
                            this.frameTime = 0;
                        }
                        break;
                }
            }
        }

        //@Virtual
        public void onSpritePaint(Graphics gr, int inX, int inY)
        {
            int k = 0;
            if ((this.m_spriteType == 2) || (this.m_spriteType == 4))
            {
                gr.setColor(this.someColor);
                k = 0;
            }

            //@todo !!! this was fatal for all units!
            /*
            while (k < 5) {
                int x;
                int y;
                if (this.var_8c4[k] != false) {
                    x = (this.var_8a4[k][0] >> 10) + inX + this.posXPixel;
                    y = (this.var_8a4[k][1] >> 10) + inY + this.posYPixel;
                    gr.fillRect(x, y, this.var_8bc[k], this.var_8bc[k]);
                }
                k++;
            }*/

            if (this.m_spriteType == 6)
            {
                int x = 0;
                if (this.currentFrameIndex == 0)
                {
                    gr.setColor(15718144); // #EFD700 sand yellow
                }
                else
                {
                    gr.setColor(16777215); // white
                }
                if (this.someXVal3 > 0)
                {
                    int y = this.posXPixel + 15;
                    gr.fillArc(this.posXPixel, this.posYPixel - 15, 30,
                            30, 0, 360);
                    gr.fillRect(y, this.posYPixel - 15,
                            E_MainCanvas.canvasWidth - y, 30);
                    return;
                }
                gr.fillArc(this.posXPixel - 30, this.posYPixel - 15, 30,
                        30, 0, 360);
                gr.fillRect(0, this.posYPixel - 15, this.posXPixel - 15,
                        30);
                return;
            }
            if (this.m_spriteType == 3)
            {
                gr.setColor(0); //black
                if (this.someXVal3 > 0)
                {
                    gr.drawLine(this.posXPixel, this.posYPixel,
                            this.posXPixel + 4, this.posYPixel - 2);
                    return;
                }
                gr.drawLine(this.posXPixel - 4, this.posYPixel - 2,
                        this.posXPixel, this.posYPixel);
                return;
            }
            if (this.m_applyAnchorMb)
            {
                int x = this.posXPixel + inX;
                int y = this.posYPixel + inY;
                if (this.spriteString != null)
                {
                    E_MainCanvas.drawCharedString(gr, this.spriteString, x, y, this.charFontId, Graphics.HCENTER | Graphics.BOTTOM);
                    return;
                }
                if (this.randPosCounter > 0)
                {
                    x += E_MainCanvas.getRandomWithin(-4, 5);
                    y += E_MainCanvas.getRandomWithin(-1, 2);
                }
                k = this.frameSequence[this.currentFrameIndex];
                this.frameImages[k].drawImage(gr, x, y);
                if (this.kingHeadSprite != null)
                {
                    int m = k % (getFramesCount() / 2);
                    F_Sprite sprite;
                    if (m == 2)
                    {
                        sprite = this.kingBackSprite;
                    }
                    else
                    {
                        sprite = this.kingHeadSprite;
                        sprite.setCurrentFrameIndex(m);
                    }
                    sprite.onSpritePaint(gr, x, y);
                }
            }


        }
    }

}