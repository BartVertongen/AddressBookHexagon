// By Bart Vertongen.

using System.Linq;
using System.Collections.Generic;


namespace PS.AddressBook.Hexagon.Domain
{
    public interface IAddressBook
    {
        string SelectedContactName { get; set; }

        IList<IContactLineDTO> GetOverview(string filter);

        IContact GetContact(string nameToFind);

        void Delete(string nameToDelete);

        void Update(IContact changedContact);

        bool ContainsName(string name);

        public void Add(IContact item);
    }
}
