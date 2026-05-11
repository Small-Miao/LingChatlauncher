using Newtonsoft.Json;
using System.IO;

namespace LingChat.Struct.LingChat
{
    internal class LingChatElement
    {
        public LingChatElement(string installPath)
        {
            InstallPath = installPath;
        }

        public string InstallPath { get; }

        public string Version { get; private set; } = "Unknown";

        public bool LaunchWithNoGui { get; set; }

        public LingChatSetting ElementSetting { get; private set; } = new();

        public string ConfigFilePath => Path.Combine(InstallPath, "Config.json");

        public string VersionFilePath => Path.Combine(InstallPath, "version");

        public bool InstallDirectoryExists => Directory.Exists(InstallPath);

        public bool ConfigExists => File.Exists(ConfigFilePath);

        public bool VersionFileExists => File.Exists(VersionFilePath);

        public bool LoadLingChatSetting()
        {
            if (!ConfigExists)
            {
                ElementSetting = new LingChatSetting();
                return false;
            }

            LingChatSetting? setting = JsonConvert.DeserializeObject<LingChatSetting>(File.ReadAllText(ConfigFilePath));
            ElementSetting = setting ?? new LingChatSetting();
            return true;
        }

        public bool LoadVersion()
        {
            if (!VersionFileExists)
            {
                Version = "Not installed";
                return false;
            }

            Version = File.ReadAllText(VersionFilePath).Trim();
            if (string.IsNullOrWhiteSpace(Version))
            {
                Version = "Unknown";
            }

            return true;
        }
    }
}
