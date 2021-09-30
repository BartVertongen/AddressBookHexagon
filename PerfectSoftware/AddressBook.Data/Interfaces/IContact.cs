// By Bart Vertongen copyright 2021


namespace PS.AddressBook.Data.Interfaces
{
    /// <summary>
    /// The Interface for the Contact Business Object.
    /// </summary>
    public interface IContact
    {
        /// <summary>
        /// The Name of the Contact
        /// </summary>
        string Name { get; set; }

        IAddress Address { get; set; }

        /// <summary>
        /// The Phone Number of the Contact.
        /// </summary>
        string PhoneNumber { get; set; }

        string Email { get; set; }

        public IAddressBook AddressBook {  set; }

        public string ContentsCode { get; }

        /// <summary>
        /// Flag whether this Contact contains valid data.
        /// </summary>
        /// <returns></returns>
        public bool IsValid();

        /// <summary>
        /// Creates a Deep Copy of this Contact.
        /// </summary>
        /// <returns></returns>
        public IContact DeepClone();
    }
}