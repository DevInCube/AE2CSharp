using java.lang;
namespace javax.microedition.lcdui
{

    public class TextField
      : Item
    {
        public static readonly int ANY = 0;
        public static readonly int EMAILADDR = 1;
        public static readonly int INITIAL_CAPS_WORD = 1048576;
        public static readonly int UNEDITABLE = 131072;
        public static readonly int NUMERIC = 2;
        public static readonly int INITIAL_CAPS_SENTENCE = 2097152;
        public static readonly int SENSITIVE = 262144;
        public static readonly int PHONENUMBER = 3;
        public static readonly int URL = 4;
        public static readonly int DECIMAL = 5;
        public static readonly int NON_PREDICTIVE = 524288;
        public static readonly int CONSTRAINT_MASK = 65535;
        public static readonly int PASSWORD = 65536;

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