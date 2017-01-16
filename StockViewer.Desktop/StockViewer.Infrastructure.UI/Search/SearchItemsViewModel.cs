using Prism.Commands;
using System.ComponentModel;
using System.Windows;

namespace StockViewer.Infrastructure.UI.Search
{
    /// <summary>
    /// View Model for refreshing a CollectionView Source when 
    /// searching is done or cancelled.
    /// </summary>
    public class SearchItemsViewModel
    {
        #region Fields 

        /// <summary>
        /// The Collection view source.
        /// </summary>
        private ICollectionView _cvs;

        /// <summary>
        /// The current search pattern
        /// </summary>
        private string _currentSearchPattern;

        #endregion

        #region Properties

        /// <summary>
        /// The current search pattern
        /// </summary>
        public string CurrentSearchPattern
        {
            get
            {
                return _currentSearchPattern;
            }
            set
            {
                if(_currentSearchPattern != value)
                {
                    _currentSearchPattern = value;
                    ExecuteSearchCommand();
                }
            }
        }
 
        /// <summary>
        /// Gets the cancel search command.
        /// </summary>
        /// <value>
        /// The cancel search command.
        /// </value>
        public DelegateCommand<RoutedEventArgs> CancelSearchCommand { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchItemsViewModel"/> class.
        /// </summary>
        /// <param name="cvs">The CVS.</param>
        public SearchItemsViewModel(ICollectionView cvs)
        {
            _cvs = cvs;
            CancelSearchCommand = new DelegateCommand<RoutedEventArgs>(ExecuteCancelSearchCommand);
        }

        #endregion

        #region Search

        /// <summary>
        /// Executes the search command.
        /// </summary>
        private void ExecuteSearchCommand()
        {
            _cvs.Refresh();
        }

        /// <summary>
        /// Executes the cancel search command.
        /// </summary>
        /// <param name="obj">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ExecuteCancelSearchCommand(RoutedEventArgs obj)
        {
            CurrentSearchPattern = string.Empty;
            _cvs.Refresh();
        }

        #endregion
    }
}
