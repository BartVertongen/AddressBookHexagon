//Copyright 2021 Bart Vertongen


namespace AddressBookLib
{
    /// <summary>
    /// A ContactLine is a short overview of a Contact.
    /// </summary>
    public class ContactLine
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