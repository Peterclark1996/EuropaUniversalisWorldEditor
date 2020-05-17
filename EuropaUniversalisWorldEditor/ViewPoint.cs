using System;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Controls;

namespace EuropaUniversalisWorldEditor
{
    public class ViewPoint
    {
        private readonly Image _target;
        
        private const int ZoomMax = 20;
        private const int ZoomMin = 0;

        private int _zoomCurrent = 5;
        
        private const int CameraSpeed = 5;
        
        private int _viewOffsetX = 0;
        private int _viewOffsetY = 0;

        private bool _movingUp = false;
        private bool _movingDown = false;
        private bool _movingLeft = false;
        private bool _movingRight = false;
        
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
        
        public ViewPoint(Image target)
        {
            _target = target;
            
            var timer = new DispatcherTimer();
            timer.Tick += TickMovement;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Start();
        }

        public void MovementChange(Direction dir, bool enable)
        {
            switch (dir)
            {
                case Direction.Up:
                    _movingUp = enable;
                    break;
                case Direction.Down:
                    _movingDown = enable;
                    break;
                case Direction.Left:
                    _movingLeft = enable;
                    break;
                case Direction.Right:
                    _movingRight = enable;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }

        public void ZoomChange(int x, int y, int amount)
        {
            var matrix = _target.RenderTransform.Value;
            
            if (amount > 0)
            {
                if (_zoomCurrent < ZoomMax)
                {
                    matrix.ScaleAt(1.5, 1.5, x, y);
                    _viewOffsetX = (int)(_viewOffsetX * 1.5f);
                    _viewOffsetY = (int)(_viewOffsetY * 1.5f);
                    _zoomCurrent++;
                }
            }
            else
            {
                if (_zoomCurrent > ZoomMin)
                {
                    matrix.ScaleAt(1.0 / 1.5, 1.0 / 1.5, x, y);
                    _viewOffsetX = (int)(_viewOffsetX * (1.0 / 1.5));
                    _viewOffsetY = (int)(_viewOffsetY * (1.0 / 1.5));
                    _zoomCurrent--;
                }
            }

            _target.RenderTransform = new MatrixTransform(matrix);
        }
        
        private void TickMovement(object sender, EventArgs e)
        {
            if (_movingDown) _viewOffsetY -= CameraSpeed;
            if (_movingUp) _viewOffsetY += CameraSpeed;
            if (_movingLeft) _viewOffsetX += CameraSpeed;
            if (_movingRight) _viewOffsetX -= CameraSpeed;
            
            var matrix = _target.RenderTransform.Value;
            matrix.OffsetX = _viewOffsetX;
            matrix.OffsetY = _viewOffsetY;
            _target.RenderTransform = new MatrixTransform(matrix);
        }
    }
}