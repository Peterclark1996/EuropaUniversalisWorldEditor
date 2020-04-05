namespace EuropaUniversalisWorldEditor
{
    public class World
    {

        public WorldSettings WorldSettings { get; }
        public ProvinceMap ProvinceMap { get; }
        
        public World(WorldSettings worldSettings)
        {
            WorldSettings = worldSettings;
            ProvinceMap = new ProvinceMap(worldSettings);
        }
    }
}