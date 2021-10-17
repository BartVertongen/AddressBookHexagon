//Copyright 2021 Bart Vertongen

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application
{
    /// <summary>
    /// A ContactLine is a short overview of a Contact.
    /// </summary>
    [Display(Name="ContactLine")]
    public class ContactLineDTO: IContactLineDTO
    {
        /// <summary>
        /// A Unique Id for this ContactLine.
        /// </summary>
        [Required]
        [ReadOnly(true)]
        public int Id { get; set; }

        /// <summary>
        /// The Unique Name of the Contact.
        /// </summary>
        [Required]
        [ReadOnly(true)]
        public string Name { get; set; }

        /// <summary>
        /// A Code that gives an overview of the Contents of the Contact.
        /// </summary>
        /// <example>
        /// "APE" => Has Address, PhoneNumber and Email
        /// "**E" => Has only an Email.
        /// </example>     
        [Required]
        [ReadOnly(true)]
        public string ContentsCode { get; set; }
    }
}