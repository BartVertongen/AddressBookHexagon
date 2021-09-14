// By Bart Vertongen copyright 2021.

using System;
using System.Collections.Generic;
using PS.AddressBook.Data.Interfaces;
using PS.AddressBook.Business.Interfaces;


namespace PS.AddressBook.Business.Commands
{
    public class UpdateContactCommand : IChangeCommand
    {
        private readonly AddressBook _AddressBook;
        private readonly Contact _Contact;

        /// <summary>
        /// The Command to create and add a Contact.
        /// </summary>
        /// <param name="book"></param>
        /// <param name="newContact"></param>
        public UpdateContactCommand(IAddressBook book, Contact changedContact)
        {
            _AddressBook = (AddressBook)book;
            _Contact = changedContact;
        }

        public string ShortName { get; } = "u";

        public string Name { get; } = "update";

        public string Description { get; } = "Update an existing Contact from the AddressBook.";


        public IChangeCommandResponse Run()
        {
            ChangeCommandResponse oResponse = new ChangeCommandResponse();

            try
            {
                if (_Contact.IsValid())
                {
                    _AddressBook.Update(_Contact);
                    _AddressBook.Save();
                    oResponse.WasSuccessful = true;
                    oResponse.Errors = null;
                    oResponse.Result = new List<string>();
                    oResponse.Result.Add($"The Contact with Name '{_Contact.Name}' is updated.");
                }
                else
                {
                    string Line;

                    oResponse.WasSuccessful = false;
                    oResponse.Result = null;
                    oResponse.Errors = new List<string>();
                    if (!_Contact.Address.IsValid())
                        Line = $"The Contact could not be updated because the Address was not valid!";
                    else
                        Line = $"The Contact could not be updated because the new values were not valid!";
                    oResponse.Errors.Add(Line);
                }
            }
            catch (Exception ex)
            {
                string Line;

                oResponse.WasSuccessful = false;
                oResponse.Result = null;
                oResponse.Errors = new List<string>();
                Line = $"An Error Occurred in UpdateContact Command with ContactName={_Contact.Name}.";
                oResponse.Errors.Add(Line);
                Line = "The error description is " + ex.Message;
                oResponse.Errors.Add(Line);
            }

            return oResponse;
        }
    }
}