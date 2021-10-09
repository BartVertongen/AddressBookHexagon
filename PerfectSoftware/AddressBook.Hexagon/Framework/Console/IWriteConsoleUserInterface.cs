//By Bart Vertongen copyright 2021


namespace PS.AddressBook.Hexagon.Framework.Console
{
    public interface IWriteConsoleUserInterface
    {
        void WriteMessage(string message);

        void WriteWarning(string message);

        void WriteError(string message);
    }
}