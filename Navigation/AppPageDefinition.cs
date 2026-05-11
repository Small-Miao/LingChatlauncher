using System;
using System.Windows;
using System.Windows.Controls;

namespace LingChat.Navigation
{
    public sealed record AppPageDefinition(
        string Key,
        string Title,
        string Description,
        NavigationPlacement Placement,
        Func<UserControl> Factory,
        bool? ShowHeader = null,
        Func<FrameworkElement>? HeaderFactory = null
    );
}
