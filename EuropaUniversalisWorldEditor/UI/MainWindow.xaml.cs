using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace EuropaUniversalisWorldEditor.UI
{
    public partial class MainWindow : Window
    {
        private GlobalSettings _globalSettings;
        private ViewPoint _viewPoint;
        
        private Mod _currentMod;
        
        public MainWindow()
        {
            LoadSettings();

            InitializeComponent();
            
            _viewPoint = new ViewPoint(Background);
            
            RenderOptions.SetBitmapScalingMode(Background, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(Background, EdgeMode.Aliased);
            Background.Source = _currentMod.World.ProvinceMap.GetImage();
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

        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                case Key.S:
                    _viewPoint.MovementChange(ViewPoint.Direction.Down, true);
                    break;
                case Key.Up:
                case Key.W:
                    _viewPoint.MovementChange(ViewPoint.Direction.Up, true);
                    break;
                case Key.Left:
                case Key.A:
                    _viewPoint.MovementChange(ViewPoint.Direction.Left, true);
                    break;
                case Key.Right:
                case Key.D:
                    _viewPoint.MovementChange(ViewPoint.Direction.Right, true);
                    break;
            }
        }

        private void CanvasKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                case Key.S:
                    _viewPoint.MovementChange(ViewPoint.Direction.Down, false);
                    break;
                case Key.Up:
                case Key.W:
                    _viewPoint.MovementChange(ViewPoint.Direction.Up, false);
                    break;
                case Key.Left:
                case Key.A:
                    _viewPoint.MovementChange(ViewPoint.Direction.Left, false);
                    break;
                case Key.Right:
                case Key.D:
                    _viewPoint.MovementChange(ViewPoint.Direction.Right, false);
                    break;
            }
        }

        private void CanvasMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _viewPoint.ZoomChange((int)e.GetPosition(Canvas).X, (int)e.GetPosition(Canvas).Y, e.Delta);
            // var matrix = Background.RenderTransform.Value;
            //
            // if (e.Delta > 0)
            // {
            //     if (_zoomCurrent < ZoomMax)
            //     {
            //         matrix.ScaleAt(1.5, 1.5, e.GetPosition(Canvas).X, e.GetPosition(Canvas).Y);
            //         _viewOffsetX = (int)(_viewOffsetX * 1.5f);
            //         _viewOffsetY = (int)(_viewOffsetY * 1.5f);
            //         _zoomCurrent++;
            //     }
            // }
            // else
            // {
            //     if (_zoomCurrent > ZoomMin)
            //     {
            //         matrix.ScaleAt(1.0 / 1.5, 1.0 / 1.5, e.GetPosition(Canvas).X, e.GetPosition(Canvas).Y);
            //         _viewOffsetX = (int)(_viewOffsetX * (1.0 / 1.5));
            //         _viewOffsetY = (int)(_viewOffsetY * (1.0 / 1.5));
            //         _zoomCurrent--;
            //     }
            // }
            //
            // Background.RenderTransform = new MatrixTransform(matrix);
        }
    }
}