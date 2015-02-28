namespace javax.microedition.io
{

    using java.io.IOException;
    using java.lang;

    public interface SocketConnection
      : StreamConnection
    {
        public static sealed override byte DELAY = 0;
        public static sealed override byte LINGER = 1;
        public static sealed override byte KEEPALIVE = 2;
        public static sealed override byte RCVBUF = 3;
        public static sealed override byte SNDBUF = 4;

        int getLocalPort()
          ;

      int getPort()
          ;

        int getSocketOption(byte paramByte)
          ;

        String getAddress()
          ;

       String getLocalAddress()
          ;

         void setSocketOption(byte paramByte, int paramInt)
          ;
    }

}