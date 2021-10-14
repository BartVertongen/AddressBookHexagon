using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.AddressBook.Hexagon.Application.Ports.Out
{
    public interface IAddressBookFile: ILoadFile, ISaveFile
    {
        string FullPath { get; }

    }
}
