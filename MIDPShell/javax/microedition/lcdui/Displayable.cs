namespace javax.microedition.lcdui;

public abstract class Displayable
{
  protected void sizeChanged(int paramInt1, int paramInt2) {}
  
  public bool  isShown()
  {
    return false;
  }
  
  public int getHeight()
  {
    return 0;
  }
  
  public int getWidth()
  {
    return 0;
  }
  
  public String getTitle()
  {
    return null;
  }
  
  public Ticker getTicker()
  {
    return null;
  }
  
  public void addCommand(Command paramCommand) {}
  
  public void removeCommand(Command paramCommand) {}
  
  public void setCommandListener(CommandListener paramCommandListener) {}
  
  public void setTicker(Ticker paramTicker) {}
  
  public void setTitle(String paramString) {}
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.lcdui.Displayable

 * JD-Core Version:    0.7.0.1

 */