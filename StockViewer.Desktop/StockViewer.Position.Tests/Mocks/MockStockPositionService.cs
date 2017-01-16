using StockViewer.Infrastructure.Models;
using StockViewer.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace StockViewer.Position.Tests.Mocks
{
    /// <summary>
    /// Mock stub implementation of IStockPositionService
    /// </summary>
    /// <seealso cref="IStockPositionService" />
    class MockStockPositionService : IStockPositionService
    {
        #region Fields

        /// <summary>
        /// The position data
        /// </summary>
        List<StockPosition> positionData = new List<StockPosition>();

        #endregion

        #region Add Positions

        /// <summary>
        /// Adds the position.
        /// </summary>
        /// <param name="ticker">The ticker.</param>
        /// <param name="costBasis">The cost basis.</param>
        /// <param name="shares">The shares.</param>
        /// <param name="wl52">The WL52.</param>
        /// <param name="wh52">The WH52.</param>
        public void AddPosition(string ticker, decimal costBasis, long shares,decimal wl52,decimal wh52)
        {
            AddPosition(new StockPosition(ticker, costBasis, shares,wl52,wh52));
        }

        /// <summary>
        /// Adds the position.
        /// </summary>
        /// <param name="position">The position.</param>
        public void AddPosition(StockPosition position)
        {
            (position as INotifyPropertyChanged).PropertyChanged += MockStockPositionService_PropertyChanged;
            positionData.Add(position);

            //Notify that collection has changed
            Updated(this, new StockPositionModelEventArgs(position));
        }

        #endregion

        #region Stock Position Updated

        /// <summary>
        /// Handles the PropertyChanged event of the MockStockPositionService control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void MockStockPositionService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Updated(this, new StockPositionModelEventArgs((StockPosition)sender));
        }

        /// <summary>
        /// Occurs when stock position is updated.
        /// </summary>
        public event EventHandler<StockPositionModelEventArgs> Updated = delegate { };

        #endregion

        #region  Get Stocks

        /// <summary>
        /// Gets the stock positions.
        /// </summary>
        /// <returns>
        /// List of stock positions
        /// </returns>
        public IList<StockPosition> GetStockPositions()
        {
            return positionData;
        }

        #endregion
    }
}
