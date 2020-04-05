namespace EuropaUniversalisWorldEditor
{
    public class GlobalSettings
    {
        public string GamePath { get; }
        public string ModsPath { get; }

        public GlobalSettings(string gamePath, string modsPath)
        {
            GamePath = gamePath;
            ModsPath = modsPath;
        }
    }
}