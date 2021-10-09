// By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Hexagon.Domain.Core;
using BussAddressBook = PS.AddressBook.Hexagon.Domain.AddressBook;


namespace PS.AddressBook.Hexagon.Framework.Console.Commands
{
    public class UpdateContactCommand : IChangeCommand
    {
        private readonly BussAddressBook _AddressBook;
        private readonly IConsoleUserInterface _UserInterface;
        private readonly IAddressBookUICommandFactory _CommandFactory;
        private IContact _Contact;

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
            string sNewStreet, sNewPostalCode, sNewTown, sNewPhone, sNewEmail;

            //Street
            _UserInterface.WriteMessage($"The current value for the street and number is {_Contact.Address.Street}.");
            sNewStreet = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (sNewStreet.ToUpper() != "XX")
                _Contact.Address.Street = sNewStreet;

            //Postal Code.
            _UserInterface.WriteMessage($"The current value for the postal code is {_Contact.Address.PostalCode}.");
            sNewPostalCode = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (sNewPostalCode.ToUpper() != "XX")
                _Contact.Address.PostalCode = sNewPostalCode;

            //Town
            _UserInterface.WriteMessage($"The current value for the town is {_Contact.Address.Town}.");
            sNewTown = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (sNewTown.ToUpper() != "XX")
                _Contact.Address.Town = sNewTown;

            //Phone
            _UserInterface.WriteMessage($"The current value for the phone number is {_Contact.PhoneNumber}.");
            sNewPhone = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (sNewPhone.ToUpper() != "XX")
                _Contact.PhoneNumber = sNewPhone;

            //Email
            _UserInterface.WriteMessage($"The current value for the email is {_Contact.Email}.");
            sNewEmail = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (sNewEmail.ToUpper() != "XX")
                _Contact.Email = sNewEmail;
        }

        public (bool WasSuccessful, bool IsTerminating) Run(string argument="")
        {

            try
            {
                //Select an existing Contact
                IUICommand SelectCommand = _CommandFactory.GetCommand("s");
                SelectCommand.Run();

                //Get the original selected Contact
                _Contact = _AddressBook.GetContact(_AddressBook.SelectedContactName);
                //Get the new values
                this.GetUpdatedContact();
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