using java.lang;
namespace javax.microedition.media
{

    public interface Player : Controllable
    {
        /*
        public static readonly long TIME_UNKNOWN = -1L;
        public static readonly int CLOSED = 0;
        public static readonly int UNREALIZED = 100;
        public static readonly int REALIZED = 200;
        public static readonly int PREFETCHED = 300;
        public static readonly int STARTED = 400;
        */
        int getState();

        String getContentType();

        long getDuration();

        long getMediaTime();

        long setMediaTime(long paramLong);
        

        void addPlayerListener(PlayerListener paramPlayerListener);

        void close();

        void deallocate();

        void prefetch();
        

        void realize();
        

        void removePlayerListener(PlayerListener paramPlayerListener);

        void setLoopCount(int paramInt);

        void start();
        

        void stop();
        
    }
}