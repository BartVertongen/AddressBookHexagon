// By Bart Vertongen copyright 2021.

using System;
using System.Collections.Generic;
using PS.AddressBook.Data.Interfaces;
using PS.AddressBook.Business.Interfaces;


namespace PS.AddressBook.Business.Commands
{
    public class AddContactCommand : IChangeCommand
    {
        private readonly AddressBook _AddressBook;
        private readonly Contact _Contact;

        /// <summary>
        /// The Command to create and add a Contact.
        /// </summary>
        /// <param name="book"></param>
        /// <param name="newContact"></param>
        public AddContactCommand(IAddressBook book, Contact newContact = null)
        {
            _AddressBook = (AddressBook)book;
            _Contact = newContact;
        }

        public string ShortName { get; } = "a";

        public string Name { get; } = "add";

        public string Description { get; } = "Adds a new Contact to the AddressBook.";


        public IChangeCommandResponse Run()
        {
            ChangeCommandResponse oResponse = new ChangeCommandResponse();

            try
            {
                if (_Contact.IsValid())
                {
                    _AddressBook.Add(_Contact);
                    _AddressBook.Save();
                    oResponse.WasSuccessful = true;
                    oResponse.Errors = null;
                    oResponse.Result = new List<string>();
                    oResponse.Result.Add($"The Contact with Name '{_Contact.Name}' is added.");
                }
                else
                {
                    string Line;

                    oResponse.WasSuccessful = false;
                    oResponse.Result = null;
                    oResponse.Errors = new List<string>();
                    Line = $"The Contact could not be added because it was not valid!";
                }
            }
            catch (Exception ex)
            {
                string Line;

                oResponse.WasSuccessful = false;
                oResponse.Result = null;
                oResponse.Errors = new List<string>();
                Line = $"An Error Occurred in AddContact Command with argument ContactName={_Contact.Name}.";
                oResponse.Errors.Add(Line);
                Line = "The error description is " + ex.Message;
                oResponse.Errors.Add(Line);
            }

            return oResponse;
        }
    }
}