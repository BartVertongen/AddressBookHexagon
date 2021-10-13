// By Bart Vertongen copyright 2021.


namespace PS.AddressBook.Hexagon.Domain
{
    public interface IAddressBookFile
    {
        public string FullPath { get; }

        public void Save(IAddressBookDTO book);

        public void Load(IAddressBookDTO book);
    }
}