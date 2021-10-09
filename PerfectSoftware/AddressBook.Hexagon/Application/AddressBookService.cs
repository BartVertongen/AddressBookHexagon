//By Bart Vertongen copyright 2021

using System;
using System.Collections.Generic;
using PS.AddressBook.Hexagon.Domain.Core;
using PS.AddressBook.Hexagon.Domain;


namespace PS.AddressBook.Hexagon.Application
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
            try
            {
                AdapterFromContactDTO Adapter = new(newContact);
                _AddressBook.Add(Adapter);
                _AddressBook.Save();
            }
            catch (Exception)
            {
                throw; //Does it help to improve the stack ?
            }
        }

        public void Delete(string name)
        {
            _AddressBook.Delete(name);
            _AddressBook.Save();
        }

        public IContactDTO Get(string name)
        {
            IContact FoundContact;
            AdapterToContactDTO Adapter;

            FoundContact = _AddressBook.GetContact(name);
            if (FoundContact == null)
                return null;
            else
            {
                Adapter = new(FoundContact);
                return Adapter;
            }
        }

        /// <summary>
        /// Gives an overview of the Contacts in the AddressBook filtered.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IList<IContactLineDTO> GetOverview(string filter)
        {
            return _AddressBook.GetOverview(filter);
        }

        /// <summary>
        /// Changes an existing Contact from the AddressBook.
        /// </summary>
        /// <param name="changedContact"></param>
        public void Update(IContactDTO changedContact)
        {
            AdapterFromContactDTO Adapter = new(changedContact);
            _AddressBook.Update(Adapter);
            _AddressBook.Save();
        }
    }
}