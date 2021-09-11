// By Bart Vertongen copyright 2021.

using PS.AddressBook.Data.Interfaces;
using System.Collections.Generic;


namespace PS.AddressBook.Business.Interfaces
{
    public interface IQueryCommandResponse: ICommandResponse
    {
        IList<IContactLineDTO> Result { get; }
    }
}