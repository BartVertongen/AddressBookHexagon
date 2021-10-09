//By Bart Vertongen copyright 2021

using PS.AddressBook.Hexagon.Domain.Core;


namespace PS.AddressBook.Hexagon.Domain
{
    public class AdapterFromContactDTO : IContact
    {
        private readonly IContactDTO _Adaptee;

        public AdapterFromContactDTO(IContactDTO adaptee)
        {
            _Adaptee = adaptee;
        }

        public string Name
        {
            get { return _Adaptee.Name; } 
            set => throw new System.NotImplementedException(); 
        }

        public IAddress Address 
        {
            get 
            {
                AdapterFromAddressDTO Adapter;
                Adapter = new AdapterFromAddressDTO(_Adaptee.Address);
                return Adapter; 
            }
            
            set => throw new System.NotImplementedException(); 
        }

        public string PhoneNumber 
        { 
            get { return _Adaptee.PhoneNumber; }
            set => throw new System.NotImplementedException(); 
        }

        public string Email 
        {
            get { return _Adaptee.Email; }
            set => throw new System.NotImplementedException(); 
        }

        //An adapter will not implement a Setter.
        public IAddressBook AddressBook { set => throw new System.NotImplementedException(); }

        public string ContentsCode
        {
            get
            { return Contact.GetContentsCode(_Adaptee); }
        }

        public void Assign(IContact newvalues)
        {
            throw new System.NotImplementedException();
        }


        //REM: This should not be done using an Adapter.
        public IContact DeepClone()
        {
            return null;
        }

        public bool Equals(IContact other)
        {
            throw new System.NotImplementedException();
        }

        public bool IsValid()
        {
            return Contact.IsValid((IContact)_Adaptee);
        }
    }
}