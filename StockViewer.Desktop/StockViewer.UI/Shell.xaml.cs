using Prism.Commands;
using StockViewer.Infrastructure.UI;
using System.ComponentModel.Composition;
using System.Windows;

namespace StockViewer.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export]
    public partial class Shell : BaseWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        public Shell()
        {
            InitializeComponent();

            SystemTaskbarIcon.DoubleClickCommand = new DelegateCommand(() =>
            {
                if (this.WindowState != WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }

                Activate();
            });
        }

        /// <summary>
        /// System task bar context menu close item click.
        /// Closes the application.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnMenuCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
