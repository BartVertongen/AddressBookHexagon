//Copyright 2021 Bart Vertongen

using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace PS.AddressBook.Hexagon.Domain
{
    public class AddressBook : List<IContact>, IAddressBook
    {
        private readonly IAddressBookFile _DSAddressBook;

        public AddressBook(IAddressBookFile dsAddressBook)
        {
            _DSAddressBook = dsAddressBook;
            this.Load();
            SelectedContactName = "";
        }

        public AddressBook(IAddressBookFile dsAddressBook, IAddressBookDTO dtoAddressBook)
        {
            _DSAddressBook = dsAddressBook;
            SelectedContactName = "";
            foreach (IContactDTO dtoContact in dtoAddressBook)
            {
                IContact ContactAdapter = new AdapterFromContactDTO(dtoContact);
                Contact bussContact = new(ContactAdapter);
                this.Add(bussContact);
            }
        }

        public string SelectedContactName { get; set; }

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
            else if (filter[0] == '*' && filter[^1] == '*')
            {
                PureFilter = filter[1..^1]; //^1 = length - i
                Selection = this.Where(ctt => ctt.Name.ToUpper().Contains(PureFilter.ToUpper())).OrderBy(ctt => ctt.Name).ToList();
            }
            else
            {
                Selection = this.Where(ctt => ctt.Name.ToUpper().StartsWith(filter.ToUpper())).OrderBy(ctt => ctt.Name).ToList();
            }
            foreach(IContact oContact in Selection)
            {
                Contact CurrContact = new(oContact);
                IContactLineDTO oContactLine = CurrContact.ContactLine;
                oContactLine.Id = ++ID;
                Result.Add(oContactLine);
            }
            return Result;
        }

        /// <summary>
        /// Adds a new Contact to the AddressBook in memory.
        /// </summary>
        /// <param name="newContact"></param>
        new public void Add(IContact newContact)
        {
            bool bIsValid, bIsNew;

            bIsValid = Contact.IsValid(newContact);
            bIsNew = !this.ContainsName(newContact.Name);
            if (bIsValid && bIsNew)
                base.Add(newContact);
            else if (!bIsNew)
                throw new InvalidDataException($"You tried to Add an existing Contact '{newContact.Name}' to the AddressBook!");
            else
                throw new InvalidDataException($"You tried to Add an invalid Contact '{newContact.Name}' to an AddressBook!");
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
            var Result = this.SingleOrDefault(ctt => ctt.Name == nameToFind);
            if (Result == null)
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
            IContact oContact = this.Single(ctt => ctt.Name == nameToDelete);
            Contact bussContact = new(oContact);
            //this does not work
            base.Remove(bussContact);
        }

        /// <summary>
        /// Replaces the old Contact data with new Contact Data in the AddressBook.
        /// </summary>
        /// <param name="changedContact"></param>
        public void Update(IContact changedContact)
        {
            IContact Found = (Contact)this.FirstOrDefault(ctt => ctt.Name == changedContact.Name);
            if (Found != null)
            {
                Found.Assign(changedContact);
            }
        }

        /// <summary>
        /// Loads the AddresBook with the Contacts from an Xml File.
        /// </summary>
        public void Load()
        {
            IAddressBookDTO TempBook = new AddressBookDTO();

            _DSAddressBook.Load(TempBook);
            this.Clear();
            foreach(IContactDTO dtoContact in TempBook)
            {
                IContact ContactAdapter = new AdapterFromContactDTO(dtoContact);
                Contact bussContact = new(ContactAdapter);
                this.Add(bussContact);
            }
        }

        /// <summary>
        /// Writes the Contacts in the AddresBook in memory to an Xml File.
        /// </summary>
        public void Save()
        {
            AddressBookDTO TempBook = new();

            foreach (IContact bussContact in this)
            {
                AdapterToContactDTO Adapter = new(bussContact);
                IContactDTO dtoContact = Adapter;
                TempBook.Add(dtoContact);
            }
            _DSAddressBook.Save(TempBook);
        }
    }
}