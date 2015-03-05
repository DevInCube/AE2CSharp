using java.lang;
using MIDP.WPF.ViewModels;
using System.ComponentModel;

namespace javax.microedition.lcdui
{

    public abstract class Canvas : Displayable, IEventListener
    {
        public const int UP = 1;
        public const int GAME_B = 10;
        public const int GAME_C = 11;
        public const int GAME_D = 12;
        public const int LEFT = 2;
        public const int KEY_POUND = 35;
        public const int KEY_STAR = 42;
        public const int KEY_NUM0 = 48;
        public const int KEY_NUM1 = 49;
        public const int RIGHT = 5;
        public const int KEY_NUM2 = 50;
        public const int KEY_NUM3 = 51;
        public const int KEY_NUM4 = 52;
        public const int KEY_NUM5 = 53;
        public const int KEY_NUM6 = 54;
        public const int KEY_NUM7 = 55;
        public const int KEY_NUM8 = 56;
        public const int KEY_NUM9 = 57;
        public const int DOWN = 6;
        public const int FIRE = 8;
        public const int GAME_A = 9;

        private Graphics graphics;
        private System.Drawing.Image canvasImage;
        private System.Windows.Controls.Image image;

        public override System.Windows.FrameworkElement WPFControl
        {
            get { return image; }
        }

        public Canvas()
        {         
            canvasImage = new System.Drawing.Bitmap(240, 320);
            image = new System.Windows.Controls.Image();
            image.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            System.Windows.Media.RenderOptions.SetBitmapScalingMode(image, System.Windows.Media.BitmapScalingMode.NearestNeighbor);
            System.Windows.Media.ImageSourceConverter c = new System.Windows.Media.ImageSourceConverter();            
            graphics = new Graphics(System.Drawing.Graphics.FromImage(canvasImage));
        }

        public static System.Windows.Media.Imaging.BitmapSource CreateBitmapSourceFromGdiBitmap(System.Drawing.Bitmap bitmap)
        {
            if (bitmap == null)
                throw new System.ArgumentNullException("bitmap");

            var rect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return System.Windows.Media.Imaging.BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    System.Windows.Media.PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        private void Paint()
        {
            if (isShown())
            {
                this.paint(graphics);
                System.Windows.Application.Current.Dispatcher.Invoke((System.Action)(() =>
                {
                    var bitmap = canvasImage as System.Drawing.Bitmap;
                    System.Windows.Media.ImageSource imSource = (System.Windows.Media.ImageSource)CreateBitmapSourceFromGdiBitmap(bitmap);
                    image.Source = imSource;
                }));
            }
        }

        public abstract void paint(Graphics paramGraphics);

        protected void hideNotify() { }

        public virtual void keyPressed(int keyCode) { }

        public virtual void keyReleased(int keyCode) { }

        protected void keyRepeated(int paramInt) { }

        public virtual void pointerDragged(int x, int y) { }

        public virtual void pointerPressed(int x, int y) { }

        public virtual void pointerReleased(int x, int y) { }

        protected void showNotify() { }

        protected override void sizeChanged(int w, int h) { }

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

        public  void repaint() {
            Paint();
            //@todo
        }

        public  void repaint(int x, int y, int w, int h) { }

        public void serviceRepaints()
        {
            //@todo
        }

        public int getGameAction(int keyCode)
        {
            return 0;
        }

        public override int getHeight()
        {
            int height = 0; //@todo
            System.Windows.Application.Current.Dispatcher.Invoke((System.Action)(() =>
            {
                height = canvasImage.Height;
            }));
            return height;
        }

        public int getKeyCode(int paramInt)
        {
            return 0;
        }

        public override int getWidth()
        {
            int width = 0; //@todo
            System.Windows.Application.Current.Dispatcher.Invoke((System.Action)(() => {
                width = canvasImage.Width;
            }));
            return width;
        }

        public String getKeyName(int paramInt)
        {
            return null;
        }

        public void setFullScreenMode(bool paramBoolean) {
            //@todo
            var p = this.image.Parent;
        }


    }

}