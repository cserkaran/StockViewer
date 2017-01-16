using StockViewer.Infrastructure;
using StockViewer.Infrastructure.MefAttributes;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using StockViewer.Position.Contracts;

namespace StockViewer.Position.PositionSummary
{
    /// <summary>
    /// Interaction logic for StockPositionSummaryView.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.StockSummaryRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class StockPositionSummaryView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockPositionSummaryView"/> class.
        /// </summary>
        public StockPositionSummaryView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [Import]
        public IStockPositionSummaryViewModel Model
        {
            get
            {
                return DataContext as IStockPositionSummaryViewModel;
            }
            set
            {
                DataContext = value;
            }
        }
    }
}
