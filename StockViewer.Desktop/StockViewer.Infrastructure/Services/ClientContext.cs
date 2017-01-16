using System.ComponentModel.Composition;

namespace StockViewer.Infrastructure.Services
{
    /// <summary>
    /// This is exposes all the client platform services like
    /// exception handling, logging to name a few.
    /// </summary>
    public class ClientContext : SingletonBase<ClientContext>
    {
        #region Properties 

        /// <summary>
        /// Gets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public IClientHost Host { get; private set; }

        #endregion

        #region Initialization

        public void Initialize(IClientHost host)
        {
            this.Host = host;
        }

        #endregion
    }
}
