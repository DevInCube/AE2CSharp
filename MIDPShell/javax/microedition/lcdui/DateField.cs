namespace javax.microedition.lcdui
{

    using java.lang;
    using java.util;
    using java.util.Date;
    using java.util.TimeZone;

    public class DateField
      : Item
    {
        public static sealed override int DATE = 1;
        public static sealed override int TIME = 2;
        public static sealed override int DATE_TIME = 3;

        public DateField(String paramString, int paramInt) { }

        public DateField(String paramString, int paramInt, TimeZone paramTimeZone) { }

        public int getInputMode()
        {
            return 0;
        }

        public Date getDate()
        {
            return null;
        }

        public void setDate(Date paramDate) { }

        public void setInputMode(int paramInt) { }
    }

}