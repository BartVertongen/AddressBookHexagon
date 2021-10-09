

namespace PS.AddressBook.Hexagon.Domain.Core
{
    public interface IAddressDTO
    {
        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string Town { get; set; }
    }
}