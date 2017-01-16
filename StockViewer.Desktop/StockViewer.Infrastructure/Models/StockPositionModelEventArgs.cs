using System;

namespace StockViewer.Infrastructure.Models
{
    /// <summary>
    /// Event args to notify the updated stock position data.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class StockPositionModelEventArgs : EventArgs
    {
        #region Properties 

        /// <summary>
        /// Initializes a new instance of the <see cref="StockPositionModelEventArgs"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public StockPositionModelEventArgs(StockPosition position)
        {
            StockPosition = position;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Gets or sets the stock position.
        /// </summary>
        /// <value>
        /// The stock position.
        /// </value>
        public StockPosition StockPosition { get; set; }

        #endregion
    }
}
