using Prism.Events;
using Prism.Mvvm;
using StockViewer.Infrastructure.DomainEvents;
using StockViewer.Position.Contracts;
using System.ComponentModel.Composition;

namespace StockViewer.Position.PositionSummary
{
    /// <summary>
    /// View Model for stock position summary details.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    /// <seealso cref="StockViewer.Position.Contracts.IStockPositionSummaryViewModel" />
    [Export(typeof(IStockPositionSummaryViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StockPositionSummaryViewModel : BindableBase, IStockPositionSummaryViewModel
    {
        #region Fields

        /// <summary>
        /// The current position summary item
        /// </summary>
        private StockPositionSummaryItem currentPositionSummaryItem;

        /// <summary>
        /// The event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public IObservableStockPosition Position { get; }

        /// <summary>
        /// Gets or sets the current position summary item.
        /// </summary>
        /// <value>
        /// The current position summary item.
        /// </value>
        public StockPositionSummaryItem CurrentPositionSummaryItem
        {
            get { return currentPositionSummaryItem; }
            set
            {
                if (SetProperty(ref currentPositionSummaryItem, value))
                {
                    if (currentPositionSummaryItem != null)
                    {
                        eventAggregator.GetEvent<TickerSymbolSelectedEvent>().Publish(
                            CurrentPositionSummaryItem.TickerSymbol);
                    }
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StockPositionSummaryViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="observablePosition">The observable position.</param>
        [ImportingConstructor]
        public StockPositionSummaryViewModel(IEventAggregator eventAggregator, IObservableStockPosition observablePosition)
        { 

            this.eventAggregator = eventAggregator;
            Position = observablePosition;
            Position.LoadData();
            CurrentPositionSummaryItem = new StockPositionSummaryItem("FAKEINDEX", 0, 0, 0,0,0);
        }

        #endregion
       
    }
}
