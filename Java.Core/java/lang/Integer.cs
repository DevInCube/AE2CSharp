using java.lang;
namespace java.lang
{

    public  class Integer : Object
    {
        public static readonly int MIN_VALUE = -2147483648;
        public static readonly int MAX_VALUE = 2147483647;

        private int value;

        public Integer(int paramInt) { this.value = paramInt; }

        public bool equals(Object paramObject)
        {
            if (!(paramObject is Integer)) return false;
            Integer int2 = paramObject as Integer;
            return int2.value == value;
        }

        public byte byteValue()
        {
            return 0;
        }

        public int hashCode()
        {
            return 0;
        }

        public int intValue()
        {
            return this.value;
        }

        public String toString()
        {
            return null;
        }

        public long longValue()
        {
            return 0L;
        }

        public short shortValue()
        {
            return 0;
        }

        public static int parseInt(String paramString)
        {
            return int.Parse(paramString.ToString());
        }

        public static int parseInt(String paramString, int paramInt)
        {
            return 0;
        }

        public static Integer valueOf(String paramString)
        {
            return null;
        }

        public static Integer valueOf(String paramString, int paramInt)
        {
            return null;
        }

        public static String toBinaryString(int paramInt)
        {
            return null;
        }

        public static String toHexString(int paramInt)
        {
            return null;
        }

        public static String toOctalString(int paramInt)
        {
            return null;
        }

        public static String toString(int paramInt)
        {
            return paramInt.ToString();
        }

        public static String toString(int paramInt1, int paramInt2)
        {
            return null;
        }
    }

}