namespace javax.microedition.io;

using javax.microedition.pki.Certificate;

public abstract interface SecurityInfo
{
  public abstract String getCipherSuite();
  
  public abstract String getProtocolName();
  
  public abstract String getProtocolVersion();
  
  public abstract Certificate getServerCertificate();
}



/* Location:           D:\Programming\Eclipse\midp_2.1.jar

 * Qualified Name:     javax.microedition.io.SecurityInfo

 * JD-Core Version:    0.7.0.1

 */