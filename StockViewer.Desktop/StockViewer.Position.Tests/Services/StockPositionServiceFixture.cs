using StockViewer.Position.Services;
using Xunit;

namespace StockViewer.Position.Tests.Services
{
    public class StockPositionServiceFixture
    {
        [Fact]
        public void ShouldReturnDefaultPositions()
        {
            StockPositionService model = new StockPositionService();

            Assert.True(model.GetStockPositions().Count > 0, "No account positions returned");
        }
    }
}
