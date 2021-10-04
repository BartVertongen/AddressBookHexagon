// By Bart Vertongen copyright 2021

using PS.AddressBook.Data.Interfaces;
using System;


namespace PS.AddressBook.Business.Adapters
{
    public class AdapterFromAddressDTO: IAddress
    {
        private IAddressDTO _Adaptee;

        public AdapterFromAddressDTO(IAddressDTO adaptee)
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

        public void Assign(IAddress newvalues)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IAddress other)
        {
            throw new NotImplementedException();
        }

        //TODO: Here we repeat code! DRY
        public bool IsValid()
        {
            if (
                    string.IsNullOrEmpty(_Adaptee.Street)
                    && string.IsNullOrEmpty(_Adaptee.PostalCode)
                    && string.IsNullOrEmpty(_Adaptee.Town)
               )
                return true;
            else if (
                        !string.IsNullOrEmpty(_Adaptee.Street)
                        && !string.IsNullOrEmpty(_Adaptee.PostalCode)
                        && !string.IsNullOrEmpty(_Adaptee.Town)
                )
                return true;
            else
                return false;
        }
    }
}