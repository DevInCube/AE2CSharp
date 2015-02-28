namespace javax.microedition.io;

public abstract interface CommConnection
  : StreamConnection
{
  public abstract int getBaudRate();
  
  public abstract int setBaudRate(int paramInt);
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.io.CommConnection

 * JD-Core Version:    0.7.0.1

 */