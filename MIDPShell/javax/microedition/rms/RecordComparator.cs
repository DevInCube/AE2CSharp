namespace javax.microedition.rms
{

    public interface RecordComparator
    {
        public static sealed override int PRECEDES = -1;
        public static sealed override int EQUIVALENT = 0;
        public static sealed override int FOLLOWS = 1;

        int compare(byte[] paramArrayOfByte1, byte[] paramArrayOfByte2);
    }

}
