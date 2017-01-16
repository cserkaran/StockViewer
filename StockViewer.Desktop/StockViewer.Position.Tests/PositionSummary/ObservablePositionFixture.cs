using Moq;
using Prism.Events;
using StockViewer.Infrastructure.DomainEvents;
using StockViewer.Infrastructure.Models;
using StockViewer.Position.Contracts;
using StockViewer.Position.PositionSummary;
using StockViewer.Position.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xunit;

namespace StockViewer.Position.Tests.PositionSummary
{
    public class ObservablePositionFixture
    {
        [Fact]
        public void GeneratesModelFromPositionAndMarketFeeds()
        {
            var accountPositionService = new MockStockPositionService();
            var marketFeedService = new MockMarketFeedService();
            accountPositionService.AddPosition(new StockPosition("FUND0", 300m, 1000,23,100));
            accountPositionService.AddPosition(new StockPosition("FUND1", 200m, 100,19,53));
            marketFeedService.SetPrice("FUND0", 30.00m);
            marketFeedService.SetPrice("FUND1", 20.00m);

            ObservableStockPosition position = new ObservableStockPosition(accountPositionService, marketFeedService, CreateEventAggregator());
            position.LoadData().Wait();
            var list = position.Items.OfType<StockPositionSummaryItem>();
            Assert.Equal(30.00m, list.First(x => x.TickerSymbol == "FUND0").CurrentPrice);
            Assert.Equal(1000, list.First(x => x.TickerSymbol == "FUND0").Shares);
            Assert.Equal(20.00m, list.First(x => x.TickerSymbol == "FUND1").CurrentPrice);
            Assert.Equal(100, list.First(x => x.TickerSymbol == "FUND1").Shares);
        }

        [Fact]
        public void ShouldUpdateDataWithMarketUpdates()
        {
            var accountPositionService = new MockStockPositionService();
            var marketFeedService = new MockMarketFeedService();
            var eventAggregator = CreateEventAggregator();
            var marketPricesUpdatedEvent = eventAggregator.GetEvent<MarketPriceUpdatedEvent>() as MockMarketPricesUpdatedEvent;
            marketFeedService.SetPrice("FUND0", 30.00m);
            accountPositionService.AddPosition("FUND0", 25.00m, 1000,21,78);
            marketFeedService.SetPrice("FUND1", 20.00m);
            accountPositionService.AddPosition("FUND1", 15.00m, 100,12,54);
            ObservableStockPosition position = new ObservableStockPosition(accountPositionService, marketFeedService, eventAggregator);
            position.LoadData().Wait();
            var updatedPriceList = new Dictionary<string, decimal> { { "FUND0", 50.00m } };
            Assert.NotNull(marketPricesUpdatedEvent.SubscribeArgumentAction);
            Assert.Equal(ThreadOption.UIThread, marketPricesUpdatedEvent.SubscribeArgumentThreadOption);

            marketPricesUpdatedEvent.SubscribeArgumentAction(updatedPriceList);
            var list = position.Items.OfType<StockPositionSummaryItem>();

            Assert.Equal(50.00m, list.First(x => x.TickerSymbol == "FUND0").CurrentPrice);
        }

        [Fact]
        public void MarketUpdatesPositionUpdatesButCollectionDoesNot()
        {
            var accountPositionService = new MockStockPositionService();
            var marketFeedService = new MockMarketFeedService();
            var eventAggregator = CreateEventAggregator();
            var marketPricesUpdatedEvent = eventAggregator.GetEvent<MarketPriceUpdatedEvent>() as MockMarketPricesUpdatedEvent;
            marketFeedService.SetPrice("FUND1", 20.00m);
            accountPositionService.AddPosition("FUND1", 15.00m, 100,14,31);

            ObservableStockPosition position = new ObservableStockPosition(accountPositionService, marketFeedService, eventAggregator);
            position.LoadData().Wait();
            bool itemsCollectionUpdated = false;

            var collection = ((ICollectionView)position.Items);

            collection.CollectionChanged += delegate
            {
                itemsCollectionUpdated = true;
            };

            bool itemUpdated = false;
            var list = position.Items.OfType<StockPositionSummaryItem>();
            list.First(p => p.TickerSymbol == "FUND1").PropertyChanged += delegate
            {
                itemUpdated = true;
            };

            marketPricesUpdatedEvent.SubscribeArgumentAction(new Dictionary<string, decimal> { { "FUND1", 50m } });

            Assert.False(itemsCollectionUpdated);
            Assert.True(itemUpdated);
        }

        [Fact]
        public void StockPositionModificationUpdatesPM()
        {
            var accountPositionService = new MockStockPositionService();
            var marketFeedService = new MockMarketFeedService();
            marketFeedService.SetPrice("FUND0", 20.00m);
            accountPositionService.AddPosition("FUND0", 150.00m, 100,110,210);
            ObservableStockPosition position = new ObservableStockPosition(accountPositionService, marketFeedService, CreateEventAggregator());
            position.LoadData().Wait();
            bool itemUpdated = false;
            var list = position.Items.OfType<StockPositionSummaryItem>();
            list.First(p => p.TickerSymbol == "FUND0").PropertyChanged += delegate
            {
                itemUpdated = true;
            };

            StockPosition accountPosition = accountPositionService.GetStockPositions().First<StockPosition>(p => p.TickerSymbol == "FUND0");
            accountPosition.Shares += 11;
            accountPosition.CostBasis = 25.00m;

            Assert.True(itemUpdated);
         
            Assert.Equal(111, list.First(p => p.TickerSymbol == "FUND0").Shares);
            Assert.Equal(25.00m, list.First(p => p.TickerSymbol == "FUND0").CostBasis);
        }

        private static IEventAggregator CreateEventAggregator()
        {
            var eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<MarketPriceUpdatedEvent>()).Returns(new MockMarketPricesUpdatedEvent());

            return eventAggregator.Object;
        }

        private class MockMarketPricesUpdatedEvent : MarketPriceUpdatedEvent
        {
            public Action<IDictionary<string, decimal>> SubscribeArgumentAction;
            public Predicate<IDictionary<string, decimal>> SubscribeArgumentFilter;
            public ThreadOption SubscribeArgumentThreadOption;

            public override SubscriptionToken Subscribe(Action<IDictionary<string, decimal>> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<IDictionary<string, decimal>> filter)
            {
                SubscribeArgumentAction = action;
                SubscribeArgumentFilter = filter;
                SubscribeArgumentThreadOption = threadOption;
                return null;
            }
        }
    }
}
