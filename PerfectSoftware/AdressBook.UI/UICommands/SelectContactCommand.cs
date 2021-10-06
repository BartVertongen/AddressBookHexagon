// By Bart Vertongen copyright 2021.

using System;
using System.Linq;
using System.Collections.Generic;
using PS.AddressBook.Business.Interfaces;
using BussAddressBook = PS.AddressBook.Business.AddressBook;


namespace PS.AddressBook.UI.Commands
{
    public class SelectContactCommand : IQueryCommand
    {
        private readonly BussAddressBook _AddressBook;
        private readonly IConsoleUserInterface _UserInterface;

        public SelectContactCommand(IAddressBook book, IConsoleUserInterface ui)
        {
            _AddressBook = (BussAddressBook)book;
            _UserInterface = ui;
        }

        public string ShortName { get; } = "s";

        public string Name { get; } = "select";

        public string Description { get; } = "Selects a Contact from the AddressBook.";

        public (bool WasSuccessful, bool IsTerminating) Run(string argument = "")
        {
            string sFilter = "";

            try
            {
                string sID, CurrentLetter, PreviousLetter = "";
                string sLine;


                sFilter = _UserInterface.ReadValue("Give the filter value to select a Contact ['', 'a', '*de*']: ");
                List<IContactLineDTO> Result = _AddressBook.GetOverview(sFilter).Cast<IContactLineDTO>().ToList();
                _UserInterface.WriteMessage($"The Contacts passing the filter '{sFilter}' are:");
                foreach (IContactLineDTO Line in Result)
                {
                    CurrentLetter = Line.Name.Substring(0, 1);
                    if (CurrentLetter != PreviousLetter)
                    {
                        _UserInterface.WriteWarning("[" + CurrentLetter + "]");
                        PreviousLetter = CurrentLetter;
                    }
                    sLine = string.Format("{0,-40} {1,3}", Line.Name, Line.ContentsCode);
                    _UserInterface.WriteMessage(sLine);
                }
                sID = _UserInterface.ReadValue("Give the Id of the Contact you want to select: ");

                if (int.TryParse(sID, out int Selected))
                    _AddressBook.SelectedContactName = Result[Selected - 1].Name;
                else
                    _AddressBook.SelectedContactName = "";
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