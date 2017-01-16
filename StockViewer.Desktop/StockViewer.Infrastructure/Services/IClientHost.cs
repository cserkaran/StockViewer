using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockViewer.Infrastructure.Services
{
    /// <summary>
    /// this is the interface of main ui application exposed to child views.
    /// </summary>
    public interface IClientHost
    {
        /// <summary>
        /// the messaging service.
        /// </summary>
        IMessagingService MessageService { get; }
    }
}
