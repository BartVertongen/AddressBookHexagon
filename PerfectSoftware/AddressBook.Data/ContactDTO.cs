//By Bart Vertongen copyright 2021

using System.Xml.Serialization;
using PS.AddressBook.Data.Interfaces;


namespace PS.AddressBook.Data
{
    //We do this so the XML node will be Contact and not ContactDTO
    [XmlType("Contact")]
    [XmlRoot("Contact")]
    public class ContactDTO : IContactDTO
    {
        private IAddressDTO _Address;

        public string Name { get; set; }

        /// <summary>
        /// To prevent problems with IContactDTO we have this property
        /// </summary>
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