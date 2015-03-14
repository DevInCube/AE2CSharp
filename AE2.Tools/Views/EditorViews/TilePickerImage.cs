using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AE2.Tools.Views.EditorViews
{
    public class TilePickerImage : Canvas
    {

        private byte px, py;
        private byte[][] tiles;
        private Image image, cursor;

        public event Action<byte> TileSeleted;

        public TilePickerImage()
        {
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            image = new Image();
            this.Children.Add(image);

            cursor = new Image();
            cursor.Source = MapEditor.getCursorImage();
            cursor.Width = MapEditor.CELL_SIZE;
            cursor.Height = MapEditor.CELL_SIZE;
            cursor.Opacity = 0;
            this.Children.Add(cursor);

            this.MouseMove += TilePickerImage_MouseMove;
            this.MouseUp += TilePickerImage_MouseUp;
            this.MouseEnter += TilePickerImage_MouseEnter;
            this.MouseLeave += TilePickerImage_MouseLeave;
        }

        void TilePickerImage_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            byte id = tiles[py][px];
            if (TileSeleted != null) TileSeleted.Invoke(id);
        }

        void TilePickerImage_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            cursor.Opacity = 0;
        }

        void TilePickerImage_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            cursor.Opacity = 0.75;
        }

        void TilePickerImage_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Point mousePos = e.GetPosition(sender as System.Windows.IInputElement);
            double x = (mousePos.X - mousePos.X % MapEditor.CELL_SIZE);
            double y = (mousePos.Y - mousePos.Y % MapEditor.CELL_SIZE);
            px = (byte)(x / MapEditor.CELL_SIZE);
            py = (byte)(y / MapEditor.CELL_SIZE);
            Canvas.SetLeft(cursor, x);
            Canvas.SetTop(cursor, y);
        }

        public void SetTiles(byte[][] tiles)
        {
            this.tiles = tiles;
            Update();
        }

        private void Update()
        {
            int width = tiles[0].Length;
            int height = tiles.Length;
            image.Width = width * MapEditor.CELL_SIZE;
            image.Height = height * MapEditor.CELL_SIZE;
            var bmp = new System.Drawing.Bitmap((int)image.Width, (int)image.Height);
            var gr = System.Drawing.Graphics.FromImage(bmp);

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    byte tileId = tiles[i][j];
                    var tileBmp = MapEditor.getTileBitmap(tileId);
                    if (tileBmp != null)
                        gr.DrawImage(tileBmp, j * MapEditor.CELL_SIZE, i * MapEditor.CELL_SIZE);
                }
            image.Source = MIDP.WPF.Media.ImageHelper.loadBitmap(bmp);
            this.Width = image.Width;
            this.Height = image.Height;
        }
        
    }
}
