using AE2.Tools.Loaders;
using java.csharp;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AE2.Tools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        internal void DrawMap(Map m)
        {
            Image[][] carImg = JavaArray.New<Image>(m.mapHeight, m.mapWidth);
            for (int i = 0; i < m.mapHeight; i++)
                for (int j = 0; j < m.mapWidth;j++ )
                {
                    byte tileId = m.mapTilesIds[j][i];
                    BitmapImage carBitmap = getTileImage(tileId);
                    if (carBitmap == null) continue;
                    carImg[i][j] = new Image
                    {
                        Source = carBitmap,
                        Width = carBitmap.Width,
                        Height = carBitmap.Height
                    };
                    Canvas.SetLeft(carImg[i][j], j * carBitmap.Width);
                    Canvas.SetTop(carImg[i][j], i * carBitmap.Height);
                    Canvas.Children.Add(carImg[i][j]);
                }
        }

        private BitmapImage getTileImage(byte tileId)
        {
            string id = (tileId < 10) ? ("0" + tileId) : tileId.ToString();
            return Convert(E_MainCanvas.getResourceData("tiles0_" + id + ".png"));
        }

        private BitmapImage Convert(byte[] byteVal)
        {
            if (byteVal == null) return null;
            BitmapImage myBitmapImage = null;

            try
            {
                var strmImg = new MemoryStream(byteVal);
                myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.StreamSource = strmImg;
                myBitmapImage.EndInit();
            }
            catch { }
            return myBitmapImage;
        }
    }
}
