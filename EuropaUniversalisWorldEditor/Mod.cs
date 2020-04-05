using System.Security.Cryptography;

namespace EuropaUniversalisWorldEditor
{
    public class Mod
    {
        public string Name { get; }
        public World World { get; }

        public Mod(string name, World world)
        {
            Name = name;
            World = world;
        }
    }
}