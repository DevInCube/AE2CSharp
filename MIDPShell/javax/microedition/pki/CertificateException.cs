namespace javax.microedition.pki
{

    using java.io;
    using java.lang;

    public class CertificateException
      : IOException
    {
        public static readonly byte BAD_EXTENSIONS = 1;
        public static readonly byte INAPPROPRIATE_KEY_USAGE = 10;
        public static readonly byte BROKEN_CHAIN = 11;
        public static readonly byte ROOT_CA_EXPIRED = 12;
        public static readonly byte UNSUPPORTED_PUBLIC_KEY_TYPE = 13;
        public static readonly byte VERIFICATION_FAILED = 14;
        public static readonly byte CERTIFICATE_CHAIN_TOO_LONG = 2;
        public static readonly byte EXPIRED = 3;
        public static readonly byte UNAUTHORIZED_INTERMEDIATE_CA = 4;
        public static readonly byte MISSING_SIGNATURE = 5;
        public static readonly byte NOT_YET_VALID = 6;
        public static readonly byte SITENAME_MISMATCH = 7;
        public static readonly byte UNRECOGNIZED_ISSUER = 8;
        public static readonly byte UNSUPPORTED_SIGALG = 9;

        public CertificateException(String paramString, Certificate paramCertificate, byte paramByte) { }

        public CertificateException(Certificate paramCertificate, byte paramByte) { }

        public byte getReason()
        {
            return 0;
        }

        public Certificate getCertificate()
        {
            return null;
        }
    }


}