//By Bart Vertongen copyright 2021.

using System.Collections.Generic;
using PS.AddressBook.Hexagon.Domain.Ports;
using PS.AddressBook.Hexagon.Application.Mappers;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Ports.Out;


namespace PS.AddressBook.Hexagon.Application.Services
{
    /// <summary>
    /// This is an adapter for the DeleteContactUseCase.
    /// </summary>
    public class DeleteContactService : IDeleteContactUseCase
    {
        private readonly IAddressBookFile _AddressBookFilePort;

        public DeleteContactService(IAddressBookFile file)
        {
            _AddressBookFilePort = file;
        }

        /// <summary>
        /// Deletes a Contact with a given Name.
        /// </summary>
        /// <param name="command">The Delete Command containing the Name to delete.</param>
        /// <returns></returns>
        public IContactDTO DeleteContact(DeleteContactCommand command)
        {
            IAddressBook oAddressBook;
            AddressBookDTOMapper oAddressBookDTOMapper = new();
            IList<IContactDTO> oAddressBookDTO = new List<IContactDTO>();

            _AddressBookFilePort.Load(oAddressBookDTO);
            oAddressBook = oAddressBookDTOMapper.MapFrom(oAddressBookDTO);
            if (oAddressBook.ContainsName(command.Name))
            {
                IContact FoundContact;
                ContactDTOMapper oContactDTOMapper = new();
               
                FoundContact = oAddressBook.GetContact(command.Name);
                oAddressBook.Delete(command.Name);
                //Saves the changes to the AddressBook
                _AddressBookFilePort.Save(oAddressBookDTOMapper.MapTo(oAddressBook));
                return oContactDTOMapper.MapTo(FoundContact);
            }
            else
                return null;
        }
    }
}