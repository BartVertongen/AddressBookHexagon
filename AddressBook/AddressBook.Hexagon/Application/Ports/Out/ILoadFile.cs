

namespace PS.AddressBook.Hexagon.Application.Ports.Out
{
    public interface ILoadFile
    {
        void Load(ref IAddressBookDTO book);
    }
}