//By Bart Vertongen copyright 2021

namespace PS.AddressBook.Hexagon.Application.Commands
{
    public class DeleteContactCommand
    {
        private readonly string _Name;

        public DeleteContactCommand(string name)
        {
            //TODO: validate Name, it cannot be empty
            _Name = name;
        }

        public string Name => _Name;
    }
}