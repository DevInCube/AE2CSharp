using java.lang;
using javax.microedition.lcdui;

namespace aeii
{
    public class H_ImageExt
    {
        public Image image;
        private bool isFramedImage = false;
        private int frameShiftXPix;
        private int frameShiftYPix;
        public int imageWidth;
        public int imageHeight;
        private int locationX;
        private int locationY;
        public int imageTransformation = 0;

        public H_ImageExt(H_ImageExt inImage, int framePosX, int framePosY, int imWidth, int imHeight)
        {
            this.image = inImage.image;
            this.imageWidth = imWidth;
            this.imageHeight = imHeight;
            this.frameShiftXPix = (framePosX * imWidth + inImage.frameShiftXPix);
            this.frameShiftYPix = (framePosY * imHeight + inImage.frameShiftYPix);
            this.isFramedImage = true;
        }

        public H_ImageExt(H_ImageExt image, int inTransform)
        {
            if (image == null) throw new Exception("Image is null");
            this.image = image.image;
            this.imageWidth = image.imageWidth;
            this.imageHeight = image.imageHeight;
            this.frameShiftXPix = image.frameShiftXPix;
            this.frameShiftYPix = image.frameShiftYPix;
            this.locationX = image.locationX;
            this.locationY = image.locationY;
            this.isFramedImage = image.isFramedImage;
            if ((inTransform & 0x1) != 0)
            {
                this.imageTransformation = 2;
                return;
            }
            if ((inTransform & 0x2) != 0)
            {
                this.imageTransformation = 1;
                return;
            }
            if ((inTransform & 0x4) != 0)
            {
                this.imageTransformation = 6;
                return;
            }
            if ((inTransform & 0x8) != 0)
            {
                this.imageTransformation = 3;
                return;
            }
            if ((inTransform & 0x10) != 0)
            {
                this.imageTransformation = 5;
            }
        }

        public H_ImageExt(String imgId)
        {
            byte[] imgData = E_MainCanvas.getResourceData(imgId + ".png");
            if (imgData == null) throw new Exception("No resource `" + imgId + ".png`");
            this.image = Image.createImage(imgData, 0, imgData.Length);
            this.imageWidth = ((short)this.image.getWidth());
            this.imageHeight = ((short)this.image.getHeight());
            this.isFramedImage = false;
        }

        public H_ImageExt(String imgId, int playerId)
        {
            byte[] imgData = E_MainCanvas.getResourceData(imgId + ".png");
            if (imgData == null) throw new Exception(); //@my
            if (playerId != 1)
            {
                byte[] data = new byte[imgData.Length];
                System.Array.Copy(imgData, 0, data, 0, imgData.Length);
                setPlayerColor(data, playerId);
                imgData = data;
            }
            this.image = Image.createImage((byte[])imgData, 0, imgData.Length);
            this.imageWidth = ((short)this.image.getWidth());
            this.imageHeight = ((short)this.image.getHeight());
        }

        public void applySomeTransformation(int someTransform, int inWidth, int inHeight)
        {
            if (this.isFramedImage)
            {
                return;
            }
            int i = someTransform & 0xD;
            int j = someTransform & 0x32;
            if (this.imageTransformation == 2)
            {
                if ((i & 0x4) != 0)
                {
                    i = 8;
                }
                else if ((i & 0x8) != 0)
                {
                    i = 4;
                }
            }
            else if (this.imageTransformation == 1)
            {
                if ((j & 0x10) != 0)
                {
                    i = 32;
                }
                else if ((i & 0x20) != 0)
                {
                    i = 16;
                }
            }
            if (((someTransform = i | j) & 0x8) != 0)
            {
                this.locationX = (inWidth - this.imageWidth);
            }
            else if ((someTransform & 0x1) != 0)
            {
                this.locationX = (inWidth - this.imageWidth >> 1);
            }
            if ((someTransform & 0x20) != 0)
            {
                this.locationY = (inHeight - this.imageHeight);
                return;
            }
            if ((someTransform & 0x2) != 0)
            {
                this.locationY = (inHeight - this.imageHeight >> 1);
            }
        }

        public void translateImage(int inX, int inY)
        {
            this.locationX += inX;
            this.locationY += inY;
        }

        public void drawImage(Graphics gr, int inX, int inY)
        {
            drawImageAnchored(gr, inX, inY, Graphics.TOP | Graphics.LEFT);
        }

        public void drawImageAnchored(Graphics gr, int inX, int inY, int inAnchor)
        {
            int x_dest = inX + this.locationX;
            int y_dest = inY + this.locationY;
            if ((this.isFramedImage) || (this.imageTransformation != 0))
            {
                int x_src = this.frameShiftXPix;
                int y_src = this.frameShiftYPix;
                gr.drawRegion(this.image, x_src, y_src,
                              this.imageWidth, this.imageHeight,
                              this.imageTransformation, x_dest, y_dest,
                              inAnchor);
                return;
            }
            gr.drawImage(this.image, x_dest, y_dest, inAnchor);
        }

        public static void setPlayerColor(byte[] imageData, int playerId)
        {
            try
            {
                int i = 33;
                int it = 0;
                int Length3 = imageData.Length - 3;
                while (it < Length3)
                {
                    if ((imageData[it] == 80)
                        && (imageData[(it + 1)] == 76)
                        && (imageData[(it + 2)] == 84))
                    {
                        i = it - 4;
                        break;
                    }
                    it++;
                }
                it = i;
                Length3 = (int)(((imageData[it] & 0xFF) << 24
                        | (imageData[(it + 1)] & 0xFF) << 16
                        | (imageData[(it + 2)] & 0xFF) << 8 | imageData[(it + 3)] & 0xFF) & 0xFFFFFFFF);
                it += 4;
                int m = -1;
                for (int n = 0; n < 4; n++)
                {
                    m = someColorComponentTransform(imageData[(it + n)], m);
                }
                it += 4;
                for (int colorIndex = it; colorIndex < it + Length3; colorIndex += 3)
                {
                    int r = imageData[colorIndex] & 0xFF;
                    int g = imageData[(colorIndex + 1)] & 0xFF;
                    int b = imageData[(colorIndex + 2)] & 0xFF;
                    if (playerId == 0) //gray
                    {
                        if ((r != 244) || (g != 244) || (b != 230))
                        {
                            int grayComponent = (r + g + b) / 3;
                            r = grayComponent;
                            g = grayComponent;
                            b = grayComponent;
                            imageData[colorIndex] = ((byte)r);
                            imageData[(colorIndex + 1)] = ((byte)g);
                            imageData[(colorIndex + 2)] = ((byte)b);
                        }
                    }
                    else if (playerId != 1)
                    {
                        int[][] blueColors = I_Game.playerAlphaColors[1]; //blue
                        int[][] playColors = I_Game.playerAlphaColors[playerId]; //player
                        for (int cIt = 0; cIt < blueColors.Length; cIt++)
                        {
                            if ((blueColors[cIt][0] == r)
                                && (blueColors[cIt][1] == g)
                                && (blueColors[cIt][2] == b))
                            {
                                imageData[colorIndex] = ((byte)playColors[cIt][0]);
                                imageData[(colorIndex + 1)] = ((byte)playColors[cIt][1]);
                                imageData[(colorIndex + 2)] = ((byte)playColors[cIt][2]);
                                break;
                            }
                        }
                    }
                    m = someColorComponentTransform(imageData[colorIndex], m);
                    m = someColorComponentTransform(imageData[(colorIndex + 1)], m);
                    m = someColorComponentTransform(imageData[(colorIndex + 2)], m);
                }
                m = (int)(m ^ 0xFFFFFFFF);
                int colorIndex2 = i + 8 + Length3;
                imageData[colorIndex2] = ((byte)(m >> 24));
                imageData[(colorIndex2 + 1)] = ((byte)(m >> 16));
                imageData[(colorIndex2 + 2)] = ((byte)(m >> 8));
                imageData[(colorIndex2 + 3)] = ((byte)m);
                return;
            }
            catch (Exception ex2)
            {
                ex2.printStackTrace();
            }
        }

        public static int someColorComponentTransform(byte colorComponent, int paramInt)
        {
            int i = colorComponent & 0xFF;
            paramInt ^= i;
            for (int j = 0; j < 8; j++)
            {
                if ((paramInt & 0x1) != 0)
                {
                    paramInt = (int)(paramInt >> 1 ^ 0xEDB88320); //>>>
                }
                else
                {
                    paramInt >>= 1; //>>>
                }
            }
            return paramInt;
        }
    }

}