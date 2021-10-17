// By Bart Vertongen copyright 2021.


namespace PS.AddressBook.Framework.Console
{
    public interface IUICommand
    {
        string ShortName { get; }

        string Name { get; }

        string Description { get; }

        (bool WasSuccessful, bool IsTerminating) Run(out object result, string argument = "");
    }
}