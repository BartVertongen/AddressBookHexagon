
using System.Collections.Generic;


namespace PS.AddressBook.Hexagon.Application.Messaging
{
    /// <summary>
    /// An event bus that sends serialized object payloads.
    /// </summary>
    /// <remarks>Note that <see cref="Infrastructure.EventSourcing.IEventSourced"/> entities persisted through 
    /// the <see cref="Infrastructure.EventSourcing.IEventSourcedRepository{T}"/> do not
    /// use the <see cref="IEventBus"/>, but has its own event publishing mechanism.</remarks>
    public interface IEventBus
    {
        void Publish(Envelope<IEvent> @event);

        void Publish(IEnumerable<Envelope<IEvent>> events);
    }
}
