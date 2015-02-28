using java.lang;
namespace javax.microedition.media
{

    public interface Player : Controllable
    {
        public static sealed override long TIME_UNKNOWN = -1L;
        public static sealed override int CLOSED = 0;
        public static sealed override int UNREALIZED = 100;
        public static sealed override int REALIZED = 200;
        public static sealed override int PREFETCHED = 300;
        public static sealed override int STARTED = 400;

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