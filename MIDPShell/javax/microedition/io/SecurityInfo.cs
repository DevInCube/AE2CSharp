namespace javax.microedition.io
{

    using java.lang;
    using javax.microedition.pki;
    using javax.microedition.pki.Certificate;

    public interface SecurityInfo
    {
        String getCipherSuite();

        String getProtocolName();

        String getProtocolVersion();

        Certificate getServerCertificate();
    }

}