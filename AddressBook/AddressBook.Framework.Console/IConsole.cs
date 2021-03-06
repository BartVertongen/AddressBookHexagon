//By Bart Vertongen copyright 2021.


namespace PS.AddressBook.Framework.Console
{
    /// <summary>
    /// Used for Mocking the real Console.
    /// </summary>
    public interface IConsole
    {
        void Write(string output = "");

        void WriteLine(string output="");

        string ReadLine();
    }
}