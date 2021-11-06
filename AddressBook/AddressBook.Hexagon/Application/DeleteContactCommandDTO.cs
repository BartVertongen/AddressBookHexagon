//By Bart Vertongen copyright 2021

using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application
{
    /// <summary>
    /// The DeleteContactCommand Data Transfer Object
    /// </summary>
    public class DeleteContactCommandDTO : IDeleteContactCommandDTO
    {
        /// <summary>
        /// The default constructor for the Contact Data Transfer Object.
        /// </summary>
        public DeleteContactCommandDTO()
        {
            Name = "";
        }

        public string Name { get; private set; }
    }
}