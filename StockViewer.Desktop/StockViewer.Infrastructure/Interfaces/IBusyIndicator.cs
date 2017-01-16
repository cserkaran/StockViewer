namespace StockViewer.Infrastructure.Interfaces
{
    /// <summary>
    /// Interface for items which can be busy.
    /// </summary>
    public interface IBusyIndicator
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        bool IsBusy { get; set; }
    }
}
