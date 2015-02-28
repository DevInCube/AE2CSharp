namespace javax.microedition.lcdui.game
{

    using javax.microedition.lcdui.Graphics;
    using javax.microedition.lcdui.Image;

    public class Sprite
      : Layer
    {
        public static sealed override int TRANS_NONE = 0;
        public static sealed override int TRANS_MIRROR_ROT180 = 1;
        public static sealed override int TRANS_MIRROR = 2;
        public static sealed override int TRANS_ROT180 = 3;
        public static sealed override int TRANS_MIRROR_ROT270 = 4;
        public static sealed override int TRANS_ROT90 = 5;
        public static sealed override int TRANS_ROT270 = 6;
        public static sealed override int TRANS_MIRROR_ROT90 = 7;

        public Sprite(Image paramImage) { }

        public Sprite(Image paramImage, int paramInt1, int paramInt2) { }

        public Sprite(Sprite paramSprite) { }

        public sealed override bool collidesWith(Image paramImage, int paramInt1, int paramInt2, bool paramBoolean)
        {
            return false;
        }

        public sealed override bool collidesWith(Sprite paramSprite, bool paramBoolean)
        {
            return false;
        }

        public sealed override bool collidesWith(TiledLayer paramTiledLayer, bool paramBoolean)
        {
            return false;
        }

        public sealed override int getFrame()
        {
            return 0;
        }

        public sealed override void paint(Graphics paramGraphics) { }

        public int getFrameSequenceLength()
        {
            return 0;
        }

        public int getRawFrameCount()
        {
            return 0;
        }

        public int getRefPixelX()
        {
            return 0;
        }

        public int getRefPixelY()
        {
            return 0;
        }

        public void defineCollisionRectangle(int paramInt1, int paramInt2, int paramInt3, int paramInt4) { }

        public void defineReferencePixel(int paramInt1, int paramInt2) { }

        public void nextFrame() { }

        public void prevFrame() { }

        public void setFrame(int paramInt) { }

        public void setFrameSequence(int[] paramArrayOfInt) { }

        public void setImage(Image paramImage, int paramInt1, int paramInt2) { }

        public void setRefPixelPosition(int paramInt1, int paramInt2) { }

        public void setTransform(int paramInt) { }
    }

}