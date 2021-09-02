//Copyright 2021 Bart Vertongen

using System.IO;
using System.Xml.Serialization;


namespace PS.AddressBook.Business
{
    public class Contact
    {
        private string _Name;

        public Contact()
        {
            this.Name = "";
            this.Address = new Address();
            this.PhoneNumber = "";
            this.Email = "";
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

        [XmlIgnore]
        public AddressBook AddressBook { private get;  set;  }

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

        public Address Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string ContentsCode
        {
            get
            {
                string sContentsCode;

                sContentsCode = (this.Address == null || this.Address.IsEmpty() ? "*" : "A");
                sContentsCode += string.IsNullOrEmpty(this.PhoneNumber) ? "*" : "P";
                sContentsCode += string.IsNullOrEmpty(this.Email) ? "*" : "E";
                return sContentsCode;
            } 
        }

        public ContactLine ContactLine
        {
            get
            {
                ContactLine oContactLine = new ContactLine();
                oContactLine.Name = this.Name;
                oContactLine.ContentsCode = this.ContentsCode;
                return oContactLine;
            }
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(this.Name))
                return false;
            else if (string.IsNullOrEmpty(this.PhoneNumber) && string.IsNullOrEmpty(this.Email))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Creates a Deep Copy of this Contact.
        /// </summary>
        /// <returns></returns>
        public Contact DeepClone()
        {
            Contact oCopy = new Contact();
            oCopy.Name = this.Name;
            oCopy.PhoneNumber = this.PhoneNumber;
            oCopy.Email = this.Email;
            oCopy.Address = new Address(Address.Street, Address.PostalCode, Address.Town);
            return oCopy;
        }
    }
}