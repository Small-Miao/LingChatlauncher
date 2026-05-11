using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using LingChat.Navigation;
using Wpf.Ui.Controls;

namespace LingChat
{
    public partial class MainWindow : FluentWindow
    {
        private readonly Dictionary<string, AppPageDefinition> _pageDefinitions = [];
        private readonly Dictionary<string, UserControl> _pageInstances = [];
        private readonly Dictionary<string, FrameworkElement> _headerInstances = [];
        private readonly Dictionary<string, NavigationViewItem> _navigationItems = [];
        private string? _activePageKey;

        public MainWindow()
        {
            InitializeComponent();
            BuildNavigation();
#if DEBUG
            ModeTextBlock.Text = "Debug Mode";
#endif
        }

        private void BuildNavigation()
        {
            IReadOnlyList<AppPageDefinition> pages = AppPageRegistry.GetPages();

            foreach (AppPageDefinition page in pages)
            {
                _pageDefinitions[page.Key] = page;
            }

            foreach (AppPageDefinition page in pages)
            {
                NavigationViewItem item = new()
                {
                    Content = page.Title,
                    Tag = page.Key
                };

                item.Click += NavigationItem_OnClick;
                _navigationItems[page.Key] = item;

                if (page.Placement == NavigationPlacement.Main)
                {
                    RootNavigationView.MenuItems.Add(item);
                }
            }

            List<AppPageDefinition> footerPages = [];
            foreach (AppPageDefinition page in pages)
            {
                if (page.Placement == NavigationPlacement.Footer)
                {
                    footerPages.Add(page);
                }
            }

            if (footerPages.Count > 0)
            {

                foreach (AppPageDefinition page in footerPages)
                {
                    RootNavigationView.FooterMenuItems.Add(_navigationItems[page.Key]);
                }
            }

            if (pages.Count > 0)
            {
                ShowPage(pages[0].Key);
            }
        }

        private void NavigationItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not NavigationViewItem navigationItem || navigationItem.Tag is not string pageKey)
            {
                return;
            }

            ShowPage(pageKey);
        }

        private void ShowPage(string pageKey)
        {
            if (!_pageDefinitions.TryGetValue(pageKey, out AppPageDefinition? pageDefinition))
            {
                return;
            }

            if (_activePageKey is not null && _navigationItems.TryGetValue(_activePageKey, out NavigationViewItem? previousItem))
            {
                previousItem.IsActive = false;
            }

            if (!_pageInstances.TryGetValue(pageKey, out UserControl? pageInstance))
            {
                pageInstance = pageDefinition.Factory();
                _pageInstances[pageKey] = pageInstance;
            }

            UpdatePageHeader(pageKey, pageDefinition);
            PageContentHost.Content = pageInstance;
            _navigationItems[pageKey].IsActive = true;
            _activePageKey = pageKey;
        }

        private void UpdatePageHeader(string pageKey, AppPageDefinition pageDefinition)
        {
            if (pageDefinition.ShowHeader == false)
            {
                PageHeaderHost.Content = null;
                PageHeaderHost.Visibility = Visibility.Collapsed;
                PageHeaderSpacerRow.Height = new GridLength(0);
                return;
            }

            FrameworkElement header = pageDefinition.HeaderFactory is null
                ? CreateDefaultPageHeader(pageDefinition)
                : GetOrCreateCustomHeader(pageKey, pageDefinition.HeaderFactory);

            PageHeaderHost.Content = header;
            PageHeaderHost.Visibility = Visibility.Visible;
            PageHeaderSpacerRow.Height = new GridLength(20);
        }

        private FrameworkElement GetOrCreateCustomHeader(string pageKey, System.Func<FrameworkElement> headerFactory)
        {
            if (_headerInstances.TryGetValue(pageKey, out FrameworkElement? header))
            {
                return header;
            }

            header = headerFactory();
            _headerInstances[pageKey] = header;
            return header;
        }

        private static FrameworkElement CreateDefaultPageHeader(AppPageDefinition pageDefinition)
        {
            Card card = new()
            {
                Padding = new Thickness(20)
            };

            StackPanel stack = new();
            stack.Children.Add(new System.Windows.Controls.TextBlock
            {
                Text = pageDefinition.Title,
                FontSize = 30,
                FontWeight = FontWeights.SemiBold
            });
            stack.Children.Add(new System.Windows.Controls.TextBlock
            {
                Margin = new Thickness(0, 8, 0, 0),
                Text = pageDefinition.Description,
                TextWrapping = TextWrapping.Wrap
            });

            card.Content = stack;
            return card;
        }
    }
}
