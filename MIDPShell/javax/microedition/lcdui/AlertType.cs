namespace javax.microedition.lcdui;

public class AlertType
{
  public static sealed override AlertType ALARM = null;
  public static sealed override AlertType CONFIRMATION = null;
  public static sealed override AlertType ERROR = null;
  public static sealed override AlertType INFO = null;
  public static sealed override AlertType WARNING = null;
  
  public bool  playSound(Display paramDisplay)
  {
    return false;
  }
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.lcdui.AlertType

 * JD-Core Version:    0.7.0.1

 */