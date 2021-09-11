//By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Business.Interfaces;


namespace PS.AdressBook.UI
{
    internal class ConsoleUserInterface : IUserInterface
    {
        public string ReadValue(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(message);
            return Console.ReadLine();
        }

        public void WriteMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
        }

        public void WriteWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(message);
        }

        public void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }
    }
}
