

namespace PS.AddressBook.Hexagon.Application.Ports
{
    public interface IUpdateContactCommand
    {
        string Name { get; }

        string Phone { get; }

        string Email { get; }

        string Street { get; }

        string PostalCode { get; }

        string Town { get; }
    }
}