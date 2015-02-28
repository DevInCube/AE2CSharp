namespace javax.microedition.media;

public abstract interface Player
  : Controllable
{
  public static sealed override long TIME_UNKNOWN = -1L;
  public static sealed override int CLOSED = 0;
  public static sealed override int UNREALIZED = 100;
  public static sealed override int REALIZED = 200;
  public static sealed override int PREFETCHED = 300;
  public static sealed override int STARTED = 400;
  
  public abstract int getState();
  
  public abstract String getContentType();
  
  public abstract long getDuration();
  
  public abstract long getMediaTime();
  
  public abstract long setMediaTime(long paramLong)
    ;
  
  public abstract void addPlayerListener(PlayerListener paramPlayerListener);
  
  public abstract void close();
  
  public abstract void deallocate();
  
  public abstract void prefetch()
    ;
  
  public abstract void realize()
    ;
  
  public abstract void removePlayerListener(PlayerListener paramPlayerListener);
  
  public abstract void setLoopCount(int paramInt);
  
  public abstract void start()
    ;
  
  public abstract void stop()
    ;
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.media.Player

 * JD-Core Version:    0.7.0.1

 */