// By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Hexagon.Application.UseCases;
using AppDeleteCommand = PS.AddressBook.Hexagon.Application.Commands.DeleteContactCommand;


namespace PS.AddressBook.Framework.Console.Commands
{
    public class DeleteContactCommand : IUICommand
    {
        private readonly IDeleteContactUseCase  _DeleteContactPort;
        private readonly IGetOverviewQuery      _GetOverviewPort;
        private readonly IConsoleUserInterface  _UserInterface;

        public DeleteContactCommand(IDeleteContactUseCase deleteContactPort, IGetOverviewQuery getOverviewPort, IConsoleUserInterface ui)
        {
            _DeleteContactPort = deleteContactPort;
            _GetOverviewPort = getOverviewPort;
            _UserInterface = ui;
        }

        public string ShortName { get; } = "d";

        public string Name { get; } = "delete";

        public string Description { get; } = "Deletes a Contact from the AddressBook.";


        public (bool WasSuccessful, bool IsTerminating) Run(out object result, string argument = "")
        {
            object oSelectedName = "";
            try
            {               
                SelectContactCommand SelectCommand = new(_GetOverviewPort, _UserInterface);

                if (SelectCommand.Run(out oSelectedName).WasSuccessful)
                {
                    AppDeleteCommand AppCommand = new((string)oSelectedName);
                    _DeleteContactPort.DeleteContact(AppCommand);
                    _UserInterface.WriteWarning($"The Contact with Name '{(string)oSelectedName}' is deleted.");
                    result = null;
                    return (true, false);
                }
                else
                {
                    _UserInterface.WriteWarning($"There is was no Contact selected to delete.");
                    result = null;
                    return (false, false);
                }
            }
            catch (Exception ex)
            {
                string Line;

                Line = $"An Error Occurred in DeleteContact Command with argument ContactName='{(string)oSelectedName}'.";
                _UserInterface.WriteError(Line);
                _UserInterface.WriteError("The error is " + ex.Message);
                result = null;
                return (false, false);
            }
        }
    }
}