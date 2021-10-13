// By Bart Vertongen.

using System.Collections.Generic;


namespace PS.AddressBook.Hexagon.Domain
{
    public interface IAddressBook: IList<IContact>
    {
        string SelectedContactName { get; set; }

        IList<IContactLineDTO> GetOverview(string filter);

        new void Add(IContact newContact);

        IContact GetContact(string nameToFind);

        void Delete(string nameToDelete);

        void Update(IContact changedContact);

        bool ContainsName(string name);

        void Load();

        void Save();
    }
}
