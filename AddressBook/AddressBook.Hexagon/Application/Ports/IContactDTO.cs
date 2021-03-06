


namespace PS.AddressBook.Hexagon.Application.Ports
{
    public interface IContactDTO
    {
        public string Name { get; set; }

        public IAddressDTO Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}