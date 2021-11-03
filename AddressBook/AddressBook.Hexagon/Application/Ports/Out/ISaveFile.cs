//By Bart Vertongen copyright 2021

using System.Collections.Generic;


namespace PS.AddressBook.Hexagon.Application.Ports.Out
{
    public interface ISaveFile
    {
        void Save(IAddressBookDTO book);
    }
}