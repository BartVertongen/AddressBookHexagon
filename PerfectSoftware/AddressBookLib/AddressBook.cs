//Copyright 2021 Bart Vertongen

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;


namespace AddressBookLib
{
    public class AddressBook: List<Contact>
    {
        public string XmlFile = "AddresBook.xml";

        public List<ContactLine> GetOverview(string filter)
        {
            List<ContactLine> Result = new List<ContactLine>();
            List<Contact> Selection = new List<Contact>();
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
                ContactLine oContactLine = oContact.ContactLine;
                oContactLine.Id = ++ID;
                Result.Add(oContactLine);
            }
            return Result;
        }

        /// <summary>
        /// Adds a new Contact to the AddressBook in memory.
        /// </summary>
        /// <param name="newContact"></param>
        public new void Add(Contact newContact)
        {
            base.Add(newContact);
        }

        /// <summary>
        /// Finds the Contact with the given name.
        /// </summary>
        /// <param name="nameToFind"></param>
        /// <returns>null if the Contact Name is not found.</returns>
        public Contact GetContact(string nameToFind)
        {
            return this.SingleOrDefault(ctt => ctt.Name == nameToFind);
        }

        public bool ContainsName(string nameToFind)
        {
            Contact oContact = this.SingleOrDefault(ctt => ctt.Name == nameToFind);
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
            Contact oContact = this.Single(ctt => ctt.Name == nameToDelete);
            this.Remove(oContact);
        }

        /// <summary>
        /// Replaces the old Contact data with new Contact Data in the AddressBook.
        /// </summary>
        /// <param name="changedContact"></param>
        public void Update(Contact changedContact)
        {
            Contact Found = this.FirstOrDefault(ctt => ctt.Name == changedContact.Name);
            if (Found != null) Found = changedContact;
        }

        /// <summary>
        /// Loads the AddresBook with the Contacts from an Xml File.
        /// </summary>
        public void Load()
        {
            string FullFileName = Environment.CurrentDirectory + "\\" + XmlFile;
            AddressBook LoadedBook; //CR: Not Good

            XmlSerializer AddressBookSerializer = new XmlSerializer(typeof(AddressBook), new XmlRootAttribute("AdressBook"));
            using (FileStream fs = new FileStream(FullFileName, FileMode.Open, FileAccess.Read))
            {
                
                LoadedBook = AddressBookSerializer.Deserialize(fs) as AddressBook;
            }
            this.Clear();
            //CR:Bad copy again ???
            foreach(Contact aContact in LoadedBook)
            {
                this.Add(aContact);
            }
        }

        /// <summary>
        /// Writes the Contacts in the AddresBook in memory to an Xml File.
        /// </summary>
        /// <remarks>The old version of the Xml File is deleted.</remarks>
        public void Save()
        {
            string FullFileName = Environment.CurrentDirectory + "\\" + XmlFile;

            if (File.Exists(FullFileName))
                File.Delete(FullFileName);
            XmlSerializer AddressBookSerializer = new XmlSerializer(typeof(AddressBook), new XmlRootAttribute("AdressBook"));
            using (FileStream fs = new FileStream(FullFileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                AddressBookSerializer.Serialize(fs, this);
            }
        }
    }
}