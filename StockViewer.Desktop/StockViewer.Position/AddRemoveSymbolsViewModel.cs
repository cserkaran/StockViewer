using Prism.Commands;
using StockViewer.Position.Contracts;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using StockViewer.Infrastructure.UI.Search;

namespace StockViewer.Position
{
    /// <summary>
    /// view model to select and add stock symbols to monitor.
    /// </summary>
    internal class AddRemoveSymbolsViewModel
    {
        #region Fields 

        /// <summary>
        /// The backing Collection view source behind the bound items.
        /// </summary>
        private readonly ICollectionView _cvs;

        #endregion

        #region Properties 

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IEnumerable Items { get; }

        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
        /// <value>
        /// The selected items.
        /// </value>
        public IEnumerable SelectedItems { get; private set; }

        /// <summary>
        /// Gets or sets the items selected command.
        /// </summary>
        /// <value>
        /// The items selected command.
        /// </value>
        public DelegateCommand<object> ItemsSelectedCommand { get; set; }

        /// <summary>
        /// Gets the search view model.
        /// </summary>
        /// <value>
        /// The search view model.
        /// </value>
        public SearchItemsViewModel SearchViewModel { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRemoveSymbolsViewModel"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public AddRemoveSymbolsViewModel(IList<StockPositionSummaryItem> items)
        {
            _cvs = CollectionViewSource.GetDefaultView(items);
            SearchViewModel = new SearchItemsViewModel(_cvs);
            _cvs.Filter = FilterItems;
            Items = _cvs;
            SelectedItems = new List<StockPositionSummaryItem>();
            ItemsSelectedCommand = new DelegateCommand<object>(SetSelectedItems);
        }

        #endregion

        #region Selection 

        /// <summary>
        /// Sets the selected items.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void SetSelectedItems(object obj)
        {
            SelectedItems = (IEnumerable)obj; 
        }

        #endregion

        #region Filtering

        private bool FilterItems(object obj)
        {
            var stockPositionSummary = (StockPositionSummaryItem)obj;

            if (!string.IsNullOrWhiteSpace(SearchViewModel.CurrentSearchPattern)
                  && stockPositionSummary.TickerSymbol.
                  ToLowerInvariant().StartsWith(SearchViewModel.CurrentSearchPattern.ToLowerInvariant()))
            {
                return true;
            }

            else if (string.IsNullOrWhiteSpace(SearchViewModel.CurrentSearchPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }
}
