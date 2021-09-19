//By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Business.Interfaces;


namespace PS.AddressBook.UI
{
    public class ConsoleUserInterface : IConsoleUserInterface
    {
        private readonly IConsole _Console;

        public ConsoleUserInterface(IConsole console = null)
        {
            _Console = console;
        }

        public string ReadValue(string message)
        {          
            if (_Console == null)
            {
                ConsoleColor OldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(message);
                Console.ForegroundColor = OldColor;
                return Console.ReadLine();
            }
            else
            {
                _Console.Write("INPUT: " + message);
                return _Console.ReadLine();
            }
        }

        public void WriteMessage(string message)
        {
            if (_Console == null)
            {
                ConsoleColor OldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.ForegroundColor = OldColor;
            }
            else
            {
                _Console.WriteLine(message);
            }
        }

        public void WriteWarning(string message)
        {
            if (_Console == null)
            {
                ConsoleColor OldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(message);
                Console.ForegroundColor = OldColor;
            }
            else
            {
                _Console.WriteLine("WARNING: " + message);
            }
        }

        public void WriteError(string message)
        {
            if (_Console == null)
            {
                ConsoleColor OldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ForegroundColor = OldColor;
            }
            else
                Console.WriteLine("ERROR: " + message);
        }
    }
}