namespace javax.microedition.pki;

public abstract interface Certificate
{
  public abstract String getIssuer();
  
  public abstract String getSerialNumber();
  
  public abstract String getSigAlgName();
  
  public abstract String getSubject();
  
  public abstract String getType();
  
  public abstract String getVersion();
  
  public abstract long getNotAfter();
  
  public abstract long getNotBefore();
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.pki.Certificate

 * JD-Core Version:    0.7.0.1

 */