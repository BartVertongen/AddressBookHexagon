//By Bart Vertongen copyright 2021

using PS.AddressBook.Hexagon.Domain.Core;


namespace PS.AddressBook.Hexagon.Domain
{
    public class AdapterToContactDTO : IContactDTO
    {
        private IContact _Adaptee;

        public AdapterToContactDTO(IContact adaptee)
        {
            _Adaptee = adaptee;
        }

        public string Name
        {
            get { return _Adaptee.Name; } 
            set => throw new System.NotImplementedException(); 
        }

        public IAddressDTO Address 
        {
            get 
            {
                AdapterToAddressDTO Adapter;
                Adapter = new AdapterToAddressDTO(_Adaptee.Address);
                return (IAddressDTO)Adapter; 
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
