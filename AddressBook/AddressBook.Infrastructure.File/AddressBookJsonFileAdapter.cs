//By Bart Vertongen copyright 2021.

using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PS.AddressBook.Hexagon.Application.Ports.Out;
using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Infrastructure.File
{
    public class AddressBookJsonFileAdapter : IAddressBookFile
    {
        private const string ErrFullFilenameNeeded = "AddressBookJsonFileAdapter needs a Full Filename of an existing json-file.";

        public string FullPath { get; private set; }

        public AddressBookJsonFileAdapter(IConfiguration config)
        {
            FullPath = config.GetSection("ContactsFile").Value;
        }

        public void Load(IList<IContactDTO> book)
        {
            /*XmlSerializer AddressBookSerializer;
            IList<IContactDTO> TempBook;

            //Check if a Full File Name is given.
            if (string.IsNullOrEmpty(this.FullPath))
            {
                throw new InvalidDataException(ErrFullFilenameNeeded);
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
            }*/
        }

        public void Save(IList<IContactDTO> book)
        {
            /*XmlSerializer AddressBookSerializer;
            AddressBookDTO TempBook = new();

            if (string.IsNullOrEmpty(this.FullPath))
            {
                throw new InvalidDataException(ErrFullFilenameNeeded);
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
                dtoContact.Phone = ContactSource.Phone;
                dtoContact.Email = ContactSource.Email;
                TempBook.Add(dtoContact);
            }

            AddressBookSerializer = new XmlSerializer(typeof(AddressBookDTO), new XmlRootAttribute("AddressBook"));
            using FileStream fs = new(this.FullPath, FileMode.Create, FileAccess.Write);
            AddressBookSerializer.Serialize(fs, TempBook);*/
        }
    }
}