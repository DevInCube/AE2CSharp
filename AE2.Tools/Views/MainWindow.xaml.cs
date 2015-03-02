using AE2.Tools.Loaders;
using java.csharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                    carImg[i][j] = new Image();
                    carImg[i][j].Source = carBitmap;
                    carImg[i][j].Width = carBitmap.Width;
                    carImg[i][j].Height = carBitmap.Height;
                    Canvas.SetLeft(carImg[i][j], j * carBitmap.Width);
                    Canvas.SetTop(carImg[i][j], i * carBitmap.Height);
                    Canvas.Children.Add(carImg[i][j]);
                }
        }

        private BitmapImage getTileImage(byte tileId)
        {
            string id = (tileId < 10) ? ("0" + tileId) : tileId.ToString();
            BitmapImage carBitmap = Convert(E_MainCanvas.getResourceData("tiles0_" + id + ".png"));
            return carBitmap;
        }

        private BitmapImage Convert(byte[] byteVal)
        {
            if (byteVal == null) return null;
            BitmapImage myBitmapImage = null;

            try
            {
                MemoryStream strmImg = new MemoryStream(byteVal);
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
