// By Bart Vertongen copyright 2021.

using System.Collections.Generic;


namespace PS.AddressBook.Business.Interfaces
{
    public interface ICommandResponse
    {
        bool WasSuccessful { get; }

        IList<string> Errors { get; }
    }
}