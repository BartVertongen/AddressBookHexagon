// By Bart Vertongen copyright 2021

using PS.AddressBook.Data.Interfaces;
using System;


namespace PS.AddressBook.Business.Adapters
{
    public class AdapterToAddressDTO: IAddressDTO
    {
        private IAddress _Adaptee;

        public AdapterToAddressDTO(IAddress adaptee)
        {
            _Adaptee = adaptee;
        }

        public string Street
        { 
            get { return _Adaptee.Street;  }
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