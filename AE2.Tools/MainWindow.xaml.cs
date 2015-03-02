using AE2.Tools.Loaders;
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
            Image[][] carImg = ArrayHelper.createArray<Image>(m.mapHeight, m.mapWidth);
            for (int i = 0; i < m.mapHeight; i++)
                for (int j = 0; j < m.mapWidth;j++ )
                {
                    byte tileId = m.mapTilesIds[j][i];
                    string id = (tileId < 10) ? ("0" + tileId) : tileId.ToString();
                    BitmapImage carBitmap = Convert(ResourceLoader.getResourceData("tiles0_" + id + ".png"));
                    if (carBitmap == null) continue;
                    carImg[i][j] = new Image();
                    carImg[i][j].Source = carBitmap;
                    carImg[i][j].Width = carBitmap.Width;
                    carImg[i][j].Height = carBitmap.Height;
                    Canvas.SetLeft(carImg[i][j], j*24);
                    Canvas.SetTop(carImg[i][j], i*24);
                    Canvas.Children.Add(carImg[i][j]);
                }
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
              //  myBitmapImage.Width = 24;
              //  myBitmapImage.Height = 24;
                myBitmapImage.EndInit();
            }
            catch { }
            return myBitmapImage;
        }
    }
}
