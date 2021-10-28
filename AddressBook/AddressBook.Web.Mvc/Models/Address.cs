//By Bart Vertongen copyright 2021

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AddressBook.Web.Mvc.Models
{
    public class Address : IValidatableObject
    {
        public string Street { get; set; } = "";

        public string PostalCode { get; set; } = "";

        public string Town { get; set; } = "";

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool bEmptyTown, bEmptyStreet, bEmptyPostalCode;

            bEmptyStreet = string.IsNullOrEmpty(Street);
            bEmptyPostalCode = string.IsNullOrEmpty(Street);
            bEmptyTown = string.IsNullOrEmpty(Town);
            if (
                (bEmptyStreet || bEmptyPostalCode || bEmptyTown)
                && !(bEmptyStreet && bEmptyPostalCode && bEmptyTown)
               )
            {
                yield return new ValidationResult(
                    $"A valid address is completely empty or has no empty values.",
                        new[] {nameof(Street), nameof(PostalCode), nameof(Town)});
            }
        }
    }
}