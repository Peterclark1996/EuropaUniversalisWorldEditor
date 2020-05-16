using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace EuropaUniversalisWorldEditor.UI
{
    public partial class MainWindow : Window
    {
        private GlobalSettings _globalSettings;
        
        private Mod _currentMod;
        
        private const int CameraSpeed = 5;
        
        private int _viewOffsetX = 0;
        private int _viewOffsetY = 0;

        private bool _movingDown = false;
        private bool _movingUp = false;
        private bool _movingLeft = false;
        private bool _movingRight = false;

        public MainWindow()
        {
            LoadSettings();

            InitializeComponent();
            
            RenderOptions.SetBitmapScalingMode(Background, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(Background, EdgeMode.Aliased);
            Background.Source = _currentMod.World.ProvinceMap.GetImage();
            
            var timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TickMovement);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Start();
        }

        private void LoadSettings()
        {
            var dialog = new SetupWindow();
            if (dialog.ShowDialog() != true) return;
            
            //_globalSettings = new GlobalSettings(dialog.GamePath, dialog.ModsPath);
            _globalSettings = new GlobalSettings("S:/Steam/SteamApps/common/Europa Universalis IV",
                "C:/Users/Pete/Documents/Paradox Interactive/Europa Universalis IV/mod");
            
            _currentMod = new Mod(new World(new WorldSettings(_globalSettings.GamePath + "/map/provinces.bmp")));
        }
        
        private void TickMovement(object sender, EventArgs e)
        {
            if (_movingDown) _viewOffsetY -= CameraSpeed;
            if (_movingUp) _viewOffsetY += CameraSpeed;
            if (_movingLeft) _viewOffsetX += CameraSpeed;
            if (_movingRight) _viewOffsetX -= CameraSpeed;
            
            var matrix = Background.RenderTransform.Value;
            matrix.OffsetX = _viewOffsetX;
            matrix.OffsetY = _viewOffsetY;
            Background.RenderTransform = new MatrixTransform(matrix);
        }

        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                case Key.S:
                    _movingDown = true;
                    break;
                case Key.Up:
                case Key.W:
                    _movingUp = true;
                    break;
                case Key.Left:
                case Key.A:
                    _movingLeft = true;
                    break;
                case Key.Right:
                case Key.D:
                    _movingRight = true;
                    break;
            }
        }

        private void CanvasKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                case Key.S:
                    _movingDown = false;
                    break;
                case Key.Up:
                case Key.W:
                    _movingUp = false;
                    break;
                case Key.Left:
                case Key.A:
                    _movingLeft = false;
                    break;
                case Key.Right:
                case Key.D:
                    _movingRight = false;
                    break;
            }
        }

        private void CanvasMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var matrix = Background.RenderTransform.Value;
            
            if (e.Delta > 0)
            {
                matrix.ScaleAt(1.5, 1.5, e.GetPosition(Canvas).X, e.GetPosition(Canvas).Y);
            }
            else
            {
                matrix.ScaleAt(1.0 / 1.5, 1.0 / 1.5, e.GetPosition(Canvas).X, e.GetPosition(Canvas).Y);
            }

            Background.RenderTransform = new MatrixTransform(matrix);
        }
    }
}