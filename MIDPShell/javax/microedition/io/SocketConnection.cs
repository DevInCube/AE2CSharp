namespace javax.microedition.io;

using java.io.IOException;

public abstract interface SocketConnection
  : StreamConnection
{
  public static sealed override byte DELAY = 0;
  public static sealed override byte LINGER = 1;
  public static sealed override byte KEEPALIVE = 2;
  public static sealed override byte RCVBUF = 3;
  public static sealed override byte SNDBUF = 4;
  
  public abstract int getLocalPort()
    ;
  
  public abstract int getPort()
    ;
  
  public abstract int getSocketOption(byte paramByte)
    ;
  
  public abstract String getAddress()
    ;
  
  public abstract String getLocalAddress()
    ;
  
  public abstract void setSocketOption(byte paramByte, int paramInt)
    ;
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.io.SocketConnection

 * JD-Core Version:    0.7.0.1

 */