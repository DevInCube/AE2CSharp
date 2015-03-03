using java.lang;
namespace javax.microedition.lcdui
{

    public class Graphics
    {
        public const int SOLID = 0;
        public const int DOTTED = 1;
        public const int HCENTER = 1;
        public const int TOP = 16;
        public const int VCENTER = 2;
        public const int BOTTOM = 32;
        public const int LEFT = 4;
        public const int BASELINE = 64;
        public const int RIGHT = 8;

        private Font font;

        private System.Drawing.Graphics gr;
        private System.Drawing.Color wpfColor;
        private System.Drawing.Font wpfFont;
        private int colorInt;

        public System.Drawing.Graphics WPFGraphics { get { return gr; } }

        public Graphics(System.Drawing.Graphics newGraphics)
        {
            this.gr = newGraphics;
        }

        public int getBlueComponent()
        {
            return 0;
        }

        public int getClipHeight()
        {
            return 0;
        }

        public int getClipWidth()
        {
            return 0;
        }

        public int getClipX()
        {
            return 0;
        }

        public int getClipY()
        {
            return 0;
        }

        public int getColor()
        {
            return 0;
        }

        public int getDisplayColor(int paramInt)
        {
            return 0;
        }

        public int getGrayScale()
        {
            return 0;
        }

        public int getGreenComponent()
        {
            return 0;
        }

        public int getRedComponent()
        {
            return 0;
        }

        public int getStrokeStyle()
        {
            return 0;
        }

        public int getTranslateX()
        {
            return 0;
        }

        public int getTranslateY()
        {
            return 0;
        }

        public Font getFont()
        {
            return font;
        }

        public void clipRect(int x, int y, int width, int height)
        {
            //@todo
        }

        public void copyArea(int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6, int paramInt7) { }

        public void drawArc(int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6) { }

        public void drawChar(char paramChar, int paramInt1, int paramInt2, int paramInt3) { }

        public void drawChars(char[] paramArrayOfChar, int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5) { }

        public void drawImage(Image img, int x, int y, int anchor)
        {
            this.gr.DrawImage(img.WPFImage, x, y);
        }

        public void drawLine(int x1, int y1, int x2, int y2)
        {
            var pen = new System.Drawing.Pen(this.wpfColor);
            this.gr.DrawLine(new System.Drawing.Pen(wpfColor), x1, y1, x2, y2);
        }

        public void drawRGB(int[] rgbData, int offset, int scanlength, int x, int y, int width, int height, bool processAlpha) { }

        public void drawRect(int x, int y, int width, int height) {
            var brush = new System.Drawing.SolidBrush(this.wpfColor);
            var pen = new System.Drawing.Pen(brush);
            this.gr.DrawRectangle(pen, x, y, width, height);
        }

        public void drawRegion(
            Image src,
            int x_src, int y_src,
            int width, int height,
            int transform,
            int x_dest, int y_dest,
            int anchor)
        {
            var srcRect = new System.Drawing.Rectangle(x_src, y_src, width, height);
            this.gr.DrawImage(src.WPFImage, x_dest, y_dest, srcRect, System.Drawing.GraphicsUnit.Pixel);
        }

        public void drawRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight) { }

        public void drawString(String str, int x, int y, int anchor)
        {            
            var b = new System.Drawing.SolidBrush(this.wpfColor);
            System.Drawing.SizeF stringSize = new System.Drawing.SizeF();
            stringSize = this.gr.MeasureString(str.ToString(), wpfFont);
            float fx = (float)x;
            float fy = (float)y;
            if ((anchor & Graphics.HCENTER) != 0)
            {
                fx -= stringSize.Width / 2.0F;
            }
            if ((anchor & Graphics.BOTTOM) != 0)
            {
                fy -= stringSize.Height;
            }
            this.gr.DrawString(str.ToString(), wpfFont, b, fx, fy);//@todo
        }

        public void drawSubstring(String str, int offset, int len, int x, int y, int anchor) { }

        public void fillArc(int x, int y, int width, int height, int startAngle, int arcAngle)
        {
            var brush = new System.Drawing.SolidBrush(this.wpfColor);
            this.gr.FillPie(brush, x, y, width, height, startAngle, arcAngle);
        }

        public void fillRect(int x, int y, int width, int height)
        {
            var brush = new System.Drawing.SolidBrush(this.wpfColor);
            this.gr.FillRectangle(brush, x, y, width, height);
        }

        public void fillRoundRect(int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6) { }

        public void fillTriangle(int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6) { }

        public void setClip(int x, int y, int width, int height) {
            this.gr.SetClip(new System.Drawing.Rectangle(x, y, width, height));
        }

        public void setColor(int colorInt)
        {
            int r = (colorInt >> 16) & 255;
            int g = (colorInt >> 8) & 255;
            int b = (colorInt) & 255;
            setColor(r, g, b);
        }

        public void setColor(int r, int g, int b) 
        {
            int alpha = 255;
            this.wpfColor = System.Drawing.Color.FromArgb(alpha, r, g, b);
        }

        public void setFont(Font font)
        {
            var defFont = System.Drawing.SystemFonts.DefaultFont;
            float fontSize = (float)font.getSize();
            if (fontSize == 0) fontSize = 10;
            this.wpfFont = new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace.Name, fontSize);
            //@todo init font
            this.font = font;
        }

        public void setGrayScale(int paramInt) { }

        public void setStrokeStyle(int paramInt) { }

        public void translate(int x, int y)
        {
            this.gr.TranslateTransform((float)x, (float)y);
        }
    }

}