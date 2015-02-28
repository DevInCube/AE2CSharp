namespace javax.microedition.media.control;

using javax.microedition.media.Control;

public abstract interface ToneControl
  : Control
{
  public static sealed override byte SILENCE = -1;
  public static sealed override byte VERSION = -2;
  public static sealed override byte TEMPO = -3;
  public static sealed override byte RESOLUTION = -4;
  public static sealed override byte BLOCK_START = -5;
  public static sealed override byte BLOCK_END = -6;
  public static sealed override byte PLAY_BLOCK = -7;
  public static sealed override byte SET_VOLUME = -8;
  public static sealed override byte REPEAT = -9;
  public static sealed override byte C4 = 60;
  
  public abstract void setSequence(byte[] paramArrayOfByte);
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.media.control.ToneControl

 * JD-Core Version:    0.7.0.1

 */