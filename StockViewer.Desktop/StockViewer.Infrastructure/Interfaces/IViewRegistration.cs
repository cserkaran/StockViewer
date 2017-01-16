namespace StockViewer.Infrastructure.Interfaces
{
    /// <summary>
    /// Interface to register a view region with a given RegionName
    /// </summary>
    public interface IViewRegionRegistration
    {
        /// <summary>
        /// Gets the name of the region.
        /// </summary>
        /// <value>
        /// The name of the region.
        /// </value>
        string RegionName { get; }
    }
}
