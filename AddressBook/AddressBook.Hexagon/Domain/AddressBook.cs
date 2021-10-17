//Copyright 2021 Bart Vertongen


using System.Collections.Generic;
using System.IO;
using System.Linq;
using PS.AddressBook.Hexagon.Domain.Ports;


namespace PS.AddressBook.Hexagon.Domain
{
    public class AddressBook : List<IContact>, IAddressBook
    {
        public AddressBook()
        {
            SelectedContactName = "";
        }


        public string SelectedContactName { get; set; }

        public bool IsReadOnly => throw new System.NotImplementedException();


        public IList<IContact> GetOverview(string filter)
        {
            List<IContact> Selection = new();

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
            return Selection;
        }

        /// <summary>
        /// Adds a new Contact to the AddressBook in memory.
        /// </summary>
        /// <param name="newContact"></param>
        public new void Add(IContact newContact)
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
    }
}