using java.lang;
using java.csharp;
using javax.microedition.lcdui;

namespace aeii
{

    public class H_ImageExt
    {

        public Image image;
        private bool var_4c3 = false;
        private int var_4cb;
        private int var_4d3;
        public int imageWidth;
        public int imageHeight;
        private int locationX;
        private int locationY;
        public int imageTransformation = 0;

        public H_ImageExt(H_ImageExt image, int paramInt1, int paramInt2, int imWidth, int imHeight)
        {
            this.image = image.image;
            this.imageWidth = imWidth;
            this.imageHeight = imHeight;
            this.var_4cb = (paramInt1 * imWidth + image.var_4cb);
            this.var_4d3 = (paramInt2 * imHeight + image.var_4d3);
            this.var_4c3 = true;
        }

        public H_ImageExt(H_ImageExt image, int paramInt)
        {
            if (image == null) throw new Exception("Image is null");
            this.image = image.image;
            this.imageWidth = image.imageWidth;
            this.imageHeight = image.imageHeight;
            this.var_4cb = image.var_4cb;
            this.var_4d3 = image.var_4d3;
            this.locationX = image.locationX;
            this.locationY = image.locationY;
            this.var_4c3 = image.var_4c3;
            if ((paramInt & 0x1) != 0)
            {
                this.imageTransformation = 2;
                return;
            }
            if ((paramInt & 0x2) != 0)
            {
                this.imageTransformation = 1;
                return;
            }
            if ((paramInt & 0x4) != 0)
            {
                this.imageTransformation = 6;
                return;
            }
            if ((paramInt & 0x8) != 0)
            {
                this.imageTransformation = 3;
                return;
            }
            if ((paramInt & 0x10) != 0)
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
            this.var_4c3 = false;
        }

        public H_ImageExt(String imgId, int playerId)
        {
            byte[] imgData = E_MainCanvas.getResourceData(imgId + ".png");
            if (imgData == null) throw new Exception(); //
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

        public void sub_6d9(int paramInt1, int paramInt2, int paramInt3)
        {
            if (this.var_4c3)
            {
                return;
            }
            int i = paramInt1 & 0xD;
            int j = paramInt1 & 0x32;
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
            if (((paramInt1 = i | j) & 0x8) != 0)
            {
                this.locationX = (paramInt2 - this.imageWidth);
            }
            else if ((paramInt1 & 0x1) != 0)
            {
                this.locationX = (paramInt2 - this.imageWidth >> 1);
            }
            if ((paramInt1 & 0x20) != 0)
            {
                this.locationY = (paramInt3 - this.imageHeight);
                return;
            }
            if ((paramInt1 & 0x2) != 0)
            {
                this.locationY = (paramInt3 - this.imageHeight >> 1);
            }
        }

        public void translateImage(int inX, int inY)
        {
            this.locationX += inX;
            this.locationY += inY;
        }

        public void drawImageExt(Graphics gr, int inX,
                int inY)
        {
            drawImageExt(gr, inX, inY, 20);
        }

        public void drawImageExt(Graphics gr, int inX, int inY, int inAnchor)
        {
            int x_dest = inX + this.locationX;
            int y_dest = inY + this.locationY;
            if ((this.var_4c3) || (this.imageTransformation != 0))
            {
                int x_src = this.var_4cb;
                int y_src = this.var_4d3;
                gr.drawRegion(this.image, x_src,  y_src,
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
                    m = sub_cab(imageData[(it + n)], m);
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
                    m = sub_cab(imageData[colorIndex], m);
                    m = sub_cab(imageData[(colorIndex + 1)], m);
                    m = sub_cab(imageData[(colorIndex + 2)], m);
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

        public static int sub_cab(byte paramByte, int paramInt)
        {
            int i = paramByte & 0xFF;
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