using System;
using System.Collections.Generic;


namespace PS.AddressBook.Data.Interfaces
{
    public interface IContact
    {
        public string Name { get; set; }

        public IAddress Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}