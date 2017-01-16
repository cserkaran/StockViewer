using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using StockViewer.Infrastructure.Models;
using System.IO;
using System.Globalization;
using System.Xml.Linq;
using StockViewer.Infrastructure.Services;

namespace StockViewer.Position.Services
{
    /// <summary>
    /// StockPosition service implementation.
    /// </summary>
    /// <seealso cref="IStockPositionService" />
    [Export(typeof(IStockPositionService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class StockPositionService : IStockPositionService
    {
        #region Fields 

        /// <summary>
        /// The positions
        /// </summary>
        private IList<StockPosition> _positions = new List<StockPosition>();

        #endregion

        #region Events

        /// <summary>
        /// Occurs when stock position is updated.
        /// </summary>
        public event EventHandler<StockPositionModelEventArgs> Updated = delegate { };

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StockPositionService"/> class.
        /// </summary>
        public StockPositionService()
        {
            InitializePositions();
        }

        #endregion

        #region Stock Positions

        /// <summary>
        /// Gets the stock positions.
        /// </summary>
        /// <returns>
        /// List of stock positions
        /// </returns>
        public IList<StockPosition> GetStockPositions()
        {
            return _positions;
        }

        /// <summary>
        /// Initializes the positions.
        /// </summary>
        private void InitializePositions()
        {
            using (var sr = new StringReader(Resources.Resources.StockPositions))
            {
                XDocument document = XDocument.Load(sr);
                _positions = document.Descendants("StockPosition")
                    .Select(
                    x => new StockPosition(x.Element("TickerSymbol")?.Value,
                                             decimal.Parse(x.Element("CostBasis").Value, CultureInfo.InvariantCulture),
                                             long.Parse(x.Element("Shares")?.Value, CultureInfo.InvariantCulture),
                                             decimal.Parse(x.Element("WL52").Value, CultureInfo.InvariantCulture),
                                             decimal.Parse(x.Element("WH52").Value, CultureInfo.InvariantCulture))).ToList();
            }
        }

        #endregion
    }
}
