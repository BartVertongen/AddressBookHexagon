// By Bart Vertongen copyright 2021.

using System;
using System.Linq;
using System.Collections.Generic;
using PS.AddressBook.Data.Interfaces;
using PS.AddressBook.Business.Interfaces;


namespace PS.AddressBook.Business.Commands
{
    public class GetOverViewCommand : IQueryCommand
    {
        private readonly string _Filter;
        private readonly AddressBook _AddressBook;

        public GetOverViewCommand(IAddressBook book, string filter)
        {
            _Filter = filter;
            _AddressBook = (AddressBook)book;
        }

        public string ShortName { get; } = "l";

        public string Name { get; } = "list";

        public string Description { get; } = "Gives an overview of Contacts in the AddressBook.";

        public IQueryCommandResponse Run()
        {
            QueryCommandResponse oResponse = new QueryCommandResponse();

            try
            {
                oResponse.Result = _AddressBook.GetOverview(_Filter).Cast<IContactLineDTO>().ToList();
                oResponse.WasSuccessful = true;
                oResponse.Errors = null;
            }
            catch (Exception ex)
            {
                string Line;

                oResponse.WasSuccessful = false;
                oResponse.Result = null;
                oResponse.Errors = new List<string>();
                Line = $"An Error Occurred in GetOverviewCommand with argument filter={_Filter}.";
                oResponse.Errors.Add(Line);
                Line = "The error description is " + ex.Message;
                oResponse.Errors.Add(Line);
            }

            return oResponse;
        }
    }
}
