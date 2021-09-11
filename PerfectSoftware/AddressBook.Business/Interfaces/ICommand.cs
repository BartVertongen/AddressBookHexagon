// By Bart Vertongen copyright 2021.


namespace PS.AddressBook.Business.Interfaces
{
    public interface ICommand
    {
        string ShortName { get; }

        string Name { get; }

        string Description { get; }
    }
}