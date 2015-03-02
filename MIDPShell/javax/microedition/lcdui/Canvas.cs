using java.lang;

namespace javax.microedition.lcdui
{

    public abstract class Canvas : Displayable
    {
        public static readonly int UP = 1;
        public static readonly int GAME_B = 10;
        public static readonly int GAME_C = 11;
        public static readonly int GAME_D = 12;
        public static readonly int LEFT = 2;
        public static readonly int KEY_POUND = 35;
        public static readonly int KEY_STAR = 42;
        public static readonly int KEY_NUM0 = 48;
        public static readonly int KEY_NUM1 = 49;
        public static readonly int RIGHT = 5;
        public static readonly int KEY_NUM2 = 50;
        public static readonly int KEY_NUM3 = 51;
        public static readonly int KEY_NUM4 = 52;
        public static readonly int KEY_NUM5 = 53;
        public static readonly int KEY_NUM6 = 54;
        public static readonly int KEY_NUM7 = 55;
        public static readonly int KEY_NUM8 = 56;
        public static readonly int KEY_NUM9 = 57;
        public static readonly int DOWN = 6;
        public static readonly int FIRE = 8;
        public static readonly int GAME_A = 9;

        public abstract void paint(Graphics paramGraphics);

        protected void hideNotify() { }

        protected void keyPressed(int paramInt) { }

        protected void keyReleased(int paramInt) { }

        protected void keyRepeated(int paramInt) { }

        protected void pointerDragged(int paramInt1, int paramInt2) { }

        protected void pointerPressed(int paramInt1, int paramInt2) { }

        protected void pointerReleased(int paramInt1, int paramInt2) { }

        protected void showNotify() { }

        protected void sizeChanged(int paramInt1, int paramInt2) { }

        public bool hasPointerEvents()
        {
            return false;
        }

        public bool hasPointerMotionEvents()
        {
            return false;
        }

        public bool hasRepeatEvents()
        {
            return false;
        }

        public bool isDoubleBuffered()
        {
            return false;
        }

        public  void repaint() { }

        public  void repaint(int paramInt1, int paramInt2, int paramInt3, int paramInt4) { }

        public  void serviceRepaints() { }

        public int getGameAction(int paramInt)
        {
            return 0;
        }

        public override int getHeight()
        {
            return 0;
        }

        public int getKeyCode(int paramInt)
        {
            return 0;
        }

        public override int getWidth()
        {
            return 0;
        }

        public String getKeyName(int paramInt)
        {
            return null;
        }

        public void setFullScreenMode(bool paramBoolean) { }
    }

}