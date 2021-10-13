﻿


namespace PS.AddressBook.Hexagon.Domain
{
    public interface IContactDTO
    {
        public string Name { get; set; }

        public IAddressDTO Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}