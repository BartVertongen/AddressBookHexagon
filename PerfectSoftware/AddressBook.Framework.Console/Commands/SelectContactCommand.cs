// By Bart Vertongen copyright 2021.

using System;
using System.Collections.Generic;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.UseCases;


namespace PS.AddressBook.Framework.Console.Commands
{
    public class SelectContactCommand : IUICommand
    {
        private readonly IGetOverviewQuery _GetOverviewPort;
        private readonly IConsoleUserInterface _UserInterface;

        public SelectContactCommand(IGetOverviewQuery getOverviewPort, IConsoleUserInterface ui)
        {
            _GetOverviewPort = getOverviewPort;
            _UserInterface = ui;
        }

        public string ShortName { get; } = "s";

        public string Name { get; } = "select";

        public string Description { get; } = "Selects a Contact from the AddressBook.";

        /// <summary>
        /// This UICommand lets you select a Contact from a list.
        /// </summary>
        /// <param name="result">
        /// Object containing the name of the selected Contact.
        /// Will be null if something went wrong.
        /// </param>
        /// <returns>a tuple with 2 items WasSuccessful and IsTerminating</returns>
        public (bool WasSuccessful, bool IsTerminating) Run(out object result, string argument = "")
        {
            string sFilter = "";

            try
            {
                string sID = "", CurrentLetter, PreviousLetter = "";
                string sLine;


                sFilter = _UserInterface.ReadValue("Give the filter value to select a Contact ['', 'a', '*de*']: ");
                List<IContactLineDTO> Result = _GetOverviewPort.GetOverview(sFilter);
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
                    result = Result[Selected - 1].Name;
                else
                    result = null;
                return (true, false);
            }
            catch (Exception ex)
            {
                string Line;

                result = null;
                Line = $"An Error Occurred in GetOverviewCommand with Filter={sFilter}.";
                _UserInterface.WriteError(Line);
                _UserInterface.WriteError("The error description is " + ex.Message);
                return (false, false);
            }
        }
    }
}