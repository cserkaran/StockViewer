using Prism.Mef;
using Prism.Regions;
using StockViewer.Infrastructure.Behaviors;
using StockViewer.Infrastructure.Services;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace StockViewer.UI
{
    /// <summary>
    /// Bootstrapper for the application.
    /// </summary>
    /// <seealso cref="Prism.Mef.MefBootstrapper" />
    public sealed class StockViewerBootstrapper : MefBootstrapper
    {
        #region Initialization

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        /// <remarks>
        /// The base implementation ensures the shell is composed in the container.
        /// </remarks>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            ClientContext.Instance.Initialize(this.Container.GetExportedValue<IClientHost>());
            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }

        #endregion

        #region Configuration

        /// <summary>
        /// Configures the <see cref="P:Prism.Mef.MefBootstrapper.AggregateCatalog" /> used by MEF.
        /// </summary>
        /// <remarks>
        /// The base implementation does nothing.
        /// </remarks>
        protected override void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(StockViewerBootstrapper).Assembly));
            var modulesDirectoryCatalog = new DirectoryCatalog(@".//Modules");
            AggregateCatalog.Catalogs.Add(modulesDirectoryCatalog);
        }

        /// <summary>
        /// Configures the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.
        /// May be overwritten in a derived class to add specific type mappings required by the application.
        /// </summary>
        /// <remarks>
        /// The base implementation registers all the types direct instantiated by the bootstrapper with the container.
        /// If the method is overwritten, the new implementation should call the base class version.
        /// </remarks>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        /// <summary>
        /// Configures the <see cref="T:Prism.Regions.IRegionBehaviorFactory" />.
        /// This will be the list of default behaviors that will be added to a region.
        /// </summary>
        /// <returns></returns>
        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));

            return factory;
        }


        #endregion

        #region Shell

        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>
        /// The shell of the application.
        /// </returns>
        /// <remarks>
        /// If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        /// <see cref="T:Prism.Bootstrapper" /> will attach the default <see cref="T:Prism.Regions.IRegionManager" /> of
        /// the application in its <see cref="F:Prism.Regions.RegionManager.RegionManagerProperty" /> attached property
        /// in order to be able to add regions by using the <see cref="F:Prism.Regions.RegionManager.RegionNameProperty" />
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<Shell>();
        }

        #endregion

    }
}

