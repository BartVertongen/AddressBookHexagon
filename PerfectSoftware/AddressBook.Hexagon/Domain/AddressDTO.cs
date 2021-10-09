//By Bart Vertongen copyright 2021

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PS.AddressBook.Hexagon.Domain.Core;


namespace PS.AddressBook.Hexagon.Domain
{
    /// <summary>
    /// Address Data Transfer Type.
    /// </summary>
    //[DataContract(Name = "Address", Namespace = "http://vertongensoftware.be")]
    [Display(Name ="Address")]
    public class AddressDTO : IAddressDTO
    {
        /// <summary>
        /// The default constructor for an Adress Data Transfer Object
        /// </summary>
        public AddressDTO()
        {
            this.Street = "";
            this.PostalCode = "";
            this.Town = "";
        }

        public AddressDTO(IAddressDTO dtoRef)
        {
            this.Street = dtoRef.Street;
            this.PostalCode = dtoRef.PostalCode;
            this.Town = dtoRef.Town;
        }

        /// <summary>
        ///The Street of an Address Data Transfer Object.
        /// </summary>
        [Required]
        [DefaultValue("")]
        public string Street { get; set; }

        [Required]
        [DefaultValue("")]
        public string PostalCode { get; set; }

        [Required]
        [DefaultValue("")]
        public string Town { get; set; }
    }
}