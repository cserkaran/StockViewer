using Prism.Events;

namespace StockViewer.Infrastructure.DomainEvents
{
    /// <summary>
    /// Domain event to publish changes when ticker is selected.
    /// </summary>
    /// <seealso cref="Prism.Events.PubSubEvent{System.String}" />
    public class TickerSymbolSelectedEvent : PubSubEvent<string>
    {
    }
}
