using StockViewer.Infrastructure.Services;
using System.Windows;

namespace StockViewer.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Raises the <see cref="E:Startup" /> event.
        /// </summary>
        /// <param name="e">The <see cref="StartupEventArgs"/> instance containing the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            StockViewerBootstrapper bootstrapper = new StockViewerBootstrapper();
            bootstrapper.Run();
        }
    }
}
