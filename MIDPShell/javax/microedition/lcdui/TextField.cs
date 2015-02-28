using java.lang;
namespace javax.microedition.lcdui
{

    public class TextField
      : Item
    {
        public static sealed override int ANY = 0;
        public static sealed override int EMAILADDR = 1;
        public static sealed override int INITIAL_CAPS_WORD = 1048576;
        public static sealed override int UNEDITABLE = 131072;
        public static sealed override int NUMERIC = 2;
        public static sealed override int INITIAL_CAPS_SENTENCE = 2097152;
        public static sealed override int SENSITIVE = 262144;
        public static sealed override int PHONENUMBER = 3;
        public static sealed override int URL = 4;
        public static sealed override int DECIMAL = 5;
        public static sealed override int NON_PREDICTIVE = 524288;
        public static sealed override int CONSTRAINT_MASK = 65535;
        public static sealed override int PASSWORD = 65536;

        public TextField(String paramString1, String paramString2, int paramInt1, int paramInt2) { }

        public int getCaretPosition()
        {
            return 0;
        }

        public int getChars(char[] paramArrayOfChar)
        {
            return 0;
        }

        public int getConstraints()
        {
            return 0;
        }

        public int getMaxSize()
        {
            return 0;
        }

        public int setMaxSize(int paramInt)
        {
            return 0;
        }

        public int size()
        {
            return 0;
        }

        public String getString()
        {
            return null;
        }

        public void delete(int paramInt1, int paramInt2) { }

        public void insert(char[] paramArrayOfChar, int paramInt1, int paramInt2, int paramInt3) { }

        public void insert(String paramString, int paramInt) { }

        public void setChars(char[] paramArrayOfChar, int paramInt1, int paramInt2) { }

        public void setConstraints(int paramInt) { }

        public void setInitialInputMode(String paramString) { }

        public void setString(String paramString) { }
    }

}