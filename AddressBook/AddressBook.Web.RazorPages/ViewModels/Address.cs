
using System.ComponentModel.DataAnnotations;
using PS.AddressBook.Hexagon.Application;


namespace AddressBook.Web.Razor.ViewModels
{
    public class Address : IAddressDTO
    {
        [Required]
        public string Street { get; set; } = "";

        [Required]
        public string PostalCode { get; set; } = "";

        [Required]
        public string Town { get; set; } = "";
    }
}
