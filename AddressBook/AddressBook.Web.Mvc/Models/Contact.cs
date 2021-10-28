//By Bart Vertongen copyright 2021.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AddressBook.Web.Mvc.Models
{
    public class Contact : IValidatableObject
    {
        [StringLength(70, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [Required]     
        public Address Address { get; set; }

        public string Phone { get; set; } = "";

        public string Email { get; set; } = "";

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (string.IsNullOrEmpty(Phone) && string.IsNullOrEmpty(Email))
            {
                yield return new ValidationResult(
                    $"A Contact needs an Email or a Phone.",
                        new[] { nameof(Phone), nameof(Email) });
            }
        }
    }
}
