//By Bart Vertongen copyright 2021.

using PS.AddressBook.Business.Interfaces;


namespace PS.AddressBook.UI.Commands
{
    internal class UnknownCommand : IChangeCommand
    {
        private readonly IConsoleUserInterface _UserInterface;

        public UnknownCommand(IConsoleUserInterface ui)
        {
            _UserInterface = ui;
        }

        public string ShortName { get; } = "unk";

        public string Name { get; } = "unknown";

        public string Description { get; } = "This is a Dummy Command.";

        public (bool WasSuccessful, bool IsTerminating) Run(string argument)
        {
            _UserInterface.WriteError("Unable to determine the desired command.");
            return (false, false);
        }
    }
}