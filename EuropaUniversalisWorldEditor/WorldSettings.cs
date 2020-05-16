using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace EuropaUniversalisWorldEditor
{
    public class WorldSettings
    {
        public int Width { get; }
        public int Height { get; }
        public BitmapImage ReferenceImage { get; }

        public WorldSettings(string path)
        {
            ReferenceImage = new BitmapImage(new Uri(path));

            Width = ReferenceImage.PixelWidth;
            Height = ReferenceImage.PixelHeight;
        }
    }
}