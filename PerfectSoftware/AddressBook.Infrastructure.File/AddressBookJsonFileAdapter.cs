//By Bart Vertongen copyright 2021.

using System.IO;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application;
using System.Collections.Generic;

namespace PS.AddressBook.Infrastructure.Driven.File
{
    public class AddressBookJsonFileAdapter : IAddressBookFile
    {
        public string FullPath { get; private set; }

        public AddressBookJsonFileAdapter(IConfigurationRoot config)
        {
            FullPath = config.GetSection("ContactsFile").Value;
        }

        public void Load(IList<IContactDTO> book)
        {
            XmlSerializer AddressBookSerializer;
            IList<IContactDTO> TempBook;

            //Check if a Full File Name is given.
            if (string.IsNullOrEmpty(this.FullPath))
            {
                throw new InvalidDataException("DSAddressBook needs a Full Filename of an existing xml-file.");
            }
            //Check if the File exists
            if (System.IO.File.Exists(this.FullPath))
            {
                AddressBookSerializer = new XmlSerializer(typeof(AddressBookDTO), new XmlRootAttribute("AddressBook"));
                using (FileStream fs = new(this.FullPath, FileMode.Open, FileAccess.Read))
                {
                    TempBook = AddressBookSerializer.Deserialize(fs) as IList<IContactDTO>;
                }
                book.Clear();
                foreach (IContactDTO aContact in TempBook)
                {
                    book.Add(aContact);
                }
            }
        }

        public void Save(IList<IContactDTO> book)
        {
            XmlSerializer AddressBookSerializer;
            AddressBookDTO TempBook = new();

            if (string.IsNullOrEmpty(this.FullPath))
            {
                throw new InvalidDataException("DSAddressBook needs a Full Filename of an existing xml-file.");
            }
            if (System.IO.File.Exists(this.FullPath)) System.IO.File.Delete(this.FullPath);

            foreach (IContactDTO ContactSource in book)
            {
                ContactDTO dtoContact = new();
                AddressDTO dtoAddress = new();

                dtoContact.Name = ContactSource.Name;
                dtoAddress.Street = ContactSource.Address.Street;
                dtoAddress.PostalCode = ContactSource.Address.PostalCode;
                dtoAddress.Town = ContactSource.Address.Town;
                dtoContact.Address = dtoAddress;
                dtoContact.PhoneNumber = ContactSource.PhoneNumber;
                dtoContact.Email = ContactSource.Email;
                TempBook.Add(dtoContact);
            }

            AddressBookSerializer = new XmlSerializer(typeof(AddressBookDTO), new XmlRootAttribute("AddressBook"));
            using FileStream fs = new(this.FullPath, FileMode.Create, FileAccess.Write);
            AddressBookSerializer.Serialize(fs, TempBook);
        }
    }
}