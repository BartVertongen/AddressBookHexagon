//Copyright 2021 Bart Vertongen

using PS.AddressBook.Data.Interfaces;
using System.IO;


namespace PS.AddressBook.Business
{
    public class Contact: IContact
    {
        private string _Name;

        public Contact()
        {
            this.Name = "";
            this.Address = new Address();
            this.PhoneNumber = "";
            this.Email = "";
        }

        public Contact(IContact bussRef)
        {
            this.Name = bussRef.Name;
            this.Address = new Address(bussRef.Address);
            this.PhoneNumber = bussRef.PhoneNumber;
            this.Email = bussRef.Email;
        }

        /// <summary>
        /// Constructor for Contact.
        /// </summary>
        /// <param name="addressBook"></param>
        public Contact(AddressBook addressBook)
        {
            AddressBook = addressBook;
            this.Address = new Address();
            this.PhoneNumber = "";
            this.Email = "";
        }

        public IAddressBook AddressBook { private get;  set;  }

        public string Name
        { 
            get { return _Name; }

            set
            {
                if (AddressBook != null && AddressBook.ContainsName(value))
                    throw new InvalidDataException($"Name {value} exists allready");
                else
                    _Name = value;
            } 
        }

        public IAddress Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        static public string GetContentsCode(IContact contact)
        {
            string sContentsCode;
            Address bussAddress = new Address
            {
                Street = contact.Address.Street,
                PostalCode = contact.Address.PostalCode,
                Town = contact.Address.Town
            };
            sContentsCode = bussAddress.IsEmpty() ? "*" : "A";
            sContentsCode += string.IsNullOrEmpty(contact.PhoneNumber) ? "*" : "P";
            sContentsCode += string.IsNullOrEmpty(contact.Email) ? "*" : "E";
            return sContentsCode;
        }

        static public string GetContentsCode(IContactDTO contact)
        {
            string sContentsCode;
            Address bussAddress = new Address
            {
                Street = contact.Address.Street,
                PostalCode = contact.Address.PostalCode,
                Town = contact.Address.Town
            };
            sContentsCode = bussAddress.IsEmpty() ? "*" : "A";
            sContentsCode += string.IsNullOrEmpty(contact.PhoneNumber) ? "*" : "P";
            sContentsCode += string.IsNullOrEmpty(contact.Email) ? "*" : "E";
            return sContentsCode;
        }

        public string ContentsCode
        {
            get
            {
                return Contact.GetContentsCode(this);
            } 
        }

        public ContactLineDTO ContactLine
        {
            get
            {
                ContactLineDTO oContactLine = new ContactLineDTO
                {
                    Name = this.Name,
                    ContentsCode = this.ContentsCode
                };
                return oContactLine;
            }
        }

        static public bool IsValid (IContact contact)
        {
            if (string.IsNullOrEmpty(contact.Name))
                return false;
            else if (string.IsNullOrEmpty(contact.PhoneNumber) && string.IsNullOrEmpty(contact.Email))
                return false;
            else if (!contact.Address.IsValid())
                return false;
            else
                return true;
        }

        public bool IsValid()
        {
            return Contact.IsValid(this);
        }

        /// <summary>
        /// Creates a Deep Copy of this Contact.
        /// </summary>
        /// <returns></returns>
        public IContact DeepClone()
        {
            Contact oCopy = new Contact
            {
                Name = this.Name,
                PhoneNumber = this.PhoneNumber,
                Email = this.Email,
                Address = new Address(Address.Street, Address.PostalCode, Address.Town)
            };
            return oCopy;
        }
    }
}