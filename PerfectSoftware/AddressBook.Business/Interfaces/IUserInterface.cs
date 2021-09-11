//By Bart Vertongen copyright 2021


namespace PS.AddressBook.Business.Interfaces
{
    public interface IUserInterface
    {
        string ReadValue(string message);

        void WriteMessage(string message);

        void WriteWarning(string message);

        void WriteError(string message);
    }
}