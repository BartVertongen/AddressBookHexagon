﻿// By Bart Vertongen.

using System.Collections.Generic;


namespace PS.AddressBook.Data.Interfaces
{
    public interface IAddressBook: IList<IContact>
    {
        //IList<IContactLine> GetOverview(string filter);

        new void Add(IContact newContact);

        IContact GetContact(string nameToFind);

        void Delete(string nameToDelete);

        void Update(IContact changedContact);

        bool ContainsName(string name);
    }
}
