using StockViewer.Infrastructure.Services;
using System.Collections.Generic;

namespace StockViewer.Position.Tests.Mocks
{
    class MockMarketFeedService : IMarketFeedService
    {
        #region Fields

        /// <summary>
        /// The feed data
        /// </summary>
        Dictionary<string, decimal> _priceList = new Dictionary<string, decimal>();

        /// <summary>
        /// The volume list
        /// </summary>
        private readonly Dictionary<string, long> _volumeList = new Dictionary<string, long>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the refresh interval.
        /// </summary>
        /// <value>
        /// The refresh interval.
        /// </value>
        public int RefreshInterval { get; set; }

        #endregion

        #region Price change

        /// <summary>
        /// Sets the price.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol.</param>
        /// <param name="price">The price.</param>
        internal void SetPrice(string tickerSymbol, decimal price)
        {
            _priceList.Add(tickerSymbol, price);
        }

        /// <summary>
        /// Updates the price.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol.</param>
        /// <param name="newPrice">The new price.</param>
        /// <param name="volume">The volume.</param>
        internal void UpdatePrice(string tickerSymbol, decimal newPrice, long volume)
        {
            _priceList[tickerSymbol] = newPrice;
        }

        #endregion

        #region IMarketFeedService Members

        /// <summary>
        /// Gets the price.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol.</param>
        /// <returns></returns>
        public decimal GetPrice(string tickerSymbol)
        {
            if (_priceList.ContainsKey(tickerSymbol))
                return _priceList[tickerSymbol];
            return 0m;
        }

        /// <summary>
        /// Gets the volume.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public long GetVolume(string tickerSymbol)
        {
            if (_volumeList.ContainsKey(tickerSymbol))
                return _volumeList[tickerSymbol];
            return 0;
        }

        #endregion

        #region Symbol dictionary check.

        /// <summary>
        /// Symbols the exists.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool SymbolExists(string tickerSymbol)
        {
           return _priceList.ContainsKey(tickerSymbol);
        }

        #endregion
    }
}
