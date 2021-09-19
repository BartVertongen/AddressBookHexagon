// By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Data.Interfaces;
using PS.AddressBook.Business;
using PS.AddressBook.Business.Interfaces;
using BussAddressBook = PS.AddressBook.Business.AddressBook;


namespace PS.AddressBook.UI.Commands
{
    public class UpdateContactCommand : IChangeCommand
    {
        private readonly BussAddressBook _AddressBook;
        private readonly IConsoleUserInterface _UserInterface;
        private readonly IAddressBookUICommandFactory _CommandFactory;
        private Contact _UpdatedContact, _OriginalContact;

        /// <summary>
        /// The Command to create and add a Contact.
        /// </summary>
        /// <param name="book"></param>
        /// <param name="newContact"></param>
        public UpdateContactCommand(IAddressBook book, IConsoleUserInterface ui, IAddressBookUICommandFactory commandFactory)
        {
            _AddressBook = (BussAddressBook)book;
            _UserInterface = ui;
            _CommandFactory = commandFactory;
        }

        public string ShortName { get; } = "u";

        public string Name { get; } = "update";

        public string Description { get; } = "Update an existing Contact from the AddressBook.";

        private void GetUpdatedContact()
        {
            _UpdatedContact = new Contact(_AddressBook);
            _UpdatedContact.Name = _OriginalContact.Name;

            //Street
            _UserInterface.WriteMessage($"The current value for the street and number is {_OriginalContact.Address.Street}.");
            _UpdatedContact.Address.Street = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (_UpdatedContact.Address.Street.ToUpper() == "XX")
                _UpdatedContact.Address.Street = _OriginalContact.Address.Street;

            //Postal Code.
            _UserInterface.WriteMessage($"The current value for the postal code is {_OriginalContact.Address.PostalCode}.");
            _UpdatedContact.Address.PostalCode = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (_UpdatedContact.Address.PostalCode.ToUpper() == "XX")
                _UpdatedContact.Address.PostalCode = _OriginalContact.Address.PostalCode;

            //Town
            _UserInterface.WriteMessage($"The current value for the town is {_OriginalContact.Address.Town}.");
            _UpdatedContact.Address.Town = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (_UpdatedContact.Address.Town.ToUpper() == "XX")
                _UpdatedContact.Address.Town = _OriginalContact.Address.Town;

            //Phone
            _UserInterface.WriteMessage($"The current value for the phone number is {_OriginalContact.PhoneNumber}.");
            _UpdatedContact.PhoneNumber = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (_UpdatedContact.PhoneNumber.ToUpper() == "XX")
                _UpdatedContact.PhoneNumber = _OriginalContact.PhoneNumber;

            //Email
            _UserInterface.WriteMessage($"The current value for the email is {_OriginalContact.Email}.");
            _UpdatedContact.Email = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (_UpdatedContact.Email.ToUpper() == "XX")
                _UpdatedContact.Email = _OriginalContact.Email;
        }

        public (bool WasSuccessful, bool IsTerminating) Run(string argument="")
        {

            try
            {
                //Select an existing Contact
                IUICommand SelectCommand = _CommandFactory.GetCommand("s");
                SelectCommand.Run();

                //Get the original selected Contact
                _OriginalContact = (Contact)_AddressBook.GetContact(_AddressBook.SelectedContactName);
                //Get the new values
                this.GetUpdatedContact();
                if (_UpdatedContact.IsValid())
                {
                    _AddressBook.Update(_UpdatedContact);
                    _AddressBook.Save();
                    _UserInterface.WriteMessage($"The Contact with Name '{_UpdatedContact.Name}' is updated.");
                    return (true, false);
                }
                else
                {
                    string Line;

                    if (!_UpdatedContact.Address.IsValid())
                        Line = $"The Contact could not be updated because the Address was not valid!";
                    else
                        Line = $"The Contact could not be updated because the new values were not valid!";
                    _UserInterface.WriteError(Line);
                    return (false, false);
                }
            }
            catch (Exception ex)
            {
                string Line;

                Line = $"An Error Occurred in UpdateContact Command with ContactName={_UpdatedContact.Name}.";
                _UserInterface.WriteError(Line);
                _UserInterface.WriteError("The error description is " + ex.Message);
                return (false, false);
            }
        }
    }
}