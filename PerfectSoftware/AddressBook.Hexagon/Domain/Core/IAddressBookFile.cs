// By Bart Vertongen copyright 2021.

using System.Collections.Generic;


namespace PS.AddressBook.Hexagon.Domain.Core
{
    public interface IAddressBookFile
    {
        public string FullPath { get; }

        public void Save(IList<IContactDTO> book);

        public void Load(IList<IContactDTO> book);
    }
}