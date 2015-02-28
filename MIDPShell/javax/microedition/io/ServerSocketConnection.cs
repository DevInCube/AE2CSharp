namespace javax.microedition.io;

using java.io.IOException;

public abstract interface ServerSocketConnection
  : StreamConnectionNotifier
{
  public abstract int getLocalPort()
    ;
  
  public abstract String getLocalAddress()
    ;
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.io.ServerSocketConnection

 * JD-Core Version:    0.7.0.1

 */