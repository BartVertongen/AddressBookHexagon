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

        public void Load(ref IAddressBookDTO book)
        {
            XmlSerializer AddressBookSerializer;

            //Check if a Full File Name is given.
            if (string.IsNullOrEmpty(this.FullPath))
            {
                throw new InvalidDataException(ErrFullFilenameNeeded);
            }
            book.Clear();
            //Check if the File exists
            if (System.IO.File.Exists(this.FullPath))
            {
                AddressBookSerializer = new XmlSerializer(typeof(AddressBookDTO), new XmlRootAttribute("AddressBook"));
                using FileStream fs = new(this.FullPath, FileMode.Open, FileAccess.Read);
                book = AddressBookSerializer.Deserialize(fs) as AddressBookDTO;
            }
        }

        public void Save(IAddressBookDTO book)
        {
            XmlSerializer AddressBookSerializer;

            if (string.IsNullOrEmpty(this.FullPath))
            {
                throw new InvalidDataException(ErrFullFilenameNeeded);
            }
            if (System.IO.File.Exists(this.FullPath)) System.IO.File.Delete(this.FullPath);


            AddressBookSerializer = new XmlSerializer(typeof(AddressBookDTO), new XmlRootAttribute("AddressBook"));
            using FileStream fs = new(this.FullPath, FileMode.Create, FileAccess.Write);
            AddressBookSerializer.Serialize(fs, book);
        }
    }
}