

namespace PS.AddressBook.Hexagon.Application.Ports
{
    public interface IUpdateContactCommandDTO
    {
        string Name { get; set; }

        string Phone { get; set;  }

        string Email { get; set; }

        string Street { get; set; }

        string PostalCode { get; set;  }

        string Town { get; set;  }
    }
}