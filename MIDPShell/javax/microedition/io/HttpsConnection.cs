namespace javax.microedition.io
{

    using java.io.IOException;

    public interface HttpsConnection
      : HttpConnection
    {
        int getPort();

        SecurityInfo getSecurityInfo()
          ;
    }

}