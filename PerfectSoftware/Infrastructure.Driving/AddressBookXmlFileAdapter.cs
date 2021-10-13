// By Bart Vertongen copyright 2021.

using System;
//TODO we should use Application not Domain here
using PS.AddressBook.Hexagon.Domain;


namespace PS.AddressBook.Infrastructure.Driven.File
{
    public class AddressBookXmlFileAdapter : IAddressBookFile
    {
        public string FullPath => throw new NotImplementedException();

        public void Load(IAddressBookDTO book)
        {
            throw new NotImplementedException();
        }

        public void Save(IAddressBookDTO book)
        {
            throw new NotImplementedException();
        }
    }
}