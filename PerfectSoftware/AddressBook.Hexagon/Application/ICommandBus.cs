
using System.Collections.Generic;


namespace PS.AddressBook.Hexagon.Application.Messaging
{
    /// <summary>
    /// Sends or Dispatches Commands to be handled by the right handler.
    /// </summary>
    public interface ICommandBus : IMessageBus
    {
        void Send(Envelope<ICommand> command);

        void Send(IEnumerable<Envelope<ICommand>> commands);
    }
}