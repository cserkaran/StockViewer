using StockViewer.Infrastructure.Interfaces;
using System.Collections;
using System.Threading.Tasks;

namespace StockViewer.Position.Contracts
{
    /// <summary>
    /// Interface to observe changes in stock position summary.
    /// </summary>
    public interface IObservableStockPosition : IBusyIndicator
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        IEnumerable Items { get; }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <returns></returns>
        Task LoadData();
    }
}
