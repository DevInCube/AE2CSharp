using java.csharp;
using java.io;
using MIDP.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace AE2.Tools.Views
{
    /// <summary>
    /// Interaction logic for MapEditor.xaml
    /// </summary>
    public partial class MapEditor : Window, INotifyPropertyChanged
    {

        const int CELL_SIZE = 24; 
        const byte DEFAULT_TILE = 0;
        private List<List<byte>> mapData;
        private Point cursorPos, cursorPos2;
        private List<Point> selectionPos = new List<Point>();
        private Image cursor, cursor2;

        private Image tilesCanvasImage = new Image();

        private Image mapCanvasImage = new Image();
        private Image mapSelectionCanvasImage = new Image();
        private System.Drawing.Bitmap selBitmap;

        public int Width { get { return mapData.Count == 0 ? 0 : mapData[0].Count; } }
        public int Height { get { return mapData.Count; } }

        public int AddColX
        {
            get { return Width * CELL_SIZE; }
        }

        public int AddRowY
        {
            get { return Height * CELL_SIZE; }
        }

        public ICommand AddColumn { get; set; }
        public ICommand AddRow { get; set; }
        public ICommand SaveMap { get; set; }

        public MapEditor()
        {
            InitializeComponent();
            this.DataContext = this;

            E_MainCanvas.loadResourcesPak(null);
            selBitmap = getSelectionBitmap();

            mapData = new List<List<byte>>();
            for (int i = 0; i < 5; i++)
            {
                var row = new List<byte>();
                for (int j = 0; j < 5; j++)
                    row.Add(DEFAULT_TILE);
                mapData.Add(row);
            }

            cursor = new Image();
            cursor.Source = getCursorImage();
            cursor.Width = CELL_SIZE;
            cursor.Height = CELL_SIZE;

            cursor2 = new Image();
            cursor2.Source = cursor.Source;
            cursor2.Width = CELL_SIZE;
            cursor2.Height = CELL_SIZE;       

            MapCanvas.Children.Add(mapCanvasImage);
            MapCanvas.Children.Add(mapSelectionCanvasImage);
            MapCanvas.Children.Add(cursor);

            TileSelector.Children.Add(tilesCanvasImage);
            TileSelector.Children.Add(cursor2);             

            this.AddColumn = new RelayCommand((o) =>
            {
                foreach (var l in mapData)
                    l.Add(DEFAULT_TILE);
                Update();
            });

            this.AddRow = new RelayCommand((o) =>
            {
                var row = new List<byte>();
                mapData.Add(row);
                int count = (mapData.Count == 0) ? 1 : mapData[0].Count;
                for (int i = 0; i < count; i++)
                    row.Add(DEFAULT_TILE);
                Update();
            });
            SaveMap = new RelayCommand((o) => { 
                byte[] data = GetSaveMapData();
                File.WriteAllBytes("newMap.aem", data);
            });

            Update();
        }
      

        private void Update()
        {
            Canvas mapCanvas = this.MapCanvas;
            UpdateMap();
            UpdateSelections();
            Canvas.SetLeft(cursor, (double)cursorPos.X);
            Canvas.SetTop(cursor, (double)cursorPos.Y);            
            OnPropertyChanged("AddColX");
            OnPropertyChanged("AddRowY");

            UpdateTiles();
            Canvas.SetLeft(cursor2, (double)cursorPos2.X);
            Canvas.SetTop(cursor2, (double)cursorPos2.Y);            
        }

        private void UpdateSelections()
        {
            mapSelectionCanvasImage.Width = Width * CELL_SIZE;
            mapSelectionCanvasImage.Height = Height * CELL_SIZE;
            var mapBitmap = new System.Drawing.Bitmap((int)mapCanvasImage.Width, (int)mapCanvasImage.Height);            
            var gr = System.Drawing.Graphics.FromImage(mapBitmap);
            
            foreach (var pos in selectionPos)
            {
                gr.DrawImage(selBitmap,
                    new System.Drawing.Rectangle((int)pos.X, (int)pos.Y, CELL_SIZE, CELL_SIZE),
                    new System.Drawing.Rectangle(0, 0, CELL_SIZE, CELL_SIZE), 
                    System.Drawing.GraphicsUnit.Pixel);
            }
            mapSelectionCanvasImage.Source = MIDP.WPF.Media.ImageHelper.loadBitmap(mapBitmap);
        }

        public void UpdateMap()
        {
            mapCanvasImage.Width = Width * CELL_SIZE;
            mapCanvasImage.Height = Height * CELL_SIZE;
            var mapBitmap = new System.Drawing.Bitmap((int)mapCanvasImage.Width, (int)mapCanvasImage.Height);
            var gr = System.Drawing.Graphics.FromImage(mapBitmap);

            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                {
                    byte tileId = mapData[i][j];
                    var tileBmp = getTileBitmap(tileId);
                    int x = j * tileBmp.Width;
                    int y = i * tileBmp.Height;
                    gr.DrawImage(tileBmp, x, y);
                }
            mapCanvasImage.Source = MIDP.WPF.Media.ImageHelper.loadBitmap(mapBitmap);
         
        }

        public void UpdateTiles()
        {
            var image = tilesCanvasImage;
            int width = 9;
            int height = 9;
            image.Width = width * CELL_SIZE;
            image.Height = height * CELL_SIZE;
            var bmp = new System.Drawing.Bitmap((int)image.Width, (int)image.Height);
            var gr = System.Drawing.Graphics.FromImage(bmp);

            byte tileId = 0;            
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {                    
                    var tileBmp = getTileBitmap(tileId++);
                    if (tileBmp != null)
                        gr.DrawImage(tileBmp, j * CELL_SIZE, i * CELL_SIZE);
                }
            image.Source = MIDP.WPF.Media.ImageHelper.loadBitmap(bmp);
        }

        private BitmapImage getTileImage(byte tileId)
        {
            string id = (tileId < 10) ? ("0" + tileId) : tileId.ToString();
            BitmapImage carBitmap = Convert(E_MainCanvas.getResourceData("tiles0_" + id + ".png"));
            return carBitmap;
        }

        private System.Drawing.Bitmap getTileBitmap(byte tileId)
        {
            if (tileId <= 36)
            {
                string id = (tileId < 10) ? ("0" + tileId) : tileId.ToString();
                byte[] imageData = E_MainCanvas.getResourceData("tiles0_" + id + ".png");
                return FromBytes(imageData);
            }
            else if (tileId <= 46)
            {
                System.Drawing.Bitmap cell = new System.Drawing.Bitmap(CELL_SIZE, CELL_SIZE);
                var gr = System.Drawing.Graphics.FromImage(cell);
                int playerId = (tileId - 37) / 2;//@todo
                var bmp = getBuildingsBitmap(playerId);
                int i = (tileId - 37) % 2;
                gr.DrawImage(bmp,
                    new System.Drawing.Rectangle(0, 0, CELL_SIZE, CELL_SIZE),
                    new System.Drawing.Rectangle(CELL_SIZE * i, 0, CELL_SIZE, CELL_SIZE),
                    System.Drawing.GraphicsUnit.Pixel
                    );
                return cell;
            }
            return null;
        }

        private System.Drawing.Bitmap getBuildingsBitmap(int playerId)
        {                        
            byte[] imageData = E_MainCanvas.getResourceData("buildings.png");
            byte[] data = new byte[imageData.Length];
            System.Array.Copy(imageData, 0, data, 0, imageData.Length);
            aeii.H_ImageExt.setPlayerColor(data, playerId);
            return FromBytes(data);
        }

        private System.Drawing.Bitmap FromBytes(byte[] imageData)
        {
            using (var ms = new MemoryStream(imageData))
            {
               return new System.Drawing.Bitmap(ms);
            }            
        }

        private BitmapImage getCursorImage()
        {
            BitmapImage carBitmap = Convert(E_MainCanvas.getResourceData("cursor_00.png"));
            return carBitmap;
        }

        private ImageSource getSelectionImage()
        {
            string id = "alpha_grid.png";
            BitmapImage carBitmap = Convert(E_MainCanvas.getResourceData(id));
            return carBitmap;
        }

        private System.Drawing.Bitmap getSelectionBitmap()
        {            
            string id = "alpha_grid.png";            
            byte[] imageData = E_MainCanvas.getResourceData(id);
            return FromBytes(imageData);
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

        #region propchanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(sender as IInputElement);
            cursorPos.X = mousePos.X - mousePos.X % CELL_SIZE;
            cursorPos.Y = mousePos.Y - mousePos.Y % CELL_SIZE;
            Update();
        }

        private void MapCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(sender as IInputElement);
            int mX = (int)(mousePos.X - mousePos.X % CELL_SIZE);
            int mY = (int)(mousePos.Y - mousePos.Y % CELL_SIZE);

            bool removed = selectionPos.Remove(selectionPos.FirstOrDefault(sel => (sel.X == mX && sel.Y == mY)));
            if(!removed)
            {
                if (e.ChangedButton == MouseButton.Left)
                    selectionPos.Clear();
                var newSel = new Point(mX, mY);
                selectionPos.Add(newSel);
            }
            Update();
        }

        private void TileSelector_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(sender as IInputElement);
            cursorPos2.X = mousePos.X - mousePos.X % CELL_SIZE;
            cursorPos2.Y = mousePos.Y - mousePos.Y % CELL_SIZE;
            Update();
        }

        private void TileSelector_MouseUp(object sender, MouseButtonEventArgs e)
        {
            byte tx = (byte)(cursorPos2.X / CELL_SIZE);
            byte ty = (byte)(cursorPos2.Y / CELL_SIZE);
            byte id = (byte)(ty * 9 + tx);

            foreach (var pos in selectionPos)
            {
                byte mx = (byte)(pos.X / CELL_SIZE);
                byte my = (byte)(pos.Y / CELL_SIZE);
                mapData[my][mx] = id;
            }
            Update();
        }

        public byte[] GetSaveMapData()
        {
            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            DataOutputStream dos = new DataOutputStream(baos);
            dos.writeInt(Width);
            dos.writeInt(Height);
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    dos.writeByte(mapData[j][i]);
            int skipLen = 0;
            dos.writeInt(skipLen);
            int sLength = 2;
            dos.writeInt(sLength);
            for (short i = 0; i < sLength; i++)
            {
                byte uType = (byte)((12 * i) + 9); //king
                dos.writeByte(uType);
                short pX = (short)((2 + i) * CELL_SIZE);
                short pY = (short)((2 + i) * CELL_SIZE);
                dos.writeShort(pX);
                dos.writeShort(pY);
            }
            //dos.close();
            return baos.toByteArray();
        }
    }
}
