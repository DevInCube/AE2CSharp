using java.lang;

namespace javax.microedition.midlet
{
    public abstract class MIDlet
    {
        protected abstract void destroyApp(bool paramBoolean);

        protected abstract void pauseApp();

        protected abstract void startApp();

        public sealed override bool platformRequest(String paramString)
        {
            return false;
        }

        public sealed override int checkPermission(String paramString)
        {
            return 0;
        }

        public sealed override String getAppProperty(String paramString)
        {
            return null;
        }

        public sealed override void notifyDestroyed() { }

        public sealed override void notifyPaused() { }

        public sealed override void resumeRequest() { }
    }

}