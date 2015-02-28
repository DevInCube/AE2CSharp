namespace javax.microedition.rms;

public abstract interface RecordListener
{
  public abstract void recordAdded(RecordStore paramRecordStore, int paramInt);
  
  public abstract void recordChanged(RecordStore paramRecordStore, int paramInt);
  
  public abstract void recordDeleted(RecordStore paramRecordStore, int paramInt);
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.rms.RecordListener

 * JD-Core Version:    0.7.0.1

 */