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
        private readonly Contact _Contact;

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


        public (bool WasSuccessful, bool IsTerminating) Run(string argument="")
        {

            try
            {
                if (_Contact.IsValid())
                {
                    _AddressBook.Update(_Contact);
                    _AddressBook.Save();
                    _UserInterface.WriteMessage($"The Contact with Name '{_Contact.Name}' is updated.");
                    return (true, false);
                }
                else
                {
                    string Line;

                    if (!_Contact.Address.IsValid())
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

                Line = $"An Error Occurred in UpdateContact Command with ContactName={_Contact.Name}.";
                _UserInterface.WriteError(Line);
                _UserInterface.WriteError("The error description is " + ex.Message);
                return (false, false);
            }
        }
    }
}