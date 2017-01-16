using StockViewer.Infrastructure.Services;
using StockViewer.Infrastructure.Interfaces;
using System.ComponentModel.Composition;

namespace StockViewer.Infrastructure
{
    /// <summary>
    /// Messaging Service to show window messages.
    /// </summary>
    /// <seealso cref="IMessagingService" />
    [Export(typeof(IMessagingService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class MessagingService : IMessagingService
    {
        /// <summary>
        /// Shows the specified window.
        /// </summary>
        /// <param name="window">The window.</param>
        public void Show(IWindow window)
        {
            window.Show();
        }

        /// <summary>
        /// Shows the window dialog and block the UI.
        /// </summary>
        /// <param name="window">The window.</param>
        public bool ShowDialog(IWindow window)
        {
            var result = window.ShowDialog();
            if(result == null)
            {
                return false;
            }

            return result.Value;
        }
    }
}
