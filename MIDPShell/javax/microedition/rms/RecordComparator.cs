namespace javax.microedition.rms
{

    public interface RecordComparator 
    {/*
        public static readonly int PRECEDES = -1;
        public static readonly int EQUIVALENT = 0;
        public static readonly int FOLLOWS = 1;
        */
        int compare(byte[] paramArrayOfByte1, byte[] paramArrayOfByte2);
    }

}
