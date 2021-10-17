// By Bart Vertongen copyright 2021.


using System;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.UseCases;


namespace PS.AddressBook.Framework.Console.Commands
{
    /// <summary>
    /// Shows a Contact with the given Name.
    /// </summary>
    public class ViewContactCommand : IUICommand
    {
        private readonly IGetOverviewQuery _GetOverviewPort;
        private readonly IConsoleUserInterface _UserInterface;

        public ViewContactCommand(IGetOverviewQuery getOverviewPort, IConsoleUserInterface ui)
        {
            _GetOverviewPort = getOverviewPort;
            _UserInterface = ui;
        }

        public string ShortName { get; } = "v";

        public string Name { get; } = "view";

        public string Description { get; } = "Shows the current Contact of the AddressBook.";

        private void ShowContact(IContactDTO contact)
        {
            _UserInterface.WriteMessage("");
            _UserInterface.WriteMessage($"The currently selected contact is {contact.Name}");
            _UserInterface.WriteMessage($"\tStreet: {contact.Address.Street}");
            _UserInterface.WriteMessage($"\tPostalCode: {contact.Address.PostalCode}");
            _UserInterface.WriteMessage($"\tTown: {contact.Address.Town}");
            _UserInterface.WriteMessage($"\tPhone: {contact.Phone}");
            _UserInterface.WriteMessage($"\tEmail: {contact.Email}");
            _UserInterface.WriteMessage("");
        }

        public (bool WasSuccessful, bool IsTerminating) Run(out object result, string argument = "")
        {
            try
            {
                /*if (!string.IsNullOrEmpty(_AddressBook.SelectedContactName))
                {
                    IContactDTO CurrContact = _AddressBook.GetContact(_AddressBook.SelectedContactName);
                    if (CurrContact == null)
                    {
                        _UserInterface.WriteMessage("");
                        _UserInterface.WriteError($"There is no Contact with name {_AddressBook.SelectedContactName} is not found in the Address Book!");
                        _UserInterface.WriteMessage("");
                        return (true, false);
                    }
                    else
                        this.ShowContact(CurrContact);
                }
                else
                {
                    _UserInterface.WriteMessage("");
                    _UserInterface.WriteWarning("There is no Contact currently selected!");
                    _UserInterface.WriteMessage("");                  
                } */
                result = null;
                return (true, false);
            }
            catch (Exception ex)
            {
                _UserInterface.WriteMessage("");
                _UserInterface.WriteError("An Error Occurred in ViewContactCommand.");
                _UserInterface.WriteError("The error description is " + ex.Message);
                _UserInterface.WriteMessage("");
                result = null;
                return (false, false);
            }
        }
    }
}