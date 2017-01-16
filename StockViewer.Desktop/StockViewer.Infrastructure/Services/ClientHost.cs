using System.ComponentModel.Composition;

namespace StockViewer.Infrastructure.Services
{
    /// <summary>
    /// The client host implementation.
    /// </summary>
    /// <seealso cref="StockViewer.Infrastructure.Services.IClientHost" />
    [Export(typeof(IClientHost))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ClientHost : IClientHost
    {
        #region Properties

        /// <summary>
        /// the messaging service.
        /// </summary>
        [Import]
        public IMessagingService MessageService { get; private set; }

        #endregion
    }
}
