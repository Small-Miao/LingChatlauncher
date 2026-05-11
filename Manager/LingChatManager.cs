using LingChat.Struct;
using LingChat.Struct.LingChat;
using System.IO;

namespace LingChat.Manager
{
    internal class LingChatManager
    {
        public LingChatElement? CurrentElement { get; private set; }

        public LingChatElement RefreshCurrentLingChatElement(Setting setting)
        {
            string installPath = setting.LingChatInstallPath;
            if (string.IsNullOrWhiteSpace(installPath))
            {
                installPath = "./LingChatInstall";
            }

            CurrentElement = new LingChatElement(installPath);
            CurrentElement.LoadVersion();
            CurrentElement.LoadLingChatSetting();

            return CurrentElement;
        }

        public bool IsLingChatInstalled(Setting setting)
        {
            string installPath = setting.LingChatInstallPath;
            return !string.IsNullOrWhiteSpace(installPath) && Directory.Exists(installPath);
        }
    }
}
