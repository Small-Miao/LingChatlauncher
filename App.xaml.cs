using System.Windows;
using System.Windows.Threading;

namespace LingChat
{
    public partial class App
    {
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(
                e.Exception.Message,
                "Unhandled Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
            e.Handled = true;
        }
    }
}
