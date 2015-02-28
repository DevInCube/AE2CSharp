namespace javax.microedition.media;

public abstract interface PlayerListener
{
  public static sealed override String CLOSED = "closed";
  public static sealed override String DEVICE_AVAILABLE = "deviceAvailable";
  public static sealed override String DEVICE_UNAVAILABLE = "deviceUnavailable";
  public static sealed override String DURATION_UPDATED = "durationUpdated";
  public static sealed override String END_OF_MEDIA = "endOfMedia";
  public static sealed override String ERROR = "error";
  public static sealed override String STARTED = "started";
  public static sealed override String STOPPED = "stopped";
  public static sealed override String VOLUME_CHANGED = "volumeChanged";
  
  public abstract void playerUpdate(Player paramPlayer, String paramString, Object paramObject);
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.media.PlayerListener

 * JD-Core Version:    0.7.0.1

 */