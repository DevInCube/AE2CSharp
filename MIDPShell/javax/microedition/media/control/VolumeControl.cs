namespace javax.microedition.media.control;

using javax.microedition.media.Control;

public abstract interface VolumeControl
  : Control
{
  public abstract bool  isMuted();
  
  public abstract int getLevel();
  
  public abstract int setLevel(int paramInt);
  
  public abstract void setMute(bool  paramBoolean);
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.media.control.VolumeControl

 * JD-Core Version:    0.7.0.1

 */