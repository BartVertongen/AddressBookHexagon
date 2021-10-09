

namespace PS.AddressBook.Hexagon.Application.Messaging
{
    public interface ICommandHandlerRegistry
    {
        void Register(ICommandHandler handler);
    }
}