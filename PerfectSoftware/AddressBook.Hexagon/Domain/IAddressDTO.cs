

namespace PS.AddressBook.Hexagon.Domain
{
    public interface IAddressDTO
    {
        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string Town { get; set; }
    }
}