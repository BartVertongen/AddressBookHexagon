//By Bart Vertongen copyright 2021.

using System.Collections.Generic;
using PS.AddressBook.Business.Interfaces;


namespace PS.AddressBook.Business
{
    public interface IAddressBookService
    {
        IList<IContactLineDTO> GetOverview(string filter);

        IContactDTO Get(string name);

        void  Add(IContactDTO newContact);

        void Delete(string name);

        void Update(IContactDTO changedContact);
    }
}