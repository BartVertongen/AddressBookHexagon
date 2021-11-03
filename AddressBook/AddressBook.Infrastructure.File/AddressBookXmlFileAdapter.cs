// By Bart Vertongen copyright 2021.

using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.Ports.Out;


namespace PS.AddressBook.Infrastructure.File
{
    public class AddressBookXmlFileAdapter : IAddressBookFile
    {
        private const string ErrFullFilenameNeeded = "AddressBookXmlFileAdapter needs a Full Filename of an existing xml-file.";

        public string FullPath { get; private set; }

        public AddressBookXmlFileAdapter(IConfiguration config)
        {
            FullPath = config.GetSection("ContactsFile").Value;
        }

        public void Load(IAddressBookDTO book)
        {
            XmlSerializer AddressBookSerializer;
            //AddressBookDTO TempBook;

            //Check if a Full File Name is given.
            if (string.IsNullOrEmpty(this.FullPath))
            {
                throw new InvalidDataException(ErrFullFilenameNeeded);
            }
            (book as IList<IContactDTO>).Clear();
            //Check if the File exists
            if (System.IO.File.Exists(this.FullPath))
            {
                AddressBookSerializer = new XmlSerializer(typeof(AddressBookDTO), new XmlRootAttribute("AddressBook"));
                using (FileStream fs = new(this.FullPath, FileMode.Open, FileAccess.Read))
                {
                    book = AddressBookSerializer.Deserialize(fs) as AddressBookDTO;
                }
            }
        }

        public void Save(IAddressBookDTO book)
        {
            XmlSerializer AddressBookSerializer;
            //AddressBookDTO TempBook = new();

            if (string.IsNullOrEmpty(this.FullPath))
            {
                throw new InvalidDataException(ErrFullFilenameNeeded);
            }
            if (System.IO.File.Exists(this.FullPath)) System.IO.File.Delete(this.FullPath);

            /*foreach (IContactDTO ContactSource in book as List<ContactDTO>)
            {
                ContactDTO dtoContact = new();
                AddressDTO dtoAddress = new();

                dtoContact.Name = ContactSource.Name;
                dtoAddress.Street = ContactSource.Address.Street;
                dtoAddress.PostalCode = ContactSource.Address.PostalCode;
                dtoAddress.Town = ContactSource.Address.Town;
                dtoContact.Address = dtoAddress;
                dtoContact.Phone = ContactSource.Phone;
                dtoContact.Email = ContactSource.Email;
                TempBook.Add(dtoContact);
            }*/

            AddressBookSerializer = new XmlSerializer(typeof(AddressBookDTO), new XmlRootAttribute("AddressBook"));
            using FileStream fs = new(this.FullPath, FileMode.Create, FileAccess.Write);
            AddressBookSerializer.Serialize(fs, book);
        }
    }
}