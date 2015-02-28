using java.io;
using java.lang;

namespace javax.microedition.lcdui
{

    public class Image
    {
        public bool isMutable()
        {
            return false;
        }

        public int getHeight()
        {
            return 0;
        }

        public int getWidth()
        {
            return 0;
        }

        public Graphics getGraphics()
        {
            return null;
        }

        public static Image createImage(byte[] paramArrayOfByte, int paramInt1, int paramInt2)
        {
            return null;
        }

        public static Image createImage(int paramInt1, int paramInt2)
        {
            return null;
        }

        public static Image createImage(InputStream paramInputStream)
        {
            return null;
        }

        public static Image createImage(String paramString)
        {
            return null;
        }

        public static Image createImage(Image paramImage)
        {
            return null;
        }

        public static Image createImage(Image paramImage, int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5)
        {
            return null;
        }

        public static Image createRGBImage(int[] paramArrayOfInt, int paramInt1, int paramInt2, bool paramBoolean)
        {
            return null;
        }

        public void getRGB(int[] paramArrayOfInt, int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6) { }
    }

}