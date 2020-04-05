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
            Width = width;
            Height = height;
        }

        public WorldSettings(string path)
        {
            ReferenceImage = new BitmapImage(new Uri(path));
            Width = (int)ReferenceImage.Width;
            Height = (int)ReferenceImage.Height;
        }

        public bool IsNewWorld()
        {
            return ReferenceImage == null;
        }
    }
}