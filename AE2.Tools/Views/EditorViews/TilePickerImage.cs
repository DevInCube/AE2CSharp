using System;
using System.Windows.Controls;

namespace AE2.Tools.Views.EditorViews
{
    public class TilePickerImage : Canvas
    {

        private byte _selectedTile = 0;
        private byte px, py;
        private byte[][] tiles;
        private byte[][] units;
        private Image image, cursor, selectionImage;
        private System.Drawing.Bitmap selBitmap;
        private System.Drawing.Point selPos;

        public event Action<byte> TileSelected;

        public bool IsToggleEnabled { get; set; }
        public byte SelectedTile
        {
            get => _selectedTile;
            set
            {
                _selectedTile = value;
                if (IsToggleEnabled)
                {
                    MapPosition p = GetTilePos(_selectedTile);
                    TogglePos(p.X, p.Y);
                    UpdateSelection();
                }
            }
        }

        public TilePickerImage()
        {
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            image = new Image();
            this.Children.Add(image);

            selBitmap = MapEditor.getSelectionBitmap();
            selectionImage = new Image();
            this.Children.Add(selectionImage);

            cursor = new Image
            {
                Source = MapEditor.getCursorImage(),
                Width = MapEditor.CELL_SIZE,
                Height = MapEditor.CELL_SIZE,
                Opacity = 0
            };
            this.Children.Add(cursor);            

            this.MouseMove += TilePickerImage_MouseMove;
            this.MouseUp += TilePickerImage_MouseUp;
            this.MouseEnter += TilePickerImage_MouseEnter;
            this.MouseLeave += TilePickerImage_MouseLeave;
        }

        private void UpdateSelection()
        {
            selectionImage.Width = this.Width;
            selectionImage.Height = this.Height;
            var mapBitmap = new System.Drawing.Bitmap((int)selectionImage.Width, (int)selectionImage.Height);
            var gr = System.Drawing.Graphics.FromImage(mapBitmap);

            gr.DrawImage(selBitmap,
                   new System.Drawing.Rectangle((int)selPos.X, (int)selPos.Y, MapEditor.CELL_SIZE, MapEditor.CELL_SIZE),
                   new System.Drawing.Rectangle(MapEditor.CELL_SIZE, 0, MapEditor.CELL_SIZE, MapEditor.CELL_SIZE),
                   System.Drawing.GraphicsUnit.Pixel);

            selectionImage.Source = MIDP.WPF.Media.ImageHelper.loadBitmap(mapBitmap);
        }

        private void TogglePos(int px, int py)
        {
            selPos = new System.Drawing.Point(px * MapEditor.CELL_SIZE, py * MapEditor.CELL_SIZE);
            UpdateSelection();
        }

        public MapPosition GetTilePos(byte _SelectedTile)
        {
            if (tiles != null)
            {
                int width = tiles[0].Length;
                int height = tiles.Length;
               
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        byte tileId = tiles[i][j];
                        if (tileId == _SelectedTile)
                            return new MapPosition(j, i);
                    }
            }
            return new MapPosition(-1, -1);
        }

        private void TilePickerImage_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (IsToggleEnabled)
            {
                TogglePos(px, py);
            }
            try
            {
                if (tiles != null)
                {
                    byte id = tiles[py][px];
                    if (TileSelected != null) TileSelected.Invoke(id);
                }
                if (units != null)
                {
                    byte id = units[py][px];
                    if (TileSelected != null) TileSelected.Invoke(id);
                }
            }
            catch { }
        }

        private void TilePickerImage_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            cursor.Opacity = 0;
        }

        private void TilePickerImage_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            cursor.Opacity = 0.75;
        }

        private void TilePickerImage_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
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

        public void SetUnits(byte[][] units)
        {
            this.units = units;
            Update();
        }

        private void Update()
        {
            if (tiles != null)
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
            if (units != null)
            {
                int width = units[0].Length;
                int height = units.Length;
                image.Width = width * MapEditor.CELL_SIZE;
                image.Height = height * MapEditor.CELL_SIZE;
                var bmp = new System.Drawing.Bitmap((int)image.Width, (int)image.Height);
                var gr = System.Drawing.Graphics.FromImage(bmp);

                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        byte unitId = units[i][j];
                        int playerId = unitId / 12 + 1;
                        int unitType = unitId % 12;
                        var unitBmp = MapEditor.getUnitsBitmap(playerId);
                        if (unitBmp != null)
                            gr.DrawImage(unitBmp,
                               new System.Drawing.Rectangle(j * MapEditor.CELL_SIZE, i * MapEditor.CELL_SIZE, MapEditor.CELL_SIZE, MapEditor.CELL_SIZE),
                               new System.Drawing.Rectangle(unitType * MapEditor.CELL_SIZE, 0, MapEditor.CELL_SIZE, MapEditor.CELL_SIZE),
                               System.Drawing.GraphicsUnit.Pixel);
                    }
                image.Source = MIDP.WPF.Media.ImageHelper.loadBitmap(bmp);
                this.Width = image.Width;
                this.Height = image.Height;
            }

        }

    }
}
