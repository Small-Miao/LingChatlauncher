using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LingChat.Pages
{
    public partial class ChatPage : UserControl
    {
        private int _messageCount;

        public ChatPage()
        {
            InitializeComponent();
            AddSystemMessage("主界面已加载，当前使用页面注册模式。");
        }

        private void SendButton_OnClick(object sender, RoutedEventArgs e)
        {
            string message = MessageTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(message))
            {
                StatusTextBlock.Text = "请输入内容后再发送。";
                MessageTextBox.Focus();
                return;
            }

            AddUserMessage(message);
            AddSystemMessage($"已收到消息，长度 {message.Length} 个字符。");
            MessageTextBox.Clear();
            MessageTextBox.Focus();
        }

        private void InsertTemplateButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageTextBox.Text = "请总结当前需求，并给出下一步实现建议。";
            MessageTextBox.CaretIndex = MessageTextBox.Text.Length;
            MessageTextBox.Focus();
            StatusTextBlock.Text = "已插入示例模板。";
        }

        private void ClearConversationButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessagesListBox.Items.Clear();
            _messageCount = 0;
            CounterTextBlock.Text = "消息数：0";
            StatusTextBlock.Text = "会话已清空。";
            AddSystemMessage("新的会话已开始。");
        }

        private void AddUserMessage(string message)
        {
            AddMessage("用户", message, "#2F6FED");
            StatusTextBlock.Text = "上一条消息来自用户。";
        }

        private void AddSystemMessage(string message)
        {
            AddMessage("系统", message, "#0F9D58");
        }

        private void AddMessage(string role, string message, string accentColor)
        {
            Brush accentBrush = CreateBrush(accentColor);

            Border container = new()
            {
                Margin = new Thickness(0, 0, 0, 12),
                Padding = new Thickness(14),
                CornerRadius = new CornerRadius(12),
                BorderBrush = accentBrush,
                BorderThickness = new Thickness(1),
                Background = CreateBrush("#14000000")
            };

            StackPanel content = new();
            content.Children.Add(new TextBlock
            {
                Text = $"{role}  {DateTime.Now:HH:mm:ss}",
                FontWeight = FontWeights.SemiBold,
                Foreground = accentBrush
            });
            content.Children.Add(new TextBlock
            {
                Margin = new Thickness(0, 8, 0, 0),
                Text = message,
                TextWrapping = TextWrapping.Wrap
            });

            container.Child = content;
            MessagesListBox.Items.Add(container);
            MessagesListBox.ScrollIntoView(container);

            _messageCount++;
            CounterTextBlock.Text = $"消息数：{_messageCount}";
        }

        private static Brush CreateBrush(string color)
        {
            return (Brush)new BrushConverter().ConvertFrom(color)!;
        }
    }
}
