// By Bart Vertongen copyright 2021.

using System.Collections.Generic;


namespace PS.AddressBook.Hexagon.Application
{
    public interface ICommandResponse
    {
        bool WasSuccessful { get; }

        /// <summary>
        /// Is this a termination command ?
        /// </summary>
        bool IsTerminating { get; }

        IList<string> Errors { get; }
    }
}