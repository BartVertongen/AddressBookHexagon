

namespace PS.AddressBook.Hexagon.Application.Messaging
{
    public interface IEventHandlerRegistry
    {
        void Register(IEventHandler handler);
    }
}