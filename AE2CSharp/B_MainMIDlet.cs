using javax.microedition.midlet;

namespace aeii
{

    public class B_MainMIDlet : MIDlet
    {
        public static B_MainMIDlet midlet;
        public static E_MainCanvas canvas;

        public override void startApp()
        {
            if (midlet == null)
            {
                midlet = this;
                canvas = new E_MainCanvas(this);
            }
        }

        public override void destroyApp(bool inBool)
        {
            if (canvas != null)
            {
                canvas.stopGame();
            }
            canvas = null;
            midlet = null;
        }

        public override void pauseApp()
        {
            //
        }

    }

}