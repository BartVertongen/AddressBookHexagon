//By Bart Vertongen copyright 2021

using System.Xml.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PS.AddressBook.Data.Interfaces;


namespace PS.AddressBook.Data
{
    /// <summary>
    /// The Contact Data Transfer Object
    /// </summary>
    [XmlType("Contact")]
    [XmlRoot("Contact")]
    public class ContactDTO : IContactDTO
    {
        private IAddressDTO _Address;

        /// <summary>
        /// The default constructor for the Contact Data Transfer Object.
        /// </summary>
        public ContactDTO()
        {
            this.Name = "";
            this.PhoneNumber = "";
            this.Email = "";
            this.Address = new AddressDTO();
        }

        public ContactDTO(IContactDTO dtoRef)
        {
            this.Name = dtoRef.Name;
            this.PhoneNumber = dtoRef.PhoneNumber;
            this.Email = dtoRef.Email;
            this.Address = new AddressDTO(dtoRef.Address);
        }

        /// <summary>
        /// The Unique Name of the Contact.
        /// </summary>
        [Required]
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

        [DefaultValue("")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The Email of the Contact.
        /// </summary>
        [DefaultValue("")]
        public string Email { get; set; }

    }
}