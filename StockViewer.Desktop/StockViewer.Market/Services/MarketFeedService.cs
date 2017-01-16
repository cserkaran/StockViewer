using Prism.Events;
using StockViewer.Infrastructure.DomainEvents;
using StockViewer.Infrastructure.Services;
using StockViewer.Market.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace StockViewer.Market.Services
{
    /// <summary>
    /// <see cref="IMarketFeedService"/> implementation.
    /// </summary>
    /// <seealso cref="IMarketFeedService" />
    /// <seealso cref="IDisposable" />
    [Export(typeof(IMarketFeedService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MarketFeedService : IMarketFeedService, IDisposable
    {
        #region Fields 

        /// <summary>
        /// The price list
        /// </summary>
        private readonly Dictionary<string, decimal> _priceList = new Dictionary<string, decimal>();

        /// <summary>
        /// The volume list
        /// </summary>
        private readonly Dictionary<string, long> _volumeList = new Dictionary<string, long>();

        /// <summary>
        /// The random generator
        /// </summary>
        static readonly Random randomGenerator = new Random(unchecked((int)DateTime.Now.Ticks));

        /// <summary>
        /// The timer
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// The refresh interval
        /// </summary>
        private int _refreshInterval = 10;

        /// <summary>
        /// The lock object
        /// </summary>
        private readonly object _lockObject = new object();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the event aggregator.
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        private IEventAggregator EventAggregator { get; }

        /// <summary>
        /// Gets or sets the refresh interval.
        /// </summary>
        /// <value>
        /// The refresh interval.
        /// </value>
        public int RefreshInterval
        {
            get { return _refreshInterval; }
            set
            {

                _refreshInterval = CalculateRefreshIntervalMillisecondsFromSeconds(value);
                _timer.Change(_refreshInterval, _refreshInterval);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketFeedService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        [ImportingConstructor]
        public MarketFeedService(IEventAggregator eventAggregator)
            : this(XDocument.Parse(Resource.Market), eventAggregator)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketFeedService"/> class.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <exception cref="System.ArgumentNullException">document</exception>
        protected MarketFeedService(XDocument document, IEventAggregator eventAggregator)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }

            EventAggregator = eventAggregator;
            _timer = new Timer(TimerTick);

            var marketItemsElement = document.Element("MarketItems");
            var refreshRateAttribute = marketItemsElement.Attribute("RefreshRate");
            if (refreshRateAttribute != null)
            {
                RefreshInterval = int.Parse(refreshRateAttribute.Value, CultureInfo.InvariantCulture);
            }

            var itemElements = marketItemsElement.Elements("MarketItem");
            foreach (XElement item in itemElements)
            {
                string tickerSymbol = item.Attribute("TickerSymbol").Value;
                decimal lastPrice = decimal.Parse(item.Attribute("LastPrice").Value, NumberStyles.Float, CultureInfo.InvariantCulture);
                long volume = Convert.ToInt64(item.Attribute("Volume").Value, CultureInfo.InvariantCulture);
                _priceList.Add(tickerSymbol, lastPrice);
                _volumeList.Add(tickerSymbol, volume);
            }
        }

        #endregion

        #region Timer 

        /// <summary>
        /// Callback for Timer
        /// </summary>
        /// <param name="state"></param>
        private void TimerTick(object state)
        {
            UpdatePrices();
        }

        /// <summary>
        /// Calculates the refresh interval milliseconds from seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <returns></returns>
        private static int CalculateRefreshIntervalMillisecondsFromSeconds(int seconds)
        {
            return seconds * 1000;
        }

        #endregion

        #region IMarketFeedService Implemenation

        /// <summary>
        /// Gets the price.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">tickerSymbol</exception>
        public decimal GetPrice(string tickerSymbol)
        {
            if (!SymbolExists(tickerSymbol))
                throw new ArgumentException(Resource.MarketFeedTickerSymbolNotFoundException, "tickerSymbol");

            return _priceList[tickerSymbol];
        }

        /// <summary>
        /// Gets the volume.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol.</param>
        /// <returns></returns>
        public long GetVolume(string tickerSymbol)
        {
            return _volumeList[tickerSymbol];
        }

        /// <summary>
        /// Symbols the exists.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol.</param>
        /// <returns></returns>
        public bool SymbolExists(string tickerSymbol)
        {
            return _priceList.ContainsKey(tickerSymbol);
        }

        #endregion

        #region Price Updation

        /// <summary>
        /// Updates the price.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol.</param>
        /// <param name="newPrice">The new price.</param>
        /// <param name="newVolume">The new volume.</param>
        protected void UpdatePrice(string tickerSymbol, decimal newPrice, long newVolume)
        {
            lock (_lockObject)
            {
                _priceList[tickerSymbol] = newPrice;
                _volumeList[tickerSymbol] = newVolume;
            }
            OnMarketPriceUpdated();
        }

        /// <summary>
        /// Updates the prices.
        /// </summary>
        protected void UpdatePrices()
        {
            lock (_lockObject)
            {
                foreach (string symbol in _priceList.Keys.ToArray())
                {
                    decimal newValue = _priceList[symbol];
                    newValue += Convert.ToDecimal(randomGenerator.NextDouble() * 10f) - 5m;
                    _priceList[symbol] = newValue > 0 ? newValue : 0.1m;
                }
            }
            OnMarketPriceUpdated();
        }

        /// <summary>
        /// Called when market prices updated.
        /// </summary>
        private void OnMarketPriceUpdated()
        {
            Dictionary<string, decimal> clonedPriceList = null;
            lock (_lockObject)
            {
                clonedPriceList = new Dictionary<string, decimal>(_priceList);
            }
            EventAggregator.GetEvent<MarketPriceUpdatedEvent>().Publish(clonedPriceList);
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_timer != null)
                _timer.Dispose();
            _timer = null;
        }

        /// <summary>
        /// Use C# destructor syntax for finalization code.
        /// Finalizes an instance of the <see cref="MarketFeedService"/> class.
        /// </summary>
        ~MarketFeedService()
        {
            Dispose(false);
        }

        #endregion
    }
}
