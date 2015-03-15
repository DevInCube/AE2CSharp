using AE2.Tools.Views.EditorViews;
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

        public const int CELL_SIZE = 24; 
        public const byte DEFAULT_TILE = 0;
        private List<List<byte>> mapData = new List<List<byte>>();
        private Point cursorPos, cursorPos2;
        private List<MapPosition> mapSelections = new List<MapPosition>();
        private MapPosition[] kingsPositions = new MapPosition[4];
        private Image cursor, cursor2;

        private Image tilesCanvasImage = new Image();

        private Image mapCanvasImage = new Image();
        private Image kingsCanvasImage = new Image();
        private Image mapSelectionCanvasImage = new Image();
        private System.Drawing.Bitmap selBitmap;
        private System.Drawing.Bitmap[] unitsBitmap;

        public int MapWidth { get { return mapData.Count == 0 ? 0 : mapData[0].Count; } }
        public int MapHeight { get { return mapData.Count; } }

        public int AddColX
        {
            get { return MapWidth * CELL_SIZE; }
        }

        public int AddRowY
        {
            get { return MapHeight * CELL_SIZE; }
        }

        public ICommand AddColumn { get; set; }
        public ICommand AddRow { get; set; }
        public ICommand AddRowCol { get; set; }
        public ICommand NewMap { get; set; }
        public ICommand LoadMap { get; set; }
        public ICommand SaveMap { get; set; }
        public ICommand GenIsland { get; set; }
        public ICommand GenWater { get; set; }
        public ICommand GenRiver { get; set; }
        public ICommand GenRoad { get; set; }
        public ICommand GenForest { get; set; }

        public MapEditor()
        {
            InitializeComponent();
            this.DataContext = this;

            E_MainCanvas.loadResourcesPak(null);
            selBitmap = getSelectionBitmap();
            unitsBitmap = new System.Drawing.Bitmap[4];
            for (int i = 0; i < unitsBitmap.Count(); i++)
                unitsBitmap[i] = getUnitsBitmap(i + 1);

            cursor = new Image();
            cursor.Source = getCursorImage();
            cursor.Width = CELL_SIZE;
            cursor.Height = CELL_SIZE;

            cursor2 = new Image();
            cursor2.Source = cursor.Source;
            cursor2.Width = CELL_SIZE;
            cursor2.Height = CELL_SIZE;       

            MapCanvas.Children.Add(mapCanvasImage);
            MapCanvas.Children.Add(kingsCanvasImage);
            MapCanvas.Children.Add(mapSelectionCanvasImage);
            MapCanvas.Children.Add(cursor);          

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
                for (int i = 0; i < MapWidth; i++)
                    row.Add(DEFAULT_TILE);
                Update();
            });
            this.AddRowCol = new RelayCommand((o) =>
            {
                foreach (var l in mapData)
                    l.Add(DEFAULT_TILE);
                var row = new List<byte>();
                mapData.Add(row);
                for (int i = 0; i < MapWidth; i++)
                    row.Add(DEFAULT_TILE);
                Update();
            });
            NewMap = new RelayCommand((o) => {
                //@todo
            });
            LoadMap = new RelayCommand((o) => {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                // Set filter for file extension and default file extension 
                dlg.DefaultExt = ".aem";
                dlg.Filter = "AEM Files (*.aem)|*.aem";
                // Display OpenFileDialog by calling ShowDialog method 
                Nullable<bool> result = dlg.ShowDialog();
                // Get the selected file name and display in a TextBox 
                if (result == true)
                {
                    // Open document 
                    string filename = dlg.FileName;
                    byte[] bytes = File.ReadAllBytes(filename);
                    LoadMapData(bytes);
                }
            });
            SaveMap = new RelayCommand((o) => { 
                byte[] data = GetSaveMapData();
                File.WriteAllBytes("newMap.aem", data);
            });

            GenIsland = new SimpleCommand(GenerateIsland);
            GenWater = new SimpleCommand(GenerateWater);
            GenRiver = new SimpleCommand(GenerateRiver);
            GenRoad = new SimpleCommand(GenerateRoad);
            GenForest = new SimpleCommand(GenerateForest);

            List<TilePickerImage> pickers = new List<TilePickerImage>();

            TilePickerImage terrain = new TilePickerImage();            
            terrain.SetTiles(new byte[][] {
                new byte[] {18, 19, 17, 16, 15},
            });
            pickers.Add(terrain);

            TilePickerImage roads = new TilePickerImage();
            roads.SetTiles(new byte[][] { 
                new byte[] {26,23,18,18},                
                new byte[] {25,22,20,23},                
                new byte[] {18,21,18,21},                
                new byte[] {18,25,20,24},                
            });
            pickers.Add(roads);

            TilePickerImage water = new TilePickerImage();
            water.SetTiles(new byte[][] { 
                new byte[] { 0,5,6,7,1 },                
                new byte[] { 5,4,18,8,7 },                
                new byte[] { 3,18,18,18,9 },                
                new byte[] { 10,13,18,14,12 },                
                new byte[] { 1,10,11,12,0 },                
            });
            pickers.Add(water);

            TilePickerImage plane = new TilePickerImage();
            plane.SetTiles(new byte[][] { 
                new byte[] { 18, 36, 18}, 
                new byte[] { 33, 34, 35 } 
            });
            pickers.Add(plane);

            TilePickerImage buildings = new TilePickerImage();
            buildings.SetTiles(new byte[][] { 
                new byte[] {38,40,42,44,46},
                new byte[] {37,39,41,43,45},                
            });
            pickers.Add(buildings);

            TilePickerImage misc = new TilePickerImage();
            misc.SetTiles(new byte[][] { 
                new byte[] {29,30,31,32,27},                             
            });
            pickers.Add(misc);            

            foreach (var picker in pickers)
            {
                picker.TileSelected += picker_TileSeleted;
                CanvasStack.Children.Add(picker);
            }

            TilePickerImage units = new TilePickerImage();            
            units.SetUnits(new byte[][] { 
                new byte[] {0,12,24,36},
            });
            units.TileSelected += units_TileSeleted;
            CanvasStack.Children.Add(units);

            int mapWidth = 10;
            int mapHeight = 10;
            byte defaultTile = DEFAULT_TILE;
            CreateNewMap(mapWidth, mapHeight, defaultTile);

            Update();
        }

        void units_TileSeleted(byte obj)
        {
            int playerId = 1 + obj / 12;
            if (mapSelections.Count > 0)
                kingsPositions[playerId - 1] = new MapPosition(mapSelections.First());
            UpdateKings();
        }       

        private void CreateNewMap(int mapWidth, int mapHeight, byte defaultTile)
        {
            mapData = new List<List<byte>>();
            for (int i = 0; i < mapHeight; i++)
            {
                var row = new List<byte>();
                for (int j = 0; j < mapWidth; j++)
                    row.Add(defaultTile);
                mapData.Add(row);
            }
            UpdateMap();
        }

        void picker_TileSeleted(byte id)
        {
            foreach (var pos in mapSelections)
            {
                mapData[pos.Y][pos.X] = id;
            }
            UpdateMap();
        }
      

        private void Update()
        {
            Canvas mapCanvas = this.MapCanvas;
            UpdateMap();
            UpdateKings();
            UpdateSelections();                       
            OnPropertyChanged("AddColX");
            OnPropertyChanged("AddRowY");
            OnPropertyChanged("MapWidth");
            OnPropertyChanged("MapHeight");

            UpdateTiles();
            Canvas.SetLeft(cursor2, (double)cursorPos2.X);
            Canvas.SetTop(cursor2, (double)cursorPos2.Y);            
        }

        private void UpdateKings()
        {
            var image = kingsCanvasImage;
            image.Width = MapWidth * CELL_SIZE;
            image.Height = MapHeight * CELL_SIZE;
            var mapBitmap = new System.Drawing.Bitmap((int)mapCanvasImage.Width, (int)mapCanvasImage.Height);
            var gr = System.Drawing.Graphics.FromImage(mapBitmap);

            for (int i = 0; i < kingsPositions.Count();i++ )
            {
                MapPosition pos = kingsPositions[i];
                gr.DrawImage(unitsBitmap[i],
                    new System.Drawing.Rectangle((int)pos.X * CELL_SIZE, (int)pos.Y * CELL_SIZE, CELL_SIZE, CELL_SIZE),
                    new System.Drawing.Rectangle(0, 0, CELL_SIZE, CELL_SIZE),
                    System.Drawing.GraphicsUnit.Pixel);
            }
            image.Source = MIDP.WPF.Media.ImageHelper.loadBitmap(mapBitmap);
        }

        private void UpdateSelections()
        {
            mapSelectionCanvasImage.Width = MapWidth * CELL_SIZE;
            mapSelectionCanvasImage.Height = MapHeight * CELL_SIZE;
            var mapBitmap = new System.Drawing.Bitmap((int)mapCanvasImage.Width, (int)mapCanvasImage.Height);            
            var gr = System.Drawing.Graphics.FromImage(mapBitmap);
            
            foreach (var pos in mapSelections)
            {
                gr.DrawImage(selBitmap,
                    new System.Drawing.Rectangle((int)pos.X * CELL_SIZE, (int)pos.Y * CELL_SIZE, CELL_SIZE, CELL_SIZE),
                    new System.Drawing.Rectangle(0, 0, CELL_SIZE, CELL_SIZE), 
                    System.Drawing.GraphicsUnit.Pixel);
            }
            mapSelectionCanvasImage.Source = MIDP.WPF.Media.ImageHelper.loadBitmap(mapBitmap);
        }

        public void UpdateMap()
        {
            mapCanvasImage.Width = MapWidth * CELL_SIZE;
            mapCanvasImage.Height = MapHeight * CELL_SIZE;
            var mapBitmap = new System.Drawing.Bitmap((int)mapCanvasImage.Width, (int)mapCanvasImage.Height);
            var gr = System.Drawing.Graphics.FromImage(mapBitmap);

            for (int i = 0; i < MapHeight; i++)
                for (int j = 0; j < MapWidth; j++)
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

        public static System.Drawing.Bitmap getUnitsBitmap(int playerId)
        {
            string id = "unit_icons.png";
            byte[] imageData = E_MainCanvas.getResourceData(id);
            byte[] data = new byte[imageData.Length];
            System.Array.Copy(imageData, 0, data, 0, imageData.Length);
            aeii.H_ImageExt.setPlayerColor(data, playerId);
            return FromBytes(data);
        }

        public static System.Drawing.Bitmap getTileBitmap(byte tileId)
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

        private static System.Drawing.Bitmap getBuildingsBitmap(int playerId)
        {                        
            byte[] imageData = E_MainCanvas.getResourceData("buildings.png");
            byte[] data = new byte[imageData.Length];
            System.Array.Copy(imageData, 0, data, 0, imageData.Length);
            aeii.H_ImageExt.setPlayerColor(data, playerId);
            return FromBytes(data);
        }

        private static  System.Drawing.Bitmap FromBytes(byte[] imageData)
        {
            using (var ms = new MemoryStream(imageData))
            {
               return new System.Drawing.Bitmap(ms);
            }            
        }

        public static BitmapImage getCursorImage()
        {
            BitmapImage carBitmap = Convert(E_MainCanvas.getResourceData("cursor_00.png"));
            return carBitmap;
        }

        public static System.Drawing.Bitmap getSelectionBitmap()
        {            
            string id = "alpha_grid.png";            
            byte[] imageData = E_MainCanvas.getResourceData(id);
            return FromBytes(imageData);
        }

        private static  BitmapImage Convert(byte[] byteVal)
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
            MapPosition cPos = new MapPosition((sbyte)(mousePos.X / CELL_SIZE), (sbyte)(mousePos.Y / CELL_SIZE));
            if (cPos.IsWithin(0, 0, MapWidth, MapHeight))
            {
                cursorPos.X = mousePos.X - mousePos.X % CELL_SIZE;
                cursorPos.Y = mousePos.Y - mousePos.Y % CELL_SIZE;
                Canvas.SetLeft(cursor, (double)cursorPos.X);
                Canvas.SetTop(cursor, (double)cursorPos.Y);
                ProcessButtons(e);
                UpdateSelections();
            }
        }

        void ProcessButtons(MouseEventArgs e)
        {
            if (e.LeftButton.HasFlag(MouseButtonState.Pressed))
            {
                AddSelection();
            }

            if (e.RightButton.HasFlag(MouseButtonState.Pressed))
            {
                RemoveSelection();
            }

            if (e.MiddleButton.HasFlag(MouseButtonState.Pressed))
            {
                mapSelections.Clear();
            }
        }

        void AddSelection()
        {
            var newSel = new MapPosition((byte)(cursorPos.X / CELL_SIZE), (byte)(cursorPos.Y / CELL_SIZE));
            if (newSel.IsWithin(0, 0, MapWidth, MapHeight)
                && !mapSelections.Contains(newSel))
                mapSelections.Add(newSel);
        }

        void RemoveSelection()
        {
            mapSelections.Remove(mapSelections.FirstOrDefault(sel => 
                (sel.X == cursorPos.X / CELL_SIZE && sel.Y == cursorPos.Y / CELL_SIZE)));
        }

        private void MapCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ProcessButtons(e);
            UpdateSelections();

        }

        private void MapCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //
        }      

        public byte[] GetSaveMapData()
        {
            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            DataOutputStream dos = new DataOutputStream(baos);
            dos.writeInt(MapWidth);
            dos.writeInt(MapHeight);
            for (int i = 0; i < MapWidth; i++)
                for (int j = 0; j < MapHeight; j++)
                    dos.writeByte(mapData[j][i]);
            int skipLen = 0;
            dos.writeInt(skipLen);
            int sLength = 4;//@todo
            dos.writeInt(sLength);
            for (short i = 0; i < sLength; i++)
            {
                byte uType = (byte)((12 * i) + 9); //king
                dos.writeByte(uType);
                short pX = (short)(kingsPositions[i].X * CELL_SIZE);
                short pY = (short)(kingsPositions[i].Y * CELL_SIZE);
                dos.writeShort(pX);
                dos.writeShort(pY);
            }
            //dos.close();
            return baos.toByteArray();
        }

        private void LoadMapData(byte[] bytes)
        {
            ByteArrayInputStream stream = new ByteArrayInputStream(bytes);
            DataInputStream mapDis = new DataInputStream(stream);
            int mapWidth = mapDis.readInt();
            int mapHeight = mapDis.readInt();
            byte[][] tiles = JavaArray.New<byte>(mapWidth, mapHeight);            
            for (int mX = 0; mX < mapWidth; mX++)
            {
                for (int mY = 0; mY < mapHeight; mY++)
                {
                    tiles[mX][mY] = mapDis.readByte();                   
                }
            }
            //@todo
            mapData = new List<List<byte>>();
            for (int i = 0; i < mapHeight; i++)
            {
                var row = new List<byte>();
                for (int j = 0; j < mapWidth; j++)
                    row.Add(tiles[j][i]);
                mapData.Add(row);
            }
            int skip = mapDis.readInt();
            mapDis.skip(skip * 4);
            int sLength = mapDis.readInt();
            for (short i = 0; i < sLength; i = (short)(i + 1))
            {
                sbyte uType = (sbyte)mapDis.readByte();
                int posX = mapDis.readShort() / 24;
                int posY = mapDis.readShort() / 24;
                int playerId = (1 + uType / 12);
                kingsPositions[playerId - 1] = new MapPosition(posX, posY);
            }
            Update();
        }

        void GenerateIsland()
        {
            foreach (var pos in mapSelections)
            {
                mapData[pos.Y][pos.X] = 18;                
            }
            foreach (var pos in getBoundingPositions())
            {
                string template = createTemplate(getCellBoundings(pos));

                if (IsMatching(template, "-1-100-00")) mapData[pos.Y][pos.X] = 14;
                if (IsMatching(template, "-1-00100-")) mapData[pos.Y][pos.X] = 13;
                if (IsMatching(template, "-00100-1-")) mapData[pos.Y][pos.X] = 8;
                if (IsMatching(template, "00-001-1-")) mapData[pos.Y][pos.X] = 4;

                if (IsMatching(template, "-1-000000")) mapData[pos.Y][pos.X] = 11;
                if (IsMatching(template, "000000-1-")) mapData[pos.Y][pos.X] = 6;
                if (IsMatching(template, "-00100-00")) mapData[pos.Y][pos.X] = 9;
                if (IsMatching(template, "00-00100-")) mapData[pos.Y][pos.X] = 3;

                if (IsMatching(template, "----00-01")) mapData[pos.Y][pos.X] = 5;
                if (IsMatching(template, "---00-10-")) mapData[pos.Y][pos.X] = 7;
                if (IsMatching(template, "10-00----")) mapData[pos.Y][pos.X] = 12;
                if (IsMatching(template, "-01-00---")) mapData[pos.Y][pos.X] = 10;
            }
            UpdateMap();
        }

        void GenerateWater()
        {
            Random rand = new Random();
            foreach (var pos in mapSelections)
            {
                mapData[pos.Y][pos.X] = (byte)(rand.Next() % 2);
            }
            foreach (var pos in getBoundingPositions())
            {
                string template = createTemplate(getCellBoundings(pos));

                if (IsMatching(template, "-1-100-00")) mapData[pos.Y][pos.X] = 5;
                if (IsMatching(template, "-1-00100-")) mapData[pos.Y][pos.X] = 7;
                if (IsMatching(template, "-00100-1-")) mapData[pos.Y][pos.X] = 10;
                if (IsMatching(template, "00-001-1-")) mapData[pos.Y][pos.X] = 12;
                
                if (IsMatching(template, "-1-000000")) mapData[pos.Y][pos.X] = 6;
                if (IsMatching(template, "000000-1-")) mapData[pos.Y][pos.X] = 11;
                if (IsMatching(template, "-00100-00")) mapData[pos.Y][pos.X] = 3;
                if (IsMatching(template, "00-00100-")) mapData[pos.Y][pos.X] = 9;

                if (IsMatching(template, "----00-01")) mapData[pos.Y][pos.X] = 14;
                if (IsMatching(template, "---00-10-")) mapData[pos.Y][pos.X] = 13;
                if (IsMatching(template, "10-00----")) mapData[pos.Y][pos.X] = 4;
                if (IsMatching(template, "-01-00---")) mapData[pos.Y][pos.X] = 8;
            }
            UpdateMap();
        }

        void GenerateRiver()
        {
            Random rand = new Random();
            foreach (var pos in mapSelections)
            {                
                string template = createTemplate(getCellBoundings(pos));

                if (IsMatching(template, "----1---0")) mapData[pos.Y][pos.X] = 5;
                if (IsMatching(template, "----1-0--")) mapData[pos.Y][pos.X] = 7;
                if (IsMatching(template, "--0-1----")) mapData[pos.Y][pos.X] = 10;
                if (IsMatching(template, "0---1----")) mapData[pos.Y][pos.X] = 12;

                if (IsMatching(template, "-1-111-1-")
                    || IsMatching(template, "---111-1-")
                    || IsMatching(template, "-1--11-1-")
                    || IsMatching(template, "-1-11--1-")
                    || IsMatching(template, "-1-111---"))
                    mapData[pos.Y][pos.X] = (byte)(rand.Next() % 2);

                if (IsMatching(template, "----1--0-")) mapData[pos.Y][pos.X] = 6;
                if (IsMatching(template, "-0--1----")) mapData[pos.Y][pos.X] = 11;
                if (IsMatching(template, "----10---")) mapData[pos.Y][pos.X] = 3;
                if (IsMatching(template, "---01----")) mapData[pos.Y][pos.X] = 9;

                if (IsMatching(template, "-0-01----")) mapData[pos.Y][pos.X] = 14;
                if (IsMatching(template, "-0--10---")) mapData[pos.Y][pos.X] = 13;
                if (IsMatching(template, "----10-0-")) mapData[pos.Y][pos.X] = 4;
                if (IsMatching(template, "---01--0-")) mapData[pos.Y][pos.X] = 8;
                
            }
           
            UpdateMap();
        }

        void GenerateRoad()
        {
            foreach (var pos in mapSelections)
            {
                string template = createTemplate(getCellBoundings(pos));

                if (IsMatching(template, "---111---")
                    || IsMatching(template, "----11---")
                    || IsMatching(template, "---11----")) mapData[pos.Y][pos.X] = 20;
                if (IsMatching(template, "-1--1--1-")
                    || IsMatching(template, "----1--1-")
                    || IsMatching(template, "-1--1----")) mapData[pos.Y][pos.X] = 21;
                if (IsMatching(template, "---11--1-")) mapData[pos.Y][pos.X] = 23;
                if (IsMatching(template, "-1-11----")) mapData[pos.Y][pos.X] = 24;
                if (IsMatching(template, "-1--11---")) mapData[pos.Y][pos.X] = 25;
                if (IsMatching(template, "----11-1-")) mapData[pos.Y][pos.X] = 26;
                if (IsMatching(template, "-1-111-1-")
                    || IsMatching(template, "---111-1-")
                    || IsMatching(template, "-1--11-1-")
                    || IsMatching(template, "-1-11--1-")
                    || IsMatching(template, "-1-111---")) mapData[pos.Y][pos.X] = 22;

            }
            UpdateMap();
        }

        void GenerateForest()
        {
            Random rand = new Random();
            foreach (var pos in mapSelections)
            {
                mapData[pos.Y][pos.X] = (byte)(15 + (rand.Next() % 2));
            }
            UpdateMap();
        }

        MapPosition[] getCellBoundings(MapPosition pos)
        {
            return new MapPosition[]{
                    new MapPosition(pos.X - 1, pos.Y - 1),
                    new MapPosition(pos.X, pos.Y - 1),
                    new MapPosition(pos.X + 1, pos.Y - 1),
                    new MapPosition(pos.X - 1, pos.Y),
                    new MapPosition(pos),
                    new MapPosition(pos.X + 1, pos.Y),
                    new MapPosition(pos.X - 1, pos.Y + 1),
                    new MapPosition(pos.X, pos.Y + 1),
                    new MapPosition(pos.X + 1, pos.Y + 1)
                };
        }

        string createTemplate(MapPosition[] poss)
        {
            string t = "";
            foreach (var pos in poss)
            {
                if (!pos.IsWithin(0, 0, MapWidth, MapHeight))
                    t += "-";
                else
                    t += (mapSelections.Contains(pos)) ? "1" : "0";
            }
            return t;
        }

        bool IsMatching(string t1, string t2)
        {
            for (int i = 0; i < 9; i++)
            {
                if ((t1[i] == '-' && t2[i] != '1')
                    || t2[i] == '-') 
                    continue;
                if (t1[i] != t2[i]) return false;
            }
            return true;
        }

        List<MapPosition> getBoundingPositions()
        {
            List<MapPosition> bounds = new List<MapPosition>();
            foreach (var pos in mapSelections)
            {
                MapPosition[] positions = getCellBoundings(pos);
                foreach (var p in positions)
                {
                    if (p.IsWithin(0, 0, MapWidth, MapHeight)
                        && !mapSelections.Contains(p)
                        && !bounds.Contains(p))
                        bounds.Add(p);
                }
            }
            return bounds;
        }
      
    }
}
