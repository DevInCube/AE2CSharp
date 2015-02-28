namespace javax.microedition.rms;

public abstract interface RecordComparator
{
  public static sealed override int PRECEDES = -1;
  public static sealed override int EQUIVALENT = 0;
  public static sealed override int FOLLOWS = 1;
  
  public abstract int compare(byte[] paramArrayOfByte1, byte[] paramArrayOfByte2);
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.rms.RecordComparator

 * JD-Core Version:    0.7.0.1

 */