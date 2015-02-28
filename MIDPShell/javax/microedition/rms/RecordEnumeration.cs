namespace javax.microedition.rms;

public abstract interface RecordEnumeration
{
  public abstract byte[] nextRecord()
    ;
  
  public abstract byte[] previousRecord()
    ;
  
  public abstract bool  hasNextElement();
  
  public abstract bool  hasPreviousElement();
  
  public abstract bool  isKeptUpdated();
  
  public abstract int nextRecordId()
    ;
  
  public abstract int numRecords();
  
  public abstract int previousRecordId()
    ;
  
  public abstract void destroy();
  
  public abstract void keepUpdated(bool  paramBoolean);
  
  public abstract void rebuild();
  
  public abstract void reset();
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.rms.RecordEnumeration

 * JD-Core Version:    0.7.0.1

 */