
using System;


namespace PS.AddressBook.Hexagon.Application.Messaging
{
    /// <summary>
    /// Represents an event message.
    /// </summary>
    public interface IEvent : IMessage
    {
        /// <summary>
        /// Gets the identifier of the source originating the event.
        /// </summary>
        Guid SourceId { get; }
    }
}
