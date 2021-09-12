//By Bart Vertongen copyright 2021.

using PS.AddressBook.Data.Interfaces;


namespace PS.AddressBook.Business
{
    public class Address: IAddress
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

        public Address(IAddress bussRef)
        {
            this.Street = bussRef.Street;
            this.PostalCode = bussRef.PostalCode;
            this.Town = bussRef.Town;
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

        public bool IsEmpty()
        {
            if (string.IsNullOrEmpty(this.Street) 
                        && string.IsNullOrEmpty(this.PostalCode) 
                        && string.IsNullOrEmpty(this.Town)
               )
                return true;
            else
                return false;
        }

        public bool IsValid()
        {
            if (this.IsEmpty())
                return true;
            else if (string.IsNullOrEmpty(this.Street))
                return false;
            else if (string.IsNullOrEmpty(this.PostalCode))
                return false;
            else if (string.IsNullOrEmpty(this.Town))
                return false;
            else
                return true;
        }
    }
}