//By Bart Vertongen copyright 2021

using System.Xml.Serialization;
using PS.AddressBook.Data.Interfaces;


namespace PS.AddressBook.Data
{
    [XmlRoot(ElementName ="Contact")]
    public class ContactDTO : IContactDTO
    {
        private IAddressDTO _Address;

        public string Name { get; set; }

        [XmlIgnore]
        IAddressDTO IContactDTO.Address
        {
            get { return _Address;  }
            set { _Address = value; } 
        }

        [XmlElement(ElementName="Address")]
        public AddressDTO Address
        {
            get { return (AddressDTO)_Address; }
            set { _Address = value; }
        }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}