using StockViewer.Position.Contracts;
using System;
using System.ComponentModel;
using Xunit;

namespace StockViewer.Position.Tests.PositionSummary
{
    /// <summary>
    /// Summary description for PositionSummaryItemFixture
    /// </summary>
    public class PositionSummaryItemFixture
    {
        [Fact]
        public void ChangingCurrentPriceFiresPropertyChangeNotificationEvent()
        {
            StockPositionSummaryItem positionSummary = new StockPositionSummaryItem("FUND0", 49.99M, 50, 52.99M,10,100);

            bool currentPriceChanged = false;
            positionSummary.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "CurrentPrice")
                    currentPriceChanged = true;
            };

            positionSummary.CurrentPrice -= 5;

            Assert.True(currentPriceChanged);
        }

        [Fact]
        public void ChangingCostBasisFiresPropertyChangeNotificationEvent()
        {
            StockPositionSummaryItem positionSummary = new StockPositionSummaryItem("FUND0", 49.99M, 50, 52.99M,10,100);

            bool costBasisPropertyChanged = false;
            positionSummary.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "CostBasis")
                    costBasisPropertyChanged = true;
            };

            positionSummary.CostBasis -= 5;

            Assert.True(costBasisPropertyChanged);
        }

        [Fact]
        public void ChangingSharesFiresPropertyChangeNotificationEvent()
        {
            StockPositionSummaryItem positionSummary = new StockPositionSummaryItem("FUND0", 49.99M, 50, 52.99M,10,100);

            bool sharesPropertyChanged = false;
            positionSummary.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Shares")
                    sharesPropertyChanged = true;
            };

            positionSummary.Shares -= 5;

            Assert.True(sharesPropertyChanged);
        }

        [Fact]
        public void ChangingSymbolPropertyChangeNotificationEvent()
        {
            StockPositionSummaryItem positionSummary = new StockPositionSummaryItem("AAAA", 49.99M, 50, 52.99M,10,100);

            bool propertyChanged = false;
            string lastPropertyChanged = string.Empty;

            positionSummary.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                propertyChanged = true;
                lastPropertyChanged = e.PropertyName;
            };

            positionSummary.TickerSymbol = "XXXX";

            Assert.True(propertyChanged);
            Assert.Equal("TickerSymbol", lastPropertyChanged);
        }

        [Fact]
        public void PositionSummaryCalculatesCurrentMarketValue()
        {
            decimal lastPrice = 52.99M;
            long numShares = 50;

            StockPositionSummaryItem positionSummary = new StockPositionSummaryItem("AAAA", 49.99M, numShares, lastPrice,10,100);

            Assert.Equal(lastPrice * numShares, positionSummary.MarketValue);
        }

        [Fact]
        public void PositionSummaryCalculatesGainLossPercent()
        {
            decimal costBasis = 49.99M;
            decimal lastPrice = 52.99M;
            long numShares = 1000;

            StockPositionSummaryItem positionSummary = new StockPositionSummaryItem("AAAA", costBasis, numShares, lastPrice,10,100);

            Assert.Equal(105901.2002M, Math.Round(positionSummary.GainLossPercent, 4));
        }

    }
}
