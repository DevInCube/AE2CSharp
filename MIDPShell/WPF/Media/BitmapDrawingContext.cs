using javax.microedition.lcdui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDP.WPF.Media
{
    class BitmapDrawingContext : IDrawingContext
    {

        private System.Drawing.Image canvasImage;
        private System.Windows.Controls.Image image;

        public System.Windows.FrameworkElement WPFControl
        {
            get { return image; }
        }

        public int Width
        {
            get
            {
                int width = 0; //@todo
                System.Windows.Application.Current.Dispatcher.Invoke((System.Action)(() =>
                {
                    width = canvasImage.Width;
                }));
                return width;
            }
        }

        public int Height
        {
            get
            {
                int height = 0; //@todo
                System.Windows.Application.Current.Dispatcher.Invoke((System.Action)(() =>
                {
                    height = canvasImage.Height;
                }));
                return height;
            }
        }

        public BitmapDrawingContext()
        {
            canvasImage = new System.Drawing.Bitmap(240, 320);
            image = new System.Windows.Controls.Image();
            image.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            System.Windows.Media.RenderOptions.SetBitmapScalingMode(image, System.Windows.Media.BitmapScalingMode.NearestNeighbor);
            System.Windows.Media.ImageSourceConverter c = new System.Windows.Media.ImageSourceConverter();
        }

        public Graphics CreateGraphics()
        {
            return new Graphics(System.Drawing.Graphics.FromImage(canvasImage));
        }


        public void ServiceRepaints()
        {
            System.Windows.Application.Current.Dispatcher.Invoke((System.Action)(() =>
            {
                var bitmap = canvasImage as System.Drawing.Bitmap;
                image.Source = loadBitmap(bitmap);
            }));
        }

        public void Repaint()
        {
            throw new NotImplementedException();
        }

        [System.Runtime.InteropServices.DllImport("gdi32")]
        static extern int DeleteObject(System.IntPtr o);

        public static System.Windows.Media.Imaging.BitmapSource loadBitmap(System.Drawing.Bitmap source)
        {
            System.IntPtr ip = source.GetHbitmap();
            System.Windows.Media.Imaging.BitmapSource bs = null;
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                   System.IntPtr.Zero, System.Windows.Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(ip);
            }

            return bs;
        }
    }
}
