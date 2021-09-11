// By Bart Vertongen copyright 2021.

using System.Collections.Generic;


namespace PS.AddressBook.Business.Interfaces
{
    public interface IChangeCommandResponse: ICommandResponse
    {
        IList<string> Result { get; set; }
    }
}