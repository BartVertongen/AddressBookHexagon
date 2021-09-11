// By Bart Vertongen copyright 2021.

using System.Collections.Generic;
using PS.AddressBook.Business.Interfaces;


namespace PS.AddressBook.Business.Commands
{
    public class ChangeCommandResponse : IChangeCommandResponse
    {

        public IList<string> Result { get; set; }

        public bool WasSuccessful { get; set; } = false;

        public IList<string> Errors { get; set; }
       
    }
}