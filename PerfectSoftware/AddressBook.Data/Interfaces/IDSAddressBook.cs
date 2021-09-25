// By Bart Vertongen copyright 2021.

using System.Collections.Generic;
using PS.AddressBook.Data.Interfaces;


namespace PS.AddressBook.Data.Interfaces
{
    public interface IDSAddressBook
    {
        public string FullPath { get; }

        public void Save(IList<IContactDTO> book);

        public void Load(IList<IContactDTO> book);
    }
}