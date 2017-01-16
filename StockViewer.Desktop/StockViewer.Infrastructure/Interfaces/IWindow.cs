using System;

namespace StockViewer.Infrastructure.Interfaces
{
    /// <summary>
    /// Interface for window objects.
    /// </summary>
    public interface IWindow
    {
        #region Evenst

        /// <summary>
        /// Occurs when window is closed.
        /// </summary>
        event EventHandler Closed;

        #endregion

        /// <summary>
        /// Shows the window but does not block the UI.
        /// </summary>
        void Show();

        /// <summary>
        /// Shows the window and blocks the UI.
        /// </summary>
        /// <returns></returns>
        bool? ShowDialog();
    }
}
