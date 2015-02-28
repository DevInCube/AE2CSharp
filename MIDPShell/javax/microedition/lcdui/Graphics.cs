using java.lang;
namespace javax.microedition.lcdui
{

    public class Graphics
    {
        public static sealed override int SOLID = 0;
        public static sealed override int DOTTED = 1;
        public static sealed override int HCENTER = 1;
        public static sealed override int TOP = 16;
        public static sealed override int VCENTER = 2;
        public static sealed override int BOTTOM = 32;
        public static sealed override int LEFT = 4;
        public static sealed override int BASELINE = 64;
        public static sealed override int RIGHT = 8;

        public int getBlueComponent()
        {
            return 0;
        }

        public int getClipHeight()
        {
            return 0;
        }

        public int getClipWidth()
        {
            return 0;
        }

        public int getClipX()
        {
            return 0;
        }

        public int getClipY()
        {
            return 0;
        }

        public int getColor()
        {
            return 0;
        }

        public int getDisplayColor(int paramInt)
        {
            return 0;
        }

        public int getGrayScale()
        {
            return 0;
        }

        public int getGreenComponent()
        {
            return 0;
        }

        public int getRedComponent()
        {
            return 0;
        }

        public int getStrokeStyle()
        {
            return 0;
        }

        public int getTranslateX()
        {
            return 0;
        }

        public int getTranslateY()
        {
            return 0;
        }

        public Font getFont()
        {
            return null;
        }

        public void clipRect(int paramInt1, int paramInt2, int paramInt3, int paramInt4) { }

        public void copyArea(int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6, int paramInt7) { }

        public void drawArc(int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6) { }

        public void drawChar(char paramChar, int paramInt1, int paramInt2, int paramInt3) { }

        public void drawChars(char[] paramArrayOfChar, int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5) { }

        public void drawImage(Image paramImage, int paramInt1, int paramInt2, int paramInt3) { }

        public void drawLine(int paramInt1, int paramInt2, int paramInt3, int paramInt4) { }

        public void drawRGB(int[] paramArrayOfInt, int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6, bool paramBoolean) { }

        public void drawRect(int paramInt1, int paramInt2, int paramInt3, int paramInt4) { }

        public void drawRegion(Image paramImage, int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6, int paramInt7, int paramInt8) { }

        public void drawRoundRect(int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6) { }

        public void drawString(String paramString, int paramInt1, int paramInt2, int paramInt3) { }

        public void drawSubstring(String paramString, int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5) { }

        public void fillArc(int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6) { }

        public void fillRect(int paramInt1, int paramInt2, int paramInt3, int paramInt4) { }

        public void fillRoundRect(int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6) { }

        public void fillTriangle(int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6) { }

        public void setClip(int paramInt1, int paramInt2, int paramInt3, int paramInt4) { }

        public void setColor(int paramInt) { }

        public void setColor(int paramInt1, int paramInt2, int paramInt3) { }

        public void setFont(Font paramFont) { }

        public void setGrayScale(int paramInt) { }

        public void setStrokeStyle(int paramInt) { }

        public void translate(int paramInt1, int paramInt2) { }
    }

}