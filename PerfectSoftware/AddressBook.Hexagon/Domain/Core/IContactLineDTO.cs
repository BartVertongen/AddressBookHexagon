


namespace PS.AddressBook.Hexagon.Domain.Core
{
    public interface IContactLineDTO
    {
        int Id { get; set; }

        /// <summary>
        /// The Unique Name of the Contact.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// A Code that gives an overview of the Contents of the Contact.
        /// </summary>
        /// <example>
        /// "APE" => Has Address, PhoneNumber and Email
        /// "**E" => Has only an Email.
        /// </example>
        string ContentsCode { get; set; }
    }
}