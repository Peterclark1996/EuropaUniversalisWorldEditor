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
        private readonly ViewPoint _viewPoint;
        private readonly Selection _selection;
        
        private Mod _currentMod;
        
        public MainWindow()
        {
            LoadSettings();

            InitializeComponent();
            
            _viewPoint = new ViewPoint(Background);
            _selection = new Selection(Canvas, _currentMod);
            
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
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            _viewPoint.Lock();
        }

        private void CanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                DrawPixel(MousePositionToPixelPosition(e.GetPosition(Background).X),
                    MousePositionToPixelPosition(e.GetPosition(Background).Y));
            }
        }

        private void CanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                DrawPixel(MousePositionToPixelPosition(e.GetPosition(Background).X),
                    MousePositionToPixelPosition(e.GetPosition(Background).Y));
            }
            else
            {
                _selection.SelectPixel(MousePositionToPixelPosition(e.GetPosition(Background).X),
                    MousePositionToPixelPosition(e.GetPosition(Background).Y));
            }
        }

        private int MousePositionToPixelPosition(double v)
        {
            return (int)(v * 4.41379310345d);//TODO FInd how to calculate this magic number
        }

        private void DrawPixel(int x, int y)
        {
            _currentMod.World.ProvinceMap.ModifyImagePixel(x, y, _selection.GetSelectedPixel());
        }
    }
}