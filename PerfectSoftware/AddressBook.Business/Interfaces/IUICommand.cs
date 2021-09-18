// By Bart Vertongen copyright 2021.


namespace PS.AddressBook.Business.Interfaces
{
    public interface IUICommand
    {
        string ShortName { get; }

        string Name { get; }

        string Description { get; }

        (bool WasSuccessful, bool IsTerminating) Run(string argument = "");
    }
}