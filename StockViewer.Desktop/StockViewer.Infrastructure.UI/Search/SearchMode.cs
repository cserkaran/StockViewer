using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockViewer.Infrastructure.UI.Search
{
    /// <summary>
    /// The mode of the Search Text to search.
    /// </summary>
    public enum SearchMode
    {
        /// <summary>
        /// Apply search filter as and when user is typing
        /// </summary>
        Instant = 0,

        /// <summary>
        /// Delay the search filter till user finishes typing and invokes another action 
        /// e.g enter click or click of some button.
        /// </summary>
        Delayed = 1
    }
}
