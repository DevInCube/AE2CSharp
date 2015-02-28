using java.lang;
namespace javax.microedition.lcdui
{

    public class Command
    {
        public static sealed override int SCREEN = 1;
        public static sealed override int BACK = 2;
        public static sealed override int CANCEL = 3;
        public static sealed override int OK = 4;
        public static sealed override int HELP = 5;
        public static sealed override int STOP = 6;
        public static sealed override int EXIT = 7;
        public static sealed override int ITEM = 8;

        public Command(String paramString, int paramInt1, int paramInt2) { }

        public Command(String paramString1, String paramString2, int paramInt1, int paramInt2) { }

        public int getCommandType()
        {
            return 0;
        }

        public int getPriority()
        {
            return 0;
        }

        public String getLabel()
        {
            return null;
        }

        public String getLongLabel()
        {
            return null;
        }
    }


}