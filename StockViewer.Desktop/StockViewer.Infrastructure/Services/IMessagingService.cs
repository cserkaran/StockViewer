using StockViewer.Infrastructure.Interfaces;

namespace StockViewer.Infrastructure.Services
{
    /// <summary>
    /// Interface for showing message boxes.
    /// </summary>
    public interface IMessagingService
    {
        /// <summary>
        /// Shows the specified window.
        /// </summary>
        /// <param name="window">The window.</param>
        void Show(IWindow window);

        /// <summary>
        /// Shows the window dialog and block the UI.
        /// </summary>
        /// <param name="window">The window.</param>
        bool ShowDialog(IWindow window);
    }
}
