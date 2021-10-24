//By Bart Vertongen copyright 2021.

using System.ComponentModel.DataAnnotations;


namespace AddressBook.Web.Razor.ViewModels
{
    public class Contact
    {
        [StringLength(70, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [Required]     
        public Address Address { get; set; }

        public string Phone { get; set; } = "";

        public string Email { get; set; } = "";
    }
}
