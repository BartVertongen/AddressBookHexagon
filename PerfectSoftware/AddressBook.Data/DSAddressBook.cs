// By Bart Vertongen copyright 2021.

using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using PS.AddressBook.Data.Interfaces;


namespace PS.AddressBook.Data
{
    public class DSAddressBook
    {
        public string FullPath { get; set; }


        public void Save(IList<IContactDTO> book)
        {
            XmlSerializer AddressBookSerializer;
            AddressBookDTO TempBook = new AddressBookDTO();

            if (string.IsNullOrEmpty(this.FullPath))
            {
                throw new InvalidDataException("DSAddressBook needs a Full Filename of an existing xml-file.");
            }
            if (File.Exists(this.FullPath)) File.Delete(this.FullPath);

            foreach (IContactDTO ContactSource in book)
            {
                ContactDTO dtoContact = new ContactDTO();
                AddressDTO dtoAddress = new AddressDTO();

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
            using (FileStream fs = new FileStream(this.FullPath, FileMode.Create, FileAccess.Write))
            {
                AddressBookSerializer.Serialize(fs, TempBook);
            }
        }


        public void Load(IList<IContactDTO> book)
        {
            XmlSerializer AddressBookSerializer;
            AddressBookDTO TempBook;

            if (string.IsNullOrEmpty(this.FullPath))
            {
                throw new InvalidDataException("DSAddressBook needs a Full Filename of an existing xml-file.");
            }         
            AddressBookSerializer = new XmlSerializer(typeof(AddressBookDTO), new XmlRootAttribute("AddressBook"));
            using (FileStream fs = new FileStream(this.FullPath, FileMode.Open, FileAccess.Read))
            {
                TempBook = AddressBookSerializer.Deserialize(fs) as AddressBookDTO;
            }
            book.Clear();
            foreach( IContactDTO aContact in TempBook)
            {
                book.Add(aContact);
            }
        }
    }
}