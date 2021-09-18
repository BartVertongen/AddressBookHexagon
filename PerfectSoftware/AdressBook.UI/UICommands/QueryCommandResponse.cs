// By Bart Vertongen copyright 2021.

using System.Collections.Generic;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.Data.Interfaces;


namespace PS.AddressBook.Business.Commands
{
    public class QueryCommandResponse : IQueryCommandResponse
    {
        public IList<IContactLineDTO> Result { get; set; }

        public bool WasSuccessful { get; set; } = false;

        public IList<string> Errors { get; set; }

        public bool IsTerminating { get; private set; } = false;
    }
}