using System.Windows;

namespace StockViewer.Infrastructure.UI.Search
{
    /// <summary>
    /// Event args for search event on SearchTextBox.
    /// </summary>
    public class SearchEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Ignore delay set on binding of SearchText binding source.
        /// Set true in case we want to start search on enter press instead of binding delay to complete.
        /// </summary>
        public bool IgnoreBindingDelay { get; set; }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>
        /// The search text.
        /// </value>
        public string SearchText { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="routedEvent"><see cref="RoutedEvent"/>routed event</param>
        public SearchEventArgs(RoutedEvent routedEvent)
            : base(routedEvent)
        {
            SearchText = string.Empty;
        }
    }
}
