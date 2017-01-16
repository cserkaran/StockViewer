using StockViewer.Infrastructure.Interfaces;
using System;
using System.ComponentModel.Composition;

namespace StockViewer.Infrastructure.MefAttributes
{
    /// <summary>
    /// Mef Export attribute to export the region parts.
    /// </summary>
    /// <seealso cref="ExportAttribute" />
    /// <seealso cref="IViewRegionRegistration" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [MetadataAttribute]
    public sealed class ViewExportAttribute : ExportAttribute, IViewRegionRegistration
    {
        #region Properties 

        /// <summary>
        /// Gets the name of the view.
        /// </summary>
        /// <value>
        /// The name of the view.
        /// </value>
        public string ViewName { get { return base.ContractName; } }

        /// <summary>
        /// Gets the name of the region.
        /// </summary>
        /// <value>
        /// The name of the region.
        /// </value>
        public string RegionName { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewExportAttribute"/> class.
        /// </summary>
        public ViewExportAttribute()
            : base(typeof(object))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewExportAttribute"/> class.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        public ViewExportAttribute(string viewName)
            : base(viewName, typeof(object))
        { }

        #endregion
    }
}
