using StockViewer.Infrastructure.Interfaces;
using System.Windows;

namespace StockViewer.Infrastructure.UI
{
    /// <summary>
    /// The base window class. Done to apply default style for all windows in the application.
    /// http://stackoverflow.com/questions/4279773/wpf-window-style-not-being-applied
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    public class BaseWindow : Window, IWindow
    {
        public BaseWindow()
        {
            Style = (Style)Application.Current.Resources["WindowDefaultStyle"];
        }
    }
}
