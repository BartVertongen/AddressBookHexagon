//By Bart Vertongen copyright 2021.

using System.IO;


namespace AddressBookLib
{
    public class Address
    {
        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string Town { get; set; }

        /// <summary>
        /// Constructor for an empty Address.
        /// </summary>
        public Address()
        {
            this.Street = "";
            this.PostalCode = "";
            this.Town = "";
        }

        /// <summary>
        /// Constructor for a valid Address.
        /// </summary>
        /// <param name="street">Can not be null or empty</param>
        /// <param name="postalCode">Can not be null or empty</param>
        /// <param name="town">Can not be null or empty</param>
        public Address(string street, string postalCode, string town)
        {
            if (string.IsNullOrEmpty(street) || string.IsNullOrEmpty(postalCode) || string.IsNullOrEmpty(town))
            {
                this.Street = "";
                this.PostalCode = "";
                this.Town = "";
            }
            else
            {
                this.Street = street;
                this.PostalCode = postalCode;
                this.Town = town;
            }
        }
    }
}