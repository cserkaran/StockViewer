using Prism.Mvvm;
using PropertyChanged;

namespace StockViewer.Position.Contracts
{
    /// <summary>
    /// Summary for stock position item.
    /// </summary>
    /// <seealso cref="BindableBase" />
    [ImplementPropertyChanged]
    public class StockPositionSummaryItem : BindableBase
    {
        #region Fields 

        /// <summary>
        /// The current price
        /// </summary>
        private decimal _currentPrice;

        #endregion

        #region Properties 

        /// <summary>
        /// Gets or sets the ticker symbol.
        /// </summary>
        /// <value>
        /// The ticker symbol.
        /// </value>
        public string TickerSymbol { get; set; }

        /// <summary>
        /// Gets or sets the cost basis.
        /// </summary>
        /// <value>
        /// The cost basis.
        /// </value>
        public decimal CostBasis { get; set; }

        /// <summary>
        /// Gets or sets the shares.
        /// </summary>
        /// <value>
        /// The shares.
        /// </value>
        public long Shares { get; set; }

        /// <summary>
        /// Gets or sets the current price.
        /// </summary>
        /// <value>
        /// The current price.
        /// </value>
        public decimal CurrentPrice
        {
            get
            {
                return _currentPrice;
            }
            set
            {
                if (SetProperty(ref _currentPrice, value))
                {
                    this.OnPropertyChanged(() => this.MarketValue);
                    this.OnPropertyChanged(() => this.GainLossPercent);
                }
            }
        }

        /// <summary>
        /// Gets or sets the stock 52 week low value.
        /// </summary>
        /// <value>
        /// The WL52.
        /// </value>
        public decimal Wl52 { get; set; }

        /// <summary>
        /// Gets or sets the stock 52 week high value.
        /// </summary>
        /// <value>
        /// The WH52.
        /// </value>
        public decimal Wh52 { get; set; }

        /// <summary>
        /// Gets the market value.
        /// </summary>
        /// <value>
        /// The market value.
        /// </value>
        public decimal MarketValue => (Shares * _currentPrice);

        /// <summary>
        /// Gets the gain loss percent.
        /// </summary>
        /// <value>
        /// The gain loss percent.
        /// </value>
        public decimal GainLossPercent => ((CurrentPrice * Shares - CostBasis) * 100 / CostBasis);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StockPositionSummaryItem"/> class.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol.</param>
        /// <param name="costBasis">The cost basis.</param>
        /// <param name="shares">The shares.</param>
        /// <param name="currentPrice">The current price.</param>
        /// <param name="wl52">The stock 52 week low value.</param>
        /// <param name="wh52">The stock 52 week high value.</param>
        public StockPositionSummaryItem(string tickerSymbol, decimal costBasis, long shares, decimal currentPrice,decimal wl52, decimal wh52)
        {
            TickerSymbol = tickerSymbol;
            CostBasis = costBasis;
            Shares = shares;
            CurrentPrice = currentPrice;
            Wl52 = wl52;
            Wh52 = wh52;
        }

        #endregion
    }
}
