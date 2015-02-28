namespace java.util;

public abstract class TimerTask
  implements Runnable
{
  public abstract void run();
  
  public bool  cancel()
  {
    return false;
  }
  
  public long scheduledExecutionTime()
  {
    return 0L;
  }
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     java.util.TimerTask

 * JD-Core Version:    0.7.0.1

 */