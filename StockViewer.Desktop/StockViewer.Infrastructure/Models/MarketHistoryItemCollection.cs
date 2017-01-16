using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StockViewer.Infrastructure.Models
{
    /// <summary>
    /// Collection of <see cref="MarketHistoryItem"/> collections.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{StockViewer.Infrastructure.Models.MarketHistoryItem}" />
    public class MarketHistoryItemCollection : ObservableCollection<MarketHistoryItem>
    {
        #region Constructors 

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketHistoryItemCollection"/> class.
        /// </summary>
        public MarketHistoryItemCollection()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketHistoryItemCollection"/> class.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <exception cref="System.ArgumentNullException">list</exception>
        public MarketHistoryItemCollection(IEnumerable<MarketHistoryItem> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            foreach (MarketHistoryItem marketHistoryItem in list)
            {
                Add(marketHistoryItem);
            }
        }

        #endregion
    }
}
