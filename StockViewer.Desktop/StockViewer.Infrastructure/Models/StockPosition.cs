using PropertyChanged;
using System.ComponentModel;

namespace StockViewer.Infrastructure.Models
{
    /// <summary>
    /// The Stock position data model.
    /// </summary>
    [ImplementPropertyChanged]
    public class StockPosition
    {  
        #region Properties

        /// <summary>
        /// Gets or sets the stock ticker symbol.
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
        /// Gets or sets the number of shares.
        /// </summary>
        /// <value>
        /// The shares.
        /// </value>
        public long Shares { get; set; }

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

        #endregion

        #region Constructors 

        /// <summary>
        /// Initializes a new instance of the <see cref="StockPosition"/> class.
        /// </summary>
        public StockPosition() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockPosition"/> class.
        /// </summary>
        /// <param name="tickerSymbol">The ticker symbol.</param>
        /// <param name="costBasis">The cost basis.</param>
        /// <param name="shares">The shares.</param>
        /// <param name="wl52">The stock 52 week low value.</param>
        /// <param name="wh52">The stock 52 week high value.</param>
        public StockPosition(string tickerSymbol, decimal costBasis, long shares,decimal wl52,decimal wh52)
        {
            TickerSymbol = tickerSymbol;
            CostBasis = costBasis;
            Shares = shares;
            Wl52 = wl52;
            Wh52 = wh52;
        }

        #endregion
    }
}
