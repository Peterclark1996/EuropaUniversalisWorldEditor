using System.Security.Cryptography;

namespace EuropaUniversalisWorldEditor
{
    public class Mod
    {
        public World World { get; }

        public Mod(World world)
        {
            World = world;
        }
    }
}