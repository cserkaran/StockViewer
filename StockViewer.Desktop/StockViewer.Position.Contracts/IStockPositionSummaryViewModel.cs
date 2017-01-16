using System;
using System.Windows;
using Prism.Commands;

namespace StockViewer.Position.Contracts
{
    /// <summary>
    /// StockPosition summary details view model.
    /// </summary>
    public interface IStockPositionSummaryViewModel
    {
        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        IObservableStockPosition Position { get; }
    }
}
