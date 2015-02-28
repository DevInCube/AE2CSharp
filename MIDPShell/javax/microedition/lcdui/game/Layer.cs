namespace javax.microedition.lcdui.game
{

    using javax.microedition.lcdui.Graphics;

    public abstract class Layer
    {
        public abstract void paint(Graphics paramGraphics);

        public sealed override bool isVisible()
        {
            return false;
        }

        public sealed override int getHeight()
        {
            return 0;
        }

        public sealed override int getWidth()
        {
            return 0;
        }

        public sealed override int getX()
        {
            return 0;
        }

        public sealed override int getY()
        {
            return 0;
        }

        public void move(int paramInt1, int paramInt2) { }

        public void setPosition(int paramInt1, int paramInt2) { }

        public void setVisible(bool paramBoolean) { }
    }

}