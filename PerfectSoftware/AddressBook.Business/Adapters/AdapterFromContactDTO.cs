//By Bart Vertongen copyright 2021

using PS.AddressBook.Data.Interfaces;


namespace PS.AddressBook.Business.Adapters
{
    public class AdapterFromContactDTO : IContact
    {
        private IContactDTO _Adaptee;

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
    }
}
