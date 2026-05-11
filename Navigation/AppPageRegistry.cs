using System.Collections.Generic;
using LingChat.Pages;

namespace LingChat.Navigation
{
    public static class AppPageRegistry
    {
        private static readonly IReadOnlyList<AppPageDefinition> Pages =
        [
            new("Main","主页","",NavigationPlacement.Main,static () => new MainPage(),ShowHeader:false),
            //new(
            //    "chat",
            //    "主界面",
            //    "主对话页面，布局使用 XAML，交互逻辑放在对应的 code-behind 中。",
            //    NavigationPlacement.Main,
            //    static () => new ChatPage()
            //),
            //new(
            //    "history",
            //    "历史记录",
            //    "用于承载会话记录、任务日志或本地历史列表。",
            //    NavigationPlacement.Main,
            //    static () => new HistoryPage()
            //),
            //new(
            //    "about",
            //    "关于",
            //    "展示项目说明、当前架构和后续扩展约定。",
            //    NavigationPlacement.Main,
            //    static () => new AboutPage(),
            //    HeaderFactory: static () => new AboutPageHeader()
            //),
            //new(
            //    "settings",
            //    "设置",
            //    "集中放置应用级配置入口，不引入 MVVM。",
            //    NavigationPlacement.Footer,
            //    static () => new SettingsPage(),
            //    ShowHeader: false
            //)
        ];

        public static IReadOnlyList<AppPageDefinition> GetPages()
        {
            return Pages;
        }
    }
}
