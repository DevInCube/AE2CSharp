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
            this.gr.IntersectClip(new System.Drawing.Rectangle(x, y, width, height));
        }

        public void copyArea(int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6, int paramInt7) { }

        public void drawArc(int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5, int paramInt6) { }

        public void drawChar(char paramChar, int paramInt1, int paramInt2, int paramInt3) { }

        public void drawChars(char[] paramArrayOfChar, int paramInt1, int paramInt2, int paramInt3, int paramInt4, int paramInt5) { }

        public void drawImage(Image img, int x, int y, int anchor)
        {
            System.Drawing.PointF p = applyAnchor(x, y, img.WPFImage.Size, anchor);
            this.gr.DrawImage(img.WPFImage, p.X, p.Y);
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
            System.Drawing.PointF dest = applyAnchor(x_dest, y_dest, new System.Drawing.Size(width, height), anchor);
            this.gr.DrawImage(src.WPFImage, dest.X, dest.Y, srcRect, System.Drawing.GraphicsUnit.Pixel);
        }

        public void drawRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight) { }

        public void drawString(String str, int x, int y, int anchor)
        {            
            var b = new System.Drawing.SolidBrush(this.wpfColor);
            System.Drawing.SizeF stringSize = new System.Drawing.SizeF();
            stringSize = this.gr.MeasureString(str.ToString(), wpfFont);
            System.Drawing.PointF pos = applyAnchor(x, y, stringSize, anchor);
            this.gr.DrawString(str.ToString(), wpfFont, b, pos.X, pos.Y); 
        }

        private System.Drawing.PointF applyAnchor(int x, int y, System.Drawing.SizeF size, int anchor)
        {
            float fx = (float)x;
            float fy = (float)y;
            if ((anchor & Graphics.HCENTER) != 0) fx -= size.Width / 2.0F;
            if ((anchor & Graphics.RIGHT) != 0) fx -= size.Width;
            if ((anchor & Graphics.VCENTER) != 0) fy -= size.Height / 2.0F;            
            if ((anchor & Graphics.BOTTOM) != 0) fy -= size.Height;
            if ((anchor & Graphics.BASELINE) != 0) fy -= size.Height;
            return new System.Drawing.PointF(fx, fy);
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
            float fontSize = 10;
            if ((font.getSize() & Font.SIZE_SMALL) != 0) fontSize = 8;
            if ((font.getSize() & Font.SIZE_LARGE) != 0) fontSize = 12;
            int fStyle = font.getStyle();
            System.Drawing.FontStyle style = System.Drawing.FontStyle.Regular;
            if ((fStyle & Font.STYLE_BOLD) != 0) style |= System.Drawing.FontStyle.Bold;
            if ((fStyle & Font.STYLE_ITALIC) != 0) style |= System.Drawing.FontStyle.Italic;
            if ((fStyle & Font.STYLE_UNDERLINED) != 0) style |= System.Drawing.FontStyle.Underline;
            string fontName = System.Drawing.FontFamily.GenericMonospace.Name;
            this.wpfFont = new System.Drawing.Font(fontName, fontSize, style);            
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