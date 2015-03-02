using java.lang;

namespace javax.microedition.lcdui
{

    public class Font
    {
        public static readonly int FACE_SYSTEM = 0;
        public static readonly int FONT_STATIC_TEXT = 0;
        public static readonly int SIZE_MEDIUM = 0;
        public static readonly int STYLE_PLAIN = 0;
        public static readonly int FONT_INPUT_TEXT = 1;
        public static readonly int STYLE_BOLD = 1;
        public static readonly int SIZE_LARGE = 16;
        public static readonly int STYLE_ITALIC = 2;
        public static readonly int FACE_MONOSPACE = 32;
        public static readonly int STYLE_UNDERLINED = 4;
        public static readonly int FACE_PROPORTIONAL = 64;
        public static readonly int SIZE_SMALL = 8;

        private int face;
        private int style;
        private int size;

        public Font(int face, int style, int size)
        {
            // TODO: Complete member initialization
            this.face = face;
            this.style = style;
            this.size = size;
            //throw IllegalArgumentException 
        }

        public bool isBold()
        {
            return false;
        }

        public bool isItalic()
        {
            return false;
        }

        public bool isPlain()
        {
            return false;
        }

        public bool isUnderlined()
        {
            return false;
        }

        public int charWidth(char paramChar)
        {
            return 0;
        }

        public int charsWidth(char[] paramArrayOfChar, int paramInt1, int paramInt2)
        {
            return 0;
        }

        public int getBaselinePosition()
        {
            return 0;
        }

        public int getFace()
        {
            return 0;
        }

        public int getHeight()
        {
            return 0;
        }

        public int getSize()
        {
            return 0;
        }

        public int getStyle()
        {
            return 0;
        }

        public int stringWidth(String paramString)
        {
            return 0;
        }

        public int substringWidth(String paramString, int paramInt1, int paramInt2)
        {
            return 0;
        }

        public static Font getDefaultFont()
        {
            return null;
        }

        public static Font getFont(int fontSpecifier)
        {
            return null;
        }

        public static Font getFont(int face, int style, int size)
        {
            return new Font(face, style, size);
        }
    }
}