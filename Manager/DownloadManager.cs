using Downloader;
using System;
using System.Collections.Generic;
using System.Text;

namespace LingChat.Manager
{
    public static class DownloadManager
    {
        public static DownloadService? Downloader { get; private set; }

        public static void InitDownloaderServices()
        {
            Downloader = new DownloadService();
        }
    }
}
