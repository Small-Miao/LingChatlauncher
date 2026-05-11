using LingChat.Struct.LingChat;

namespace LingChat.Struct
{
    internal class Setting
    {
        public GlobalSetting GlobalSetting { get; set; } = new();
        public LingChatSetting LingChatSetting { get; set; } = new();
        public string LingChatInstallPath { get; set; } = "./LingChatInstall";
        public string TTSServicesInstallPath { get; set; } = "./TTSServicesInstall";
    }

    internal class GlobalSetting
    {
        public bool UseMirrorDownload { get; set; }
        public string LingChatInstallPath { get; set; } = "./LingChatInstall";
    }
}
