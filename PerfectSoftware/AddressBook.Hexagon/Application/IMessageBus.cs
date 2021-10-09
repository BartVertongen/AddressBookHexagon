
using System.Collections.Generic;


namespace PS.AddressBook.Hexagon.Application.Messaging
{
    public interface IMessageBus
    {
        void Send(Envelope<IMessage> message);

        void Send(IEnumerable<Envelope<IMessage>> messagess);
    }
}