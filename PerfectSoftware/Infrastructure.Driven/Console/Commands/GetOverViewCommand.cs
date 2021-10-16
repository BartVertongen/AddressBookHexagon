// By Bart Vertongen copyright 2021.

using System;
using System.Collections.Generic;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.UseCases;


namespace PS.AddressBook.Framework.Console.Commands
{
    public class GetOverViewCommand : IUICommand
    {
        private readonly IGetOverviewQuery _GetOverviewPort;
        private readonly IConsoleUserInterface _UserInterface;

        public GetOverViewCommand(IGetOverviewQuery getOverviewPort, IConsoleUserInterface ui)
        {
            _GetOverviewPort = getOverviewPort;
            _UserInterface = ui;
        }

        public string ShortName { get; } = "l";

        public string Name { get; } = "list";

        public string Description { get; } = "Gives an overview of Contacts in the AddressBook.";

        public (bool WasSuccessful, bool IsTerminating) Run(out object result, string argument = "")
        {
            string sLine, sFilter="";

            try
            {
                string CurrentLetter, PreviousLetter = "";

                sFilter = _UserInterface.ReadValue("Give the filter value to select a Contact ['', 'a', '*de*']: ");
                IList<IContactLineDTO> Result = _GetOverviewPort.GetOverview(sFilter);               
                if (Result.Count > 0)
                {
                    _UserInterface.WriteMessage("");
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
                    _UserInterface.WriteMessage("");
                }
                else
                {
                    if (string.IsNullOrEmpty(sFilter))
                    {
                        _UserInterface.WriteMessage("");
                        _UserInterface.WriteWarning("The Address Book has no Contacts!");
                        _UserInterface.WriteMessage("");
                    }                      
                    else
                    {
                        _UserInterface.WriteMessage("");
                        _UserInterface.WriteWarning($"There are no Contacts found passing that filter!");
                        _UserInterface.WriteMessage("");
                    }                    
                }
                result = null;
                return (true, false);
            }
            catch (Exception ex)
            {
                string Line;

                Line = $"An Error Occurred in GetOverviewCommand with Filter={sFilter}.";
                _UserInterface.WriteError(Line);
                _UserInterface.WriteError("The error description is " + ex.Message);
                result = null;
                return (false, false);
            }
        }
    }
}