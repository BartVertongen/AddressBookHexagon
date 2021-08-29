//Copyright 2021 Bart Vertongen

using System.IO;
using System.Xml.Serialization;


namespace AddressBookLib
{
    public class Contact
    {
        private string _Name;

        public Contact() {}

        /// <summary>
        /// Constructor for Contact.
        /// </summary>
        /// <param name="addressBook"></param>
        public Contact(AddressBook addressBook)
        {
            AddressBook = addressBook;
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

        public string EmailAddress { get; set; }

        public string ContentsCode
        {
            get
            {
                string sContentsCode;

                sContentsCode = (this.Address != null ? "A" : "*");
                sContentsCode += string.IsNullOrEmpty(this.PhoneNumber) ? "*" : "P";
                sContentsCode += string.IsNullOrEmpty(this.EmailAddress) ? "*" : "E";
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
    }
}