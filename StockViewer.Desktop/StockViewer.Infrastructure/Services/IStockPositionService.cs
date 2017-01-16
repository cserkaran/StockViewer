using StockViewer.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace StockViewer.Infrastructure.Services
{
    /// <summary>
    /// Service for managing stock positions.
    /// </summary>
    public interface IStockPositionService
    {
        #region Events 

        /// <summary>
        /// Occurs when stock position is updated.
        /// </summary>
        event EventHandler<StockPositionModelEventArgs> Updated;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the stock positions.
        /// </summary>
        /// <returns>List of stock positions</returns>
        IList<StockPosition> GetStockPositions();

        #endregion
    }
}
