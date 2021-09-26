// By Bart Vertongen copyright 2021


namespace PS.AddressBook.Data.Interfaces
{
    public interface IContact
    {
        string Name { get; set; }

        IAddress Address { get; set; }

        string PhoneNumber { get; set; }

        string Email { get; set; }

        public IAddressBook AddressBook {  set; }

        public string ContentsCode { get; }


        public bool IsValid();

        /// <summary>
        /// Creates a Deep Copy of this Contact.
        /// </summary>
        /// <returns></returns>
        public IContact DeepClone();
    }
}