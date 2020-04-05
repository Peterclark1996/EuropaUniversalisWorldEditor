using System;

namespace EuropaUniversalisWorldEditor
{
    public class ProvinceMap
    {
        private readonly WorldSettings _worldSettings;
        private readonly Pixel[,] _map;

        public ProvinceMap(WorldSettings worldSettings)
        {
            _worldSettings = worldSettings;
            _map = new Pixel[worldSettings.Width, worldSettings.Height];

            if (worldSettings.IsNewWorld())
            {
                var random = new Random();
            
                for (var x = 0; x < worldSettings.Width; x++)
                {
                    for (var y = 0; y < worldSettings.Height; y++)
                    {
                        _map[x, y] = new Pixel((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
                    }
                }
            }
            else
            {
                var nStride = (_worldSettings.ReferenceImage.PixelWidth * _worldSettings.ReferenceImage.Format.BitsPerPixel + 7) / 8;
                var pixelByteArray = new byte[_worldSettings.ReferenceImage.PixelHeight * nStride];
                _worldSettings.ReferenceImage.CopyPixels(pixelByteArray, nStride, 0);

                var count = 0;
                for (var x = 0; x < worldSettings.Width; x++)
                {
                    for (var y = 0; y < worldSettings.Height; y++)
                    {
                        _map[x, y] = new Pixel(pixelByteArray[count + 2], pixelByteArray[count + 1], pixelByteArray[count]);
                        count += 4;
                    }
                }
            }
        }

        public Pixel GetPixelAt(int x, int y)
        {
            if (x > _worldSettings.Width || x < 0 || y > _worldSettings.Height || y < 0)
            {
                return new Pixel(0, 0, 0);
            }
            return _map[x, y];
        }
    }
}