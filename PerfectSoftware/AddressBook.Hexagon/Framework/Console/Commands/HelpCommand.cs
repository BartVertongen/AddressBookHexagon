//By Bart Vertongen copyright 2021.

using System;


namespace PS.AddressBook.Hexagon.Framework.Console.Commands
{
    public class HelpCommand : IChangeCommand
    {
        private readonly IConsoleUserInterface _UserInterface;

        public string ShortName { get; } = "?";

        public string Name { get; } = "help";

        public string Description { get; } = "Gives info about this AddressBook Application.";

        public HelpCommand(IConsoleUserInterface ui)
        {
            _UserInterface = ui;
        }

        public (bool WasSuccessful, bool IsTerminating) Run(string argument = "")
        {
            try

            {
                if (string.IsNullOrEmpty(argument))
                {
                    _UserInterface.WriteMessage("USAGE:");
                    _UserInterface.WriteMessage("\ta\tadd\tAdds a Contact to the Address Book.");
                    _UserInterface.WriteMessage("\td\tdelete\tDeletes a Contact to the Address Book.");
                    _UserInterface.WriteMessage("\tl\tlist\tGives an overview of Contacts in the AddressBook.");
                    _UserInterface.WriteMessage("\tu\tupdate\tChanges an existing Contact of the Address Book.");
                    _UserInterface.WriteMessage("\tq\tquit\tStops the Address Book Application.");
                    _UserInterface.WriteMessage("\t?\thelp\tGives more info about a command.");
                    _UserInterface.WriteMessage("\tExamples:");
                    _UserInterface.WriteMessage("\t\t? d");
                    return (true, false);
                }
                else
                {
                    //Get the Command and help Info from that Command.
                    return (true, false);
                }


            }
            catch (Exception ex)
            {
                _UserInterface.WriteError("An Error Occurred in Help Command.");
                _UserInterface.WriteError("The error description is " + ex.Message);
                return (false, false);
            }
        }
    }
}