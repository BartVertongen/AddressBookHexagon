﻿// By Bart Vertongen copyright 2021.

using System;
using System.Collections.Generic;
using PS.AddressBook.Hexagon.Domain.Core;


namespace PS.AddressBook.Infrastructure.Driven.File
{
    public class AddressBookXmlFileAdapter : IAddressBookFile
    {
        public string FullPath => throw new NotImplementedException();

        public void Load(IList<IContactDTO> book)
        {
            throw new NotImplementedException();
        }

        public void Save(IList<IContactDTO> book)
        {
            throw new NotImplementedException();
        }
    }
}