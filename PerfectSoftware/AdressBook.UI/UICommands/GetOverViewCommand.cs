// By Bart Vertongen copyright 2021.

using System;
using System.Linq;
using System.Collections.Generic;
using PS.AddressBook.Data.Interfaces;
using PS.AddressBook.Business.Interfaces;
using BussAddressBook = PS.AddressBook.Business.AddressBook;


namespace PS.AddressBook.UI.Commands
{
    public class GetOverViewCommand : IQueryCommand
    {
        private readonly BussAddressBook _AddressBook;
        private readonly IConsoleUserInterface _UserInterface;

        public GetOverViewCommand(IAddressBook book, IConsoleUserInterface ui)
        {
            _AddressBook = (BussAddressBook)book;
            _UserInterface = ui;
        }

        public string ShortName { get; } = "l";

        public string Name { get; } = "list";

        public string Description { get; } = "Gives an overview of Contacts in the AddressBook.";

        public (bool WasSuccessful, bool IsTerminating) Run(string argument = "")
        {
            string sFilter="";

            try
            {
                sFilter = _UserInterface.ReadValue("Give the filter value to select a Contact ['', 'a', '*de*'] :");
                List<IContactLineDTO> Result = _AddressBook.GetOverview(sFilter).Cast<IContactLineDTO>().ToList();               
                if (Result.Count > 0)
                {
                    _UserInterface.WriteMessage($"The Contacts passing the filter '{sFilter}' are:");
                    foreach (IContactLineDTO Line in Result)
                    {
                        _UserInterface.WriteMessage($"{Line.Id})\t{Line.Name} {Line.ContentsCode}");
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(sFilter))
                        _UserInterface.WriteWarning("The Address Book has no Contacts!");
                    else
                        _UserInterface.WriteWarning($"There are no Contacts found passing that filter!");
                }                   
                return (true, false);
            }
            catch (Exception ex)
            {
                string Line;

                Line = $"An Error Occurred in GetOverviewCommand with Filter={sFilter}.";
                _UserInterface.WriteError(Line);
                _UserInterface.WriteError("The error description is " + ex.Message);
                return (false, false);
            }
        }
    }
}