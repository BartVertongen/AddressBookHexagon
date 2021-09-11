//Copyright 2021 Bart Vertongen

using PS.AddressBook.Business.Interfaces;


namespace PS.AddressBook.Business
{
    /// <summary>
    /// A ContactLine is a short overview of a Contact.
    /// </summary>
    public class ContactLineDTO: IContactLineDTO
    {
        /// <summary>
        /// A Unique Id for this ContactLine.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Unique Name of the Contact.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A Code that gives an overview of the Contents of the Contact.
        /// </summary>
        /// <example>
        /// "APE" => Has Address, PhoneNumber and Email
        /// "**E" => Has only an Email.
        /// </example>
        public string ContentsCode { get; set; }
    }
}