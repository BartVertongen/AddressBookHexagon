// By Bart Vertongen copyright 2021.

using System;
using System.Linq;
using System.Collections.Generic;
using PS.AddressBook.Data.Interfaces;
using PS.AddressBook.Business.Interfaces;


namespace PS.AddressBook.Business.Commands
{
    public class DeleteContactCommand : IChangeCommand
    {
        private readonly string _ContactName;
        private readonly AddressBook _AddressBook;

        public DeleteContactCommand(IAddressBook book, string contactName)
        {
            _ContactName = contactName;
            _AddressBook = (AddressBook)book;
        }

        public string ShortName { get; } = "d";

        public string Name { get; } = "del";

        public string Description { get; } = "Deletes a Contact from the AddressBook.";


        public IChangeCommandResponse Run()
        {
            ChangeCommandResponse oResponse = new ChangeCommandResponse();

            try
            {
                _AddressBook.Delete(_ContactName);
                _AddressBook.Save();
                oResponse.WasSuccessful = true;
                oResponse.Errors = null;
                oResponse.Result = new List<string>();
                oResponse.Result.Add($"The Contact with Name {_ContactName} is deleted.");
            }
            catch (Exception ex)
            {
                string Line;

                oResponse.WasSuccessful = false;
                oResponse.Result = null;
                oResponse.Errors = new List<string>();
                Line = $"An Error Occurred in DeleteContact Command with argument ContactName={_ContactName}.";
                oResponse.Errors.Add(Line);
                Line = "The error description is " + ex.Message;
                oResponse.Errors.Add(Line);
            }

            return oResponse;
        }
    }
}
