using Prism.Events;
using System.Collections.Generic;

namespace StockViewer.Infrastructure.DomainEvents
{
    /// <summary>
    /// Domain event to notify the update in stock market price.
    /// </summary>
    /// <seealso cref="Prism.Events.PubSubEvent{System.Collections.Generic.IDictionary{System.String, System.Decimal}}" />
    public class MarketPriceUpdatedEvent : PubSubEvent<IDictionary<string, decimal>>
    {
    }
}
