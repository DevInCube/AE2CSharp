namespace javax.microedition.io
{

    using java.io.IOException;
    using java.lang;

    public interface ServerSocketConnection : StreamConnectionNotifier
    {
        int getLocalPort()
          ;

        String getLocalAddress()
          ;
    }


}