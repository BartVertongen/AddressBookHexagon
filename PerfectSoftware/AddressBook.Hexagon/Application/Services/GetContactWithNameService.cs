//By Bart Vertongen copyright 2021

using PS.AddressBook.Hexagon.Domain;
using BussAddressBook = PS.AddressBook.Hexagon.Domain.AddressBook;


namespace PS.AddressBook.Hexagon.Application.Services
{
    public class GetContactWithNameService
    {
        private readonly IAddressBookFile _LoadAddressBookPort;

        public GetContactWithNameService(IAddressBookFile file)
        {
            _LoadAddressBookPort = file;
        }

        public IContactDTO GetContactWithName(string name)
        {
            IContact FoundContact;
            IAddressBook oAddressBook = new BussAddressBook(_LoadAddressBookPort);

            FoundContact = oAddressBook.GetContact(name);
            if (FoundContact == null)
                return null;
            else
            {
                AdapterToContactDTO ContactAdapter = new(FoundContact);
                return ContactAdapter;
            }
        }
    }
}