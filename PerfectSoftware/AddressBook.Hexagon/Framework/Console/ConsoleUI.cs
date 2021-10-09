//By Bart Vertongen copyright 2021.


namespace PS.AddressBook.Hexagon.Framework.Console
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
                System.ConsoleColor OldColor = System.Console.ForegroundColor;
                System.Console.ForegroundColor = System.ConsoleColor.Green;
                System.Console.Write(message);
                System.Console.ForegroundColor = OldColor;
                return System.Console.ReadLine();
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
                System.ConsoleColor OldColor = System.Console.ForegroundColor;
                System.Console.ForegroundColor = System.ConsoleColor.Green;
                System.Console.WriteLine(message);
                System.Console.ForegroundColor = OldColor;
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
                System.ConsoleColor OldColor = System.Console.ForegroundColor;
                System.Console.ForegroundColor = System.ConsoleColor.DarkYellow;
                System.Console.WriteLine(message);
                System.Console.ForegroundColor = OldColor;
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
                System.ConsoleColor OldColor = System.Console.ForegroundColor;
                System.Console.ForegroundColor = System.ConsoleColor.Red;
                System.Console.WriteLine(message);
                System.Console.ForegroundColor = OldColor;
            }
            else
                _Console.WriteLine("ERROR: " + message);
        }
    }
}