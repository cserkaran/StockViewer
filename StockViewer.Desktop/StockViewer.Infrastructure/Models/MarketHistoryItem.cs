using System;

namespace StockViewer.Infrastructure.Models
{
    /// <summary>
    /// The Stock market history data.
    /// </summary>
    public class MarketHistoryItem
    {
        /// <summary>
        /// Gets or sets the date and time when stock value changed.
        /// </summary>
        /// <value>
        /// The date time marker.
        /// </value>
        public DateTime DateTimeMarker { get; set; }

        /// <summary>
        /// Gets or sets the value of stock in the market on given date.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal Value { get; set; }
    }
}
