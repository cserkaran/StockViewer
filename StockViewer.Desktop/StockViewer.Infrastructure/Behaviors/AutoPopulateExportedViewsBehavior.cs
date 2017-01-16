using Prism.Regions;
using StockViewer.Infrastructure.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace StockViewer.Infrastructure.Behaviors
{
    /// <summary>
    /// Populate the exported views.
    /// </summary>
    /// <seealso cref="RegionBehavior" />
    /// <seealso cref="IPartImportsSatisfiedNotification" />
    [Export(typeof(AutoPopulateExportedViewsBehavior))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AutoPopulateExportedViewsBehavior : RegionBehavior, IPartImportsSatisfiedNotification
    {
        #region Properties 

        /// <summary>
        /// Gets or sets the registered views.
        /// </summary>
        /// <value>
        /// The registered views.
        /// </value>
        [ImportMany(AllowRecomposition = true)]
        public Lazy<object, IViewRegionRegistration>[] RegisteredViews { get; set; }

        #endregion

        #region Add Registered Views

        /// <summary>
        /// Override this method to perform the logic after the behavior has been attached.
        /// </summary>
        protected override void OnAttach()
        {
            AddRegisteredViews();
        }

        /// <summary>
        /// Called when [imports satisfied].
        /// </summary>
        public void OnImportsSatisfied()
        {
            AddRegisteredViews();
        }

        /// <summary>
        /// Adds the registered views.
        /// </summary>
        private void AddRegisteredViews()
        {
            if (this.Region != null)
            {
                foreach (var viewEntry in this.RegisteredViews)
                {
                    if (viewEntry.Metadata.RegionName == this.Region.Name)
                    {
                        var view = viewEntry.Value;

                        if (!this.Region.Views.Contains(view))
                        {
                            this.Region.Add(view);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
