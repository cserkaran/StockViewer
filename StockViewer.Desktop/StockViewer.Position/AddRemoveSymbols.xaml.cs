using StockViewer.Infrastructure.UI;
using System.Windows;
using System;
using StockViewer.Infrastructure;

namespace StockViewer.Position
{
    /// <summary>
    /// Interaction logic for AddSymbols.xaml
    /// </summary>
    public partial class AddSymbols : BaseWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddSymbols"/> class.
        /// </summary>
        public AddSymbols()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
        }

        /// <summary>
        /// Raises the <see cref="E:SourceInitialized" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.HideMinimizeButton();
        }

        /// <summary>
        /// Called when [cancel button click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Oks the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
