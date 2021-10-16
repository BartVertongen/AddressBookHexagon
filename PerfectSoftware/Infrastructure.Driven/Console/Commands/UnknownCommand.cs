//By Bart Vertongen copyright 2021.




namespace PS.AddressBook.Framework.Console.Commands
{
    internal class UnknownCommand : IUICommand
    {
        private readonly IConsoleUserInterface _UserInterface;

        public UnknownCommand(IConsoleUserInterface ui)
        {
            _UserInterface = ui;
        }

        public string ShortName { get; } = "unk";

        public string Name { get; } = "unknown";

        public string Description { get; } = "This is a Dummy Command.";

        public (bool WasSuccessful, bool IsTerminating) Run(out object result, string argument = "")
        {
            _UserInterface.WriteError("Unable to determine the desired command.");
            result = null;
            return (false, false);
        }
    }
}