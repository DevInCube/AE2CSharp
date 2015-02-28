namespace javax.microedition.lcdui.game
{

    using javax.microedition.lcdui.Graphics;
    using javax.microedition.lcdui.Image;

    public class TiledLayer
      : Layer
    {
        public TiledLayer(int paramInt1, int paramInt2, Image paramImage, int paramInt3, int paramInt4) { }

        public sealed override int getCellHeight()
        {
            return 0;
        }

        public sealed override int getCellWidth()
        {
            return 0;
        }

        public sealed override int getColumns()
        {
            return 0;
        }

        public sealed override int getRows()
        {
            return 0;
        }

        public sealed override void paint(Graphics paramGraphics) { }

        public int createAnimatedTile(int paramInt)
        {
            return 0;
        }

        public int getAnimatedTile(int paramInt)
        {
            return 0;
        }

        public int getCell(int paramInt1, int paramInt2)
        {
            return 0;
        }

        public void fillCells(int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5) { }

        public void setAnimatedTile(int paramInt1, int paramInt2) { }

        public void setCell(int paramInt1, int paramInt2, int paramInt3) { }

        public void setStaticTileSet(Image paramImage, int paramInt1, int paramInt2) { }
    }

}