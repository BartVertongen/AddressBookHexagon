//By Bart Vertongen copyright 2021

using System.Collections.Generic;
using PS.AddressBook.Business.Adapters;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.Data.Interfaces;


namespace PS.AddressBook.Business
{
    public class AddressBookService : IAddressBookService
    {
        private readonly IAddressBook _AddressBook;

        public AddressBookService(IAddressBook addressBook)
        {
            _AddressBook = addressBook;
        }

        public void Add(IContactDTO newContact)
        {
            AdapterFromContactDTO Adapter = new(newContact);
            _AddressBook.Add((IContact)Adapter);
        }

        public void Delete(string name)
        {
            _AddressBook.Delete(name);
        }

        public IContactDTO Get(string name)
        {
            IContact FoundContact = _AddressBook.GetContact(name);
            AdapterToContactDTO Adapter = new(FoundContact);
            return Adapter;
        }

        public IList<IContactLineDTO> GetOverview(string filter)
        {
            AddressBook objAddressBook = _AddressBook as AddressBook;
            return objAddressBook.GetOverview(filter);
        }

        public void Update(IContactDTO changedContact)
        {
            AdapterFromContactDTO Adapter = new(changedContact);
            _AddressBook.Update(Adapter);
        }
    }
}