//Copyright 2021 Bart Vertongen


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using PS.AddressBook.Business.Adapters;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.Data;
using PS.AddressBook.Data.Interfaces;


namespace PS.AddressBook.Business
{
    public class AddressBook : List<IContact>, IAddressBook
    {
        public string XmlFile;
        public string SelectedContactName;
        private readonly IConfigurationRoot _Configuration;

        public AddressBook(IConfigurationRoot config=null)
        {
            _Configuration = config;
            if (_Configuration == null)
                XmlFile = "AddresBook.xml";
            else
                XmlFile = _Configuration.GetSection("ContactsFile").Value;
            this.Load();
            SelectedContactName = "";
        }

        public IList<IContactLineDTO> GetOverview(string filter)
        {
            IList<IContactLineDTO> Result = new List<IContactLineDTO>();
            List<IContact> Selection = new();
            int ID = 0;

            string PureFilter;


            if (string.IsNullOrEmpty(filter))
            {
                Selection = this.OrderBy(ctt => ctt.Name).ToList();
            }
            else if (filter[0] == '*' && filter[filter.Length-1] == '*')
            {
                PureFilter = filter.Substring(1, filter.Length - 2);
                Selection = this.Where(ctt => ctt.Name.ToUpper().Contains(PureFilter.ToUpper())).OrderBy(ctt => ctt.Name).ToList();
            }
            else
            {
                Selection = this.Where(ctt => ctt.Name.ToUpper().StartsWith(filter.ToUpper())).OrderBy(ctt => ctt.Name).ToList();
            }
            foreach(Contact oContact in Selection)
            {
                ContactLineDTO oContactLine = oContact.ContactLine;
                oContactLine.Id = ++ID;
                Result.Add(oContactLine);
            }
            return Result;
        }

        /// <summary>
        /// Adds a new Contact to the AddressBook in memory.
        /// </summary>
        /// <param name="newContact"></param>
        public void Add(Contact newContact)
        {
            if (newContact.IsValid() && !this.ContainsName(newContact.Name))
                base.Add(newContact);
            else
                throw new InvalidDataException("You tried to Add an invalid Contact to an AddressBook!");
        }

        /// <summary>
        /// Finds the Contact with the given name.
        /// </summary>
        /// <param name="nameToFind"></param>
        /// <returns>null if the Contact Name is not found.</returns>
        public IContact GetContact(string nameToFind)
        {
            return this.SingleOrDefault(ctt => ctt.Name == nameToFind);
        }

        public bool ContainsName(string nameToFind)
        {
            Contact oContact = (Contact)this.SingleOrDefault(ctt => ctt.Name == nameToFind);
            if (oContact == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Deletes an existing Contact from the AddressBook in memory.
        /// </summary>
        /// <param name="nameToDelete"></param>
        public void Delete(string nameToDelete)
        {
            Contact oContact = (Contact)this.Single(ctt => ctt.Name == nameToDelete);
            this.Remove(oContact);
        }

        /// <summary>
        /// Replaces the old Contact data with new Contact Data in the AddressBook.
        /// </summary>
        /// <param name="changedContact"></param>
        public void Update(Contact changedContact)
        {
            Contact Found = (Contact)this.FirstOrDefault(ctt => ctt.Name == changedContact.Name);
            if (Found != null) Found = changedContact;
        }

        /// <summary>
        /// Loads the AddresBook with the Contacts from an Xml File.
        /// </summary>
        public void Load()
        {
            string sXmlFile;
            DSAddressBook aDSAddressBook;
            List<IContactDTO> TempBook = new();
            
            sXmlFile = Environment.CurrentDirectory + "\\" + XmlFile;
            aDSAddressBook = new DSAddressBook();
            aDSAddressBook.FullPath = sXmlFile;
            aDSAddressBook.Load(TempBook);
            this.Clear();
            foreach(IContactDTO dtoContact in TempBook)
            {
                AdapterFromContactDTO ContactAdapter = new(dtoContact);
                Contact bussContact = new Contact(ContactAdapter);
                this.Add(bussContact);
            }
        }

        /// <summary>
        /// Writes the Contacts in the AddresBook in memory to an Xml File.
        /// </summary>
        public void Save()
        {
            string sXmlFile;
            DSAddressBook aDSAddressBook;
            List<IContactDTO> TempBook = new List<IContactDTO>();

            sXmlFile = Environment.CurrentDirectory + "\\" + XmlFile;
            aDSAddressBook = new DSAddressBook
            {
                FullPath = sXmlFile
            };
            foreach (IContact bussContact in this)
            {
                AdapterToContactDTO Adapter = new AdapterToContactDTO(bussContact);
                ContactDTO dtoContact = new ContactDTO(Adapter);
                TempBook.Add(dtoContact);
            }
            aDSAddressBook.Save(TempBook);
        }
    }
}