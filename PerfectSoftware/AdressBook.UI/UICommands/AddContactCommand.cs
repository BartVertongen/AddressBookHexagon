// By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Data.Interfaces;   //TODO remove reference to DataLayer in App
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.Business;
using BussAddressBook = PS.AddressBook.Business.AddressBook;


namespace PS.AddressBook.UI.Commands
{
    public class AddContactCommand : IChangeCommand
    {
        private readonly BussAddressBook _AddressBook;       
        private readonly IConsoleUserInterface _UserInterface;
        private Contact _Contact;

        /// <summary>
        /// The Command to create and add a Contact.
        /// </summary>
        /// <param name="book"></param>
        /// <param name="ui"></param>
        public AddContactCommand(IAddressBook book, IConsoleUserInterface ui)
        {
            _AddressBook = (BussAddressBook)book;
            _UserInterface = ui;
            _Contact = new Contact(_AddressBook);
        }

        public string ShortName { get; } = "a";

        public string Name { get; } = "add";

        public string Description { get; } = "Adds a new Contact to the AddressBook.";

        /// <summary>
        /// Gets the needed info from the User and holds it in a Contact object.
        /// </summary>
        private void GetContactData()
        {
            _Contact.Name = _UserInterface.ReadValue("Give a name for the new Contact: ");
            _Contact.Address.Street = _UserInterface.ReadValue("Give a street and number for the Address of the new Contact: ");
            _Contact.Address.PostalCode = _UserInterface.ReadValue("Give a postal code for the Address of the new Contact: ");
            _Contact.Address.Town = _UserInterface.ReadValue("Give a town for the Address of the new Contact: ");
            _Contact.PhoneNumber = _UserInterface.ReadValue("Give a phone number for the new Contact: ");
            _Contact.Email = _UserInterface.ReadValue("Give an email for the new Contact: ");
        }

        public (bool WasSuccessful, bool IsTerminating) Run(string argument = "")
        {
            try
            {
                this.GetContactData();
                if (_Contact.IsValid())
                {
                    _AddressBook.Add(_Contact);
                    _AddressBook.Save();
                    _UserInterface.WriteMessage($"The Contact with Name '{_Contact.Name}' is added.");
                    return (true, false);
                }
                else
                {
                    string Line;

                    if (!_Contact.Address.IsValid())
                        Line = $"The Contact could not be added because the Address was not valid!";
                    else
                        Line = $"The Contact could not be added because it was not valid!";
                    _UserInterface.WriteError(Line);
                    return (false, false);
                }
            }
            catch (Exception ex)
            {
                string Line;

                Line = $"An Error Occurred in AddContact Command with argument ContactName={_Contact.Name}.";
                _UserInterface.WriteError(Line);
                _UserInterface.WriteError("The error description is " + ex.Message);
                return (false, false);
            }
        }
    }
}