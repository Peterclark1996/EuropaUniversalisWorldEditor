using System;
using System.Windows.Media.Imaging;

namespace EuropaUniversalisWorldEditor
{
    public class WorldSettings
    {
        public int Width { get; }
        public int Height { get; }
        public BitmapImage ReferenceImage { get; }

        public WorldSettings(int width, int height)
        {
            ReferenceImage = new BitmapImage();
            ReferenceImage.BeginInit();
            ReferenceImage.DecodePixelHeight = height;
            ReferenceImage.DecodePixelWidth = width;
            ReferenceImage.EndInit();
            
            Width = width;
            Height = height;
        }

        public WorldSettings(string path)
        {
            ReferenceImage = new BitmapImage(new Uri(path));
            Width = ReferenceImage.PixelWidth;
            Height = ReferenceImage.PixelHeight;
        }
    }
}