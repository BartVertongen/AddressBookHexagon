//By Bart Vertongen copyright 2021

using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Hexagon.Application
{
    /// <summary>
    /// The DeleteContactCommandBuilder Data Transfer Object
    /// </summary>
    public class DeleteContactCommandBuilderDTO : IDeleteContactCommandBuilderDTO
    {
        /// <summary>
        /// The default constructor for the Contact Data Transfer Object.
        /// </summary>
        public DeleteContactCommandBuilderDTO()
        {
            Name = "";
        }

        public string Name { get; set; }
    }
}