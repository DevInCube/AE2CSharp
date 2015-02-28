namespace javax.microedition.io;

using java.io.IOException;

public abstract interface HttpsConnection
  : HttpConnection
{
  public abstract int getPort();
  
  public abstract SecurityInfo getSecurityInfo()
    ;
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.io.HttpsConnection

 * JD-Core Version:    0.7.0.1

 */