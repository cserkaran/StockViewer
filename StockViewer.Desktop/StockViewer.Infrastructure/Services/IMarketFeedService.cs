namespace StockViewer.Infrastructure.Services
{
    /// <summary>
    /// Service to retrieve market feed data.
    /// </summary>
    public interface IMarketFeedService
    {
        /// <summary>
        /// Gets or sets the refresh interval.
        /// </summary>
        /// <value>
        /// The refresh interval.
        /// </value>
        int RefreshInterval { get; set; }

        /// <summary>
        /// Gets the price.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol.</param>
        /// <returns></returns>
        decimal GetPrice(string tickerSymbol);

        /// <summary>
        /// Gets the volume.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol.</param>
        /// <returns></returns>
        long GetVolume(string tickerSymbol);

        /// <summary>
        /// Symbols the exists.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol.</param>
        /// <returns></returns>
        bool SymbolExists(string tickerSymbol);
    }
}
