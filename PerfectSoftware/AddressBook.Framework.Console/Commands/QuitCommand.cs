// By Bart Vertongen copyright 2021.


namespace PS.AddressBook.Framework.Console.Commands
{
    public class QuitCommand : IUICommand
    {
        private readonly IConsoleUserInterface _UserInterface;

        public QuitCommand(IConsoleUserInterface ui)
        {
            _UserInterface = ui;
        }

        public string ShortName { get; } = "q";

        public string Name { get; } = "quit";

        public string Description { get; } = "Stops the AddressBook Application.";

        public (bool WasSuccessful, bool IsTerminating) Run(out object result, string argument="")
        {
            _UserInterface.WriteMessage("Thanks for using the AddressBook Application.");
            result = null;
            return (true, true);
        }
    }
}