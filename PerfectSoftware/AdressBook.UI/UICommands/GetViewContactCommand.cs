// By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Business.Interfaces;
using BussAddressBook = PS.AddressBook.Business.AddressBook;


namespace PS.AddressBook.UI.Commands
{
    public class ViewContactCommand : IQueryCommand
    {
        private readonly BussAddressBook _AddressBook;
        private readonly IConsoleUserInterface _UserInterface;

        public ViewContactCommand(IAddressBook book, IConsoleUserInterface ui)
        {
            _AddressBook = (BussAddressBook)book;
            _UserInterface = ui;
        }

        public string ShortName { get; } = "v";

        public string Name { get; } = "view";

        public string Description { get; } = "Shows the current Contact of the AddressBook.";

        private void ShowContact(IContact contact)
        {
            _UserInterface.WriteMessage("");
            _UserInterface.WriteMessage($"The currently selected contact is {contact.Name}");
            _UserInterface.WriteMessage($"\tStreet: {contact.Address.Street}");
            _UserInterface.WriteMessage($"\tPostalCode: {contact.Address.PostalCode}");
            _UserInterface.WriteMessage($"\tTown: {contact.Address.Town}");
            _UserInterface.WriteMessage($"\tPhone: {contact.PhoneNumber}");
            _UserInterface.WriteMessage($"\tEmail: {contact.Email}");
            _UserInterface.WriteMessage("");
        }

        public (bool WasSuccessful, bool IsTerminating) Run(string argument = "")
        {
            try
            {              
                if (!string.IsNullOrEmpty(_AddressBook.SelectedContactName))
                {
                    IContact CurrContact = _AddressBook.GetContact(_AddressBook.SelectedContactName);
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
                }                   
                return (true, false);
            }
            catch (Exception ex)
            {
                _UserInterface.WriteMessage("");
                _UserInterface.WriteError("An Error Occurred in ViewContactCommand.");
                _UserInterface.WriteError("The error description is " + ex.Message);
                _UserInterface.WriteMessage("");
                return (false, false);
            }
        }
    }
}