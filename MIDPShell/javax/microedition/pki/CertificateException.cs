namespace javax.microedition.pki
{

    using java.io;
    using java.io.IOException;
    using java.lang;

    public class CertificateException
      : IOException
    {
        public static sealed override byte BAD_EXTENSIONS = 1;
        public static sealed override byte INAPPROPRIATE_KEY_USAGE = 10;
        public static sealed override byte BROKEN_CHAIN = 11;
        public static sealed override byte ROOT_CA_EXPIRED = 12;
        public static sealed override byte UNSUPPORTED_PUBLIC_KEY_TYPE = 13;
        public static sealed override byte VERIFICATION_FAILED = 14;
        public static sealed override byte CERTIFICATE_CHAIN_TOO_LONG = 2;
        public static sealed override byte EXPIRED = 3;
        public static sealed override byte UNAUTHORIZED_INTERMEDIATE_CA = 4;
        public static sealed override byte MISSING_SIGNATURE = 5;
        public static sealed override byte NOT_YET_VALID = 6;
        public static sealed override byte SITENAME_MISMATCH = 7;
        public static sealed override byte UNRECOGNIZED_ISSUER = 8;
        public static sealed override byte UNSUPPORTED_SIGALG = 9;

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