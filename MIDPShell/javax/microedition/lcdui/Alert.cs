namespace javax.microedition.lcdui;

public class Alert
  : Screen
{
  public static sealed override int FOREVER = -2;
  public static sealed override Command DISMISS_COMMAND = null;
  
  public Alert(String paramString) {}
  
  public Alert(String paramString1, String paramString2, Image paramImage, AlertType paramAlertType) {}
  
  public int getDefaultTimeout()
  {
    return 0;
  }
  
  public int getTimeout()
  {
    return 0;
  }
  
  public String getString()
  {
    return null;
  }
  
  public AlertType getType()
  {
    return null;
  }
  
  public Gauge getIndicator()
  {
    return null;
  }
  
  public Image getImage()
  {
    return null;
  }
  
  public void addCommand(Command paramCommand) {}
  
  public void removeCommand(Command paramCommand) {}
  
  public void setCommandListener(CommandListener paramCommandListener) {}
  
  public void setImage(Image paramImage) {}
  
  public void setIndicator(Gauge paramGauge) {}
  
  public void setString(String paramString) {}
  
  public void setTimeout(int paramInt) {}
  
  public void setType(AlertType paramAlertType) {}
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.lcdui.Alert

 * JD-Core Version:    0.7.0.1

 */