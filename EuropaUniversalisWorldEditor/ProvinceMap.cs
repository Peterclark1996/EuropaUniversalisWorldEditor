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
            try
            {
                var nStride = (_worldSettings.ReferenceImage.PixelWidth *
                    _worldSettings.ReferenceImage.Format.BitsPerPixel + 7) / 8;
                var pixelByteArray = new byte[_worldSettings.ReferenceImage.PixelHeight * nStride];
                _worldSettings.ReferenceImage.CopyPixels(pixelByteArray, nStride, 0);

                var count = 0;
                var x = 0;
                var y = 0;

                while (count <= pixelByteArray.Length)
                {
                    _map[x, y] = new Pixel(pixelByteArray[count + 2], pixelByteArray[count + 1], pixelByteArray[count]);
                    count += 4;
                    x++;

                    if (x < _worldSettings.Width) continue;
                    x = 0;
                    y++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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