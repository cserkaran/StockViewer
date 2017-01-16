using Moq;
using Prism.Events;
using StockViewer.Infrastructure.DomainEvents;
using StockViewer.Position.Contracts;
using StockViewer.Position.PositionSummary;
using System.Threading.Tasks;
using Xunit;
using System.Collections;

namespace StockViewer.Position.Tests.PositionSummary
{

    public class PositionSummaryViewModelFixture : IClassFixture<TickerSymbolSelectedEventFixture>
    {
        TickerSymbolSelectedEventFixture fixture;

        public PositionSummaryViewModelFixture(TickerSymbolSelectedEventFixture fixture)
        {
            this.fixture = fixture;
        }

       [Fact]
        public void ShouldTriggerPropertyChangedEventOnCurrentPositionSummaryItemChange()
        {
            StockPositionSummaryViewModel viewModel = CreateViewModel();
            string changedPropertyName = string.Empty;

            viewModel.PropertyChanged += (o, e) =>
            {
                changedPropertyName = e.PropertyName;
            };

            viewModel.CurrentPositionSummaryItem = new StockPositionSummaryItem("NewTickerSymbol", 0, 0, 0,0,0);

            Assert.Equal("CurrentPositionSummaryItem", changedPropertyName);
        }

        [Fact]
        public void TickerSymbolSelectedPublishesEvent()
        {
            var tickerSymbolSelectedEvent = new MockTickerSymbolSelectedEvent();
            this.fixture.EventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(tickerSymbolSelectedEvent);
            var viewModel = CreateViewModel();

            viewModel.CurrentPositionSummaryItem = new StockPositionSummaryItem("FUND0", 0, 0, 0,0,0);

            Assert.True(tickerSymbolSelectedEvent.PublishCalled);
            Assert.Equal("FUND0", tickerSymbolSelectedEvent.PublishArgumentPayload);
        }

        

        private StockPositionSummaryViewModel CreateViewModel()
        {
            return new StockPositionSummaryViewModel(this.fixture.EventAggregator.Object, this.fixture.ObservablePosition);
        }

        [Fact]
        public void CurrentItemNullDoesNotThrow()
        {
            var model = CreateViewModel();

            model.CurrentPositionSummaryItem = null;
        }
    }

    internal class MockTickerSymbolSelectedEvent : TickerSymbolSelectedEvent
    {
        public bool PublishCalled;
        public string PublishArgumentPayload;


        public override void Publish(string payload)
        {
            PublishCalled = true;
            PublishArgumentPayload = payload;
        }
    }

    public class MockObservablePosition : IObservableStockPosition
    {
        public bool IsBusy { get; set; }
       

        public IEnumerable Items { get; }
       

        public Task LoadData()
        {
            return Task.Factory.StartNew(() =>
            {
                // Do nothing...
            });
        }
    }

    public class TickerSymbolSelectedEventFixture
    {
        public Mock<IEventAggregator> EventAggregator { get; private set; }


        public MockObservablePosition ObservablePosition { get; private set; }

        public TickerSymbolSelectedEventFixture()
        {
            ObservablePosition = new MockObservablePosition();
            EventAggregator = new Mock<IEventAggregator>();
            EventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(
                new MockTickerSymbolSelectedEvent());
        }
    }
}
