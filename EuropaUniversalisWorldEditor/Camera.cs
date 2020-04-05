using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace EuropaUniversalisWorldEditor
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };
    public class Camera
    {
        private readonly DrawingVisual _drawingVisual = new DrawingVisual();
        private readonly Mod _currentMod;
        private readonly RenderTargetBitmap _buffer;
        
        private const int MinPixelSize = 1;
        private const int MaxPixelSize = 100;
        private const int CameraSpeed = 5;

        private int _viewOffsetX = 0;
        private int _viewOffsetY = 0;
        
        private int _pixelSize = 5;

        private bool _movingDown = false;
        private bool _movingUp = false;
        private bool _movingLeft = false;
        private bool _movingRight = false;

        public Camera(Mod mod, Image target)
        {
            _currentMod = mod;

            _buffer = new RenderTargetBitmap((int) target.Width, (int) target.Height, 96, 96,
                PixelFormats.Pbgra32);

            target.Source = _buffer;

            var timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Start();
        }

        private void Draw()
        {
            if (_buffer == null) return;

            _buffer.Clear();
            
            using (var drawingContext = _drawingVisual.RenderOpen())
            {
                for (var x = _viewOffsetX/_pixelSize; x < Smallest((_viewOffsetX/_pixelSize) + (int)(_buffer.Width / _pixelSize), _currentMod.World.WorldSettings.Width); x++)
                {
                    for (var y = _viewOffsetY/_pixelSize; y < Smallest((_viewOffsetY/_pixelSize) + (int)(_buffer.Height / _pixelSize), _currentMod.World.WorldSettings.Height); y++)
                    {
                        var pixel = _currentMod.World.ProvinceMap.GetPixelAt(x, y);
                        drawingContext.DrawRectangle(new SolidColorBrush(Color.FromRgb(pixel.R, pixel.G, pixel.B)),
                            null,
                            new Rect((x - _viewOffsetX/_pixelSize) * _pixelSize, (y - _viewOffsetY/_pixelSize) * _pixelSize, _pixelSize, _pixelSize));
                    }
                }
            }

            _buffer.Render(_drawingVisual);
        }

        private static int Smallest(int a, int b)
        {
            return a < b ? a : b;
        }
        
        private void Tick(object sender, EventArgs e)
        {
            if (_movingDown) _viewOffsetY -= CameraSpeed;
            if (_movingUp) _viewOffsetY += CameraSpeed;
            if (_movingLeft) _viewOffsetX += CameraSpeed;
            if (_movingRight) _viewOffsetX -= CameraSpeed;
            Draw();
        }

        public void SetMovement(Direction direction, bool enabled)
        {
            switch (direction)
            {
                case Direction.Up:
                    _movingUp = enabled;
                    break;
                case Direction.Down:
                    _movingDown = enabled;
                    break;
                case Direction.Left:
                    _movingLeft = enabled;
                    break;
                case Direction.Right:
                    _movingRight = enabled;
                    break;
            }
        }

        public void ModifyZoom(int strength)
        {
            _pixelSize += (int)(strength / 120f);
            if (_pixelSize < MinPixelSize) _pixelSize = MinPixelSize;
            else if(_pixelSize > MaxPixelSize) _pixelSize = MaxPixelSize;
        }
    }
}