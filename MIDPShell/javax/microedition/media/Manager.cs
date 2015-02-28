using java.io;
using java.lang;

namespace javax.microedition.media
{

    using java.io.IOException;
    using java.io.InputStream;

    public sealed override class Manager
    {
        public static sealed override String TONE_DEVICE_LOCATOR = "device://tone";

        public static String[] getSupportedContentTypes(String paramString)
        {
            return null;
        }

        public static String[] getSupportedProtocols(String paramString)
        {
            return null;
        }

        public static Player createPlayer(InputStream paramInputStream, String paramString)
        {
            return null;
        }

        public static Player createPlayer(String paramString)
        {
            return null;
        }

        public static void playTone(int paramInt1, int paramInt2, int paramInt3)

        { }
    }
}