namespace javax.microedition.lcdui.game
{


    public abstract class GameCanvas
      : Canvas
    {
        public static sealed override int GAME_B_PRESSED = 1024;
        public static sealed override int UP_PRESSED = 2;
        public static sealed override int GAME_C_PRESSED = 2048;
        public static sealed override int FIRE_PRESSED = 256;
        public static sealed override int RIGHT_PRESSED = 32;
        public static sealed override int LEFT_PRESSED = 4;
        public static sealed override int GAME_D_PRESSED = 4096;
        public static sealed override int GAME_A_PRESSED = 512;
        public static sealed override int DOWN_PRESSED = 64;

        protected GameCanvas(bool paramBoolean) { }

        protected Graphics getGraphics()
        {
            return null;
        }

        public int getKeyStates()
        {
            return 0;
        }

        public void flushGraphics() { }

        public void flushGraphics(int paramInt1, int paramInt2, int paramInt3, int paramInt4) { }

        public void paint(Graphics paramGraphics) { }
    }

}