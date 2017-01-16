using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using Prism.Events;
using System;
using StockViewer.Infrastructure.Models;
using System.Linq;
using StockViewer.Infrastructure.DomainEvents;
using System.Collections.Generic;
using System.Windows;
using Prism.Commands;
using StockViewer.Position.Contracts;
using StockViewer.Infrastructure.Services;
using StockViewer.Infrastructure.UI.Search;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections;
using StockViewer.Infrastructure.Interfaces;
using PropertyChanged;
using System.Threading.Tasks;
using System.Threading;

namespace StockViewer.Position.PositionSummary
{
    /// <summary>
    ///  observe changes in stock position summary.
    /// </summary>
    /// <seealso cref="IObservableStockPosition" />
    [Export(typeof(IObservableStockPosition))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ImplementPropertyChanged]
    public class ObservableStockPosition : IObservableStockPosition
    {
        #region Constants

        /// <summary>
        /// The maximum value after which to stop refresh of the stock price.
        /// </summary>
        public const int StopRefreshLimit = 10;

        #endregion

        #region Fields 

        /// <summary>
        /// The stock position service
        /// </summary>
        private readonly IStockPositionService _stockPositionService;

        /// <summary>
        /// The market feed service
        /// </summary>
        private readonly IMarketFeedService _marketFeedService;

        /// <summary>
        /// The items
        /// </summary>
        private readonly ObservableCollection<StockPositionSummaryItem> _items;

        /// <summary>
        /// The backing Collection view source behind the bound items.
        /// </summary>
        private  ICollectionView _cvs;

        /// <summary>
        /// The untracked items
        /// </summary>
        private readonly HashSet<StockPositionSummaryItem> _removedItems;

        /// <summary>
        /// The event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IEnumerable Items { get; private set; }
 
        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
        /// <value>
        /// The selected items.
        /// </value>
        public IEnumerable SelectedItems { get; set; }

        /// <summary>
        /// Gets the change refresh rate command.
        /// </summary>
        /// <value>
        /// The change refresh rate command.
        /// </value>
        public DelegateCommand<RoutedPropertyChangedEventArgs<double>> ChangeRefreshRateCommand { get; private set; }

        /// <summary>
        /// Gets the search command.
        /// </summary>
        /// <value>
        /// The search command.
        /// </value>
        public DelegateCommand<SearchEventArgs> SearchCommand { get; private set; }

        /// <summary>
        /// Gets the cancel search command.
        /// </summary>
        /// <value>
        /// The cancel search command.
        /// </value>
        public DelegateCommand<RoutedEventArgs> CancelSearchCommand { get; private set; }

        /// <summary>
        /// Gets the add symbols command.
        /// </summary>
        /// <value>
        /// The add symbols command.
        /// </value>
        public DelegateCommand<object> AddSymbolsCommand { get; private set; }

        /// <summary>
        /// Gets the remove symbols command.
        /// </summary>
        /// <value>
        /// The remove symbols command.
        /// </value>
        public DelegateCommand<object> RemoveSymbolsCommand { get; private set; }

        /// <summary>
        /// Gets the search view model.
        /// </summary>
        /// <value>
        /// The search view model.
        /// </value>
        public SearchItemsViewModel SearchViewModel { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsBusy { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableStockPosition"/> class.
        /// </summary>
        /// <param name="stockPositionService">The stock position service.</param>
        /// <param name="marketFeedService">The market feed service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <exception cref="System.ArgumentNullException">eventAggregator</exception>
        [ImportingConstructor]
        public ObservableStockPosition(IStockPositionService stockPositionService, IMarketFeedService marketFeedService,
            IEventAggregator eventAggregator)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException(nameof(eventAggregator));
            }

            this.eventAggregator = eventAggregator;

            // services
            _stockPositionService = stockPositionService;
            _marketFeedService = marketFeedService;

            // item source 
            _removedItems = new HashSet<StockPositionSummaryItem>();
            _items = new ObservableCollection<StockPositionSummaryItem>();
        }

        #endregion

        #region Market Price Update

        /// <summary>
        /// Market Price update domain event handler.
        /// </summary>
        /// <param name="tickerSymbolsPrice">The ticker symbols price.</param>
        /// <exception cref="System.ArgumentNullException">tickerSymbolsPrice</exception>
        private void MarketPricesUpdated(IDictionary<string, decimal> tickerSymbolsPrice)
        {
            if (tickerSymbolsPrice == null)
            {
                throw new ArgumentNullException(nameof(tickerSymbolsPrice));
            }

            foreach (StockPositionSummaryItem position in Items)
            {
                if (tickerSymbolsPrice.ContainsKey(position.TickerSymbol))
                {
                    position.CurrentPrice = tickerSymbolsPrice[position.TickerSymbol];
                }
            }
        }

        #endregion

        #region Stock Position Summary

        /// <summary>
        /// Populates the items.
        /// </summary>
        private void PopulateItems()
        {
            // simulate heavy data loading for 1 seconds
            Thread.Sleep(1000);
            foreach (StockPosition stockPosition in _stockPositionService.GetStockPositions())
            {
                var positionSummaryItem = new StockPositionSummaryItem
                (stockPosition.TickerSymbol, stockPosition.CostBasis, stockPosition.Shares,
                    _marketFeedService.GetPrice(stockPosition.TickerSymbol),stockPosition.Wl52,stockPosition.Wh52);
                _items.Add(positionSummaryItem);
            }
        }

        /// <summary>
        /// Called when [position summary items updated].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="StockPositionModelEventArgs"/> instance containing the event data.</param>
        private void OnPositionSummaryItemsUpdated(object sender, StockPositionModelEventArgs e)
        {
            if (e.StockPosition != null)
            {
                StockPositionSummaryItem positionSummaryItem = _items.First(p => p.TickerSymbol == e.StockPosition.TickerSymbol);

                if (positionSummaryItem != null)
                {
                    positionSummaryItem.Shares = e.StockPosition.Shares;
                    positionSummaryItem.CostBasis = e.StockPosition.CostBasis;
                }
            }
        }

        #endregion

        #region Update Stock Current Price Refresh Rate

        private void UpdateCurrentStockPriceRefreshRate(RoutedPropertyChangedEventArgs<double> args)
        { 
            var newVal = (int)args.NewValue;
            if (newVal >= StopRefreshLimit)
            {
                newVal = 0;
            }
            _marketFeedService.RefreshInterval = newVal;
        }

        #endregion

        #region Symbol Filtering

        /// <summary>
        /// Filters the items.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        private bool FilterItems(object obj)
        {
            var stockPositionSummary = (StockPositionSummaryItem)obj;

            if (_removedItems.Contains(stockPositionSummary))
            {
                return false;
            }


            if (!string.IsNullOrWhiteSpace(SearchViewModel.CurrentSearchPattern)
                && stockPositionSummary.TickerSymbol.
                ToLowerInvariant().StartsWith(SearchViewModel.CurrentSearchPattern.ToLowerInvariant()))
            {
                return true;
            }

            else if(string.IsNullOrWhiteSpace(SearchViewModel.CurrentSearchPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Add/Remove Symbols

        /// <summary>
        /// Determines whether this instance can remove symbols
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can remove symbols] the specified argument; otherwise, <c>false</c>.
        /// </returns>
        private bool CanRemoveSymbols(object arg)
        {
            return true;
        }

        /// <summary>
        /// Executes the remove symbols command.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void ExecuteRemoveSymbolsCommand(object obj)
        {
            var selectedItems = (IEnumerable)obj;
            foreach(StockPositionSummaryItem selectedItem in selectedItems)
            {
                _removedItems.Add(selectedItem);
            }

            _cvs.Refresh();

        }

        /// <summary>
        /// Executes the add symbols command.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void ExecuteAddSymbolsCommand(object obj)
        {
            var addSymbolsViewModel = new AddRemoveSymbolsViewModel(this._removedItems.ToList<StockPositionSummaryItem>());
            IWindow addSymbolsWindow = new AddSymbols { DataContext = addSymbolsViewModel };
            if (ClientContext.Instance.Host.MessageService.ShowDialog(addSymbolsWindow))
            {
                foreach (StockPositionSummaryItem selectedItem in addSymbolsViewModel.SelectedItems)
                {
                    // item was removed.
                    _removedItems.Remove(selectedItem);
                }

                // Refresh the collection view.
                _cvs.Refresh();
            }
        }

        #endregion

        #region Data Loading

        /// <summary>
        /// Loads the data.
        /// </summary>
        public Task LoadData()
        {
          return Task.Factory.StartNew(() =>
            {
                IsBusy = true;
                PopulateItems();
            }).ContinueWith(result =>
            {
                _cvs = CollectionViewSource.GetDefaultView(_items);
                SearchViewModel = new SearchItemsViewModel(_cvs);
                _cvs.Filter = FilterItems;
                Items = _cvs;
                // events
                eventAggregator.GetEvent<MarketPriceUpdatedEvent>().Subscribe(MarketPricesUpdated, ThreadOption.UIThread);
                _stockPositionService.Updated += OnPositionSummaryItemsUpdated;

                // command initialization.
                ChangeRefreshRateCommand = new DelegateCommand<RoutedPropertyChangedEventArgs<double>>
                    (UpdateCurrentStockPriceRefreshRate);
                AddSymbolsCommand = new DelegateCommand<object>(ExecuteAddSymbolsCommand);
                RemoveSymbolsCommand = new DelegateCommand<object>(ExecuteRemoveSymbolsCommand, CanRemoveSymbols);
                IsBusy = false;
            });
        }

        #endregion
    }
}
