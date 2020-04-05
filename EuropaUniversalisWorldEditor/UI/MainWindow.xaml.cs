using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EuropaUniversalisWorldEditor.UI
{
    public partial class MainWindow : Window
    {
        private readonly Camera _camera;
        private GlobalSettings _globalSettings;
        
        private Mod _currentMod;

        public MainWindow()
        {
            LoadSettings();

            InitializeComponent();
            
            _camera = new Camera(_currentMod, Background);
        }

        private void LoadSettings()
        {
            var dialog = new SetupWindow();
            if (dialog.ShowDialog() != true) return;
            
            //_globalSettings = new GlobalSettings(dialog.GamePath, dialog.ModsPath);
            _globalSettings = new GlobalSettings("G:/SteamLibrary/SteamApps/common/Europa Universalis IV",
                "C:/Users/Pete/Documents/Paradox Interactive/Europa Universalis IV/mod");
            
            _currentMod = new Mod(dialog.ModName, new World(new WorldSettings(_globalSettings.GamePath + "/map/provinces.bmp")));
        }

        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                case Key.S:
                    _camera.SetMovement(Direction.Down, true);
                    break;
                case Key.Up:
                case Key.W:
                    _camera.SetMovement(Direction.Up, true);
                    break;
                case Key.Left:
                case Key.A:
                    _camera.SetMovement(Direction.Left, true);
                    break;
                case Key.Right:
                case Key.D:
                    _camera.SetMovement(Direction.Right, true);
                    break;
            }
        }

        private void CanvasKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                case Key.S:
                    _camera.SetMovement(Direction.Down, false);
                    break;
                case Key.Up:
                case Key.W:
                    _camera.SetMovement(Direction.Up, false);
                    break;
                case Key.Left:
                case Key.A:
                    _camera.SetMovement(Direction.Left, false);
                    break;
                case Key.Right:
                case Key.D:
                    _camera.SetMovement(Direction.Right, false);
                    break;
            }
        }

        private void CanvasMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _camera.ModifyZoom(e.Delta);
        }
    }
}