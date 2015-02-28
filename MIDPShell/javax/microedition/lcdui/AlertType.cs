namespace javax.microedition.lcdui
{

    public class AlertType
    {
        public static readonly AlertType ALARM = null;
        public static readonly AlertType CONFIRMATION = null;
        public static readonly AlertType ERROR = null;
        public static readonly AlertType INFO = null;
        public static readonly AlertType WARNING = null;

        public bool playSound(Display paramDisplay)
        {
            return false;
        }
    }


}