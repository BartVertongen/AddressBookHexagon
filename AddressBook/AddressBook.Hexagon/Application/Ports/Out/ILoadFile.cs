


using System.Collections.Generic;


namespace PS.AddressBook.Hexagon.Application.Ports.Out
{
    public interface ILoadFile
    {
        void Load(IList<IContactDTO> book);
    }
}