// By Bart Vertongen copyright 2021

using PS.AddressBook.Hexagon.Domain;
using System;


namespace PS.AddressBook.Hexagon.Application
{
    public class AdapterToAddressDTO : IAddressDTO
    {
        private readonly IAddress _Adaptee;

        public AdapterToAddressDTO(IAddress adaptee)
        {
            _Adaptee = adaptee;
        }

        public string Street
        { 
            get { return _Adaptee.Street; }
            set => throw new NotImplementedException(); 
        }

        public string PostalCode 
        {
            get { return _Adaptee.PostalCode; }
            set => throw new NotImplementedException(); 
        }

        public string Town
        { 
            get { return _Adaptee.Town; }
            set => throw new NotImplementedException();
        }
    }
}