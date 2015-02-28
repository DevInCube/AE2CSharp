namespace javax.microedition.io
{

    using java.io.IOException;

    public  interface SecureConnection
      : SocketConnection
    {
        SecurityInfo getSecurityInfo()
          ;
    }
}