
using System.Collections.Generic;


namespace PS.AddressBook.Hexagon.Application.Messaging
{
    

    /// <summary>
    /// Defines that the object exposes events that are meant to be published.
    /// </summary>
    public interface IEventPublisher
    {
        IEnumerable<IEvent> Events { get; }
    }
}
