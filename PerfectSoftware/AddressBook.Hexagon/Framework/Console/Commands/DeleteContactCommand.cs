// By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Hexagon.Domain.Core;
using BussAddressBook = PS.AddressBook.Hexagon.Domain.AddressBook;


namespace PS.AddressBook.Hexagon.Framework.Console.Commands
{
    public class DeleteContactCommand : IChangeCommand
    {
        private readonly BussAddressBook _AddressBook;
        private readonly IConsoleUserInterface _UserInterface;

        public DeleteContactCommand(IAddressBook book, IConsoleUserInterface ui)
        {
            _AddressBook = (BussAddressBook)book;
            _UserInterface = ui;
        }

        public string ShortName { get; } = "d";

        public string Name { get; } = "delete";

        public string Description { get; } = "Deletes a Contact from the AddressBook.";


        public (bool WasSuccessful, bool IsTerminating) Run(string argument="")
        {
            string sName = "";

            try
            {               
                SelectContactCommand SelectCommand = new(_AddressBook, _UserInterface);

                SelectCommand.Run();
                sName = _AddressBook.SelectedContactName;
                if (!string.IsNullOrEmpty(sName))
                {
                    _AddressBook.Delete(sName);
                    _AddressBook.Save();
                    _UserInterface.WriteMessage($"The Contact with Name '{sName}' is deleted.");
                    return (true, false);
                }
                else
                {
                    _UserInterface.WriteMessage($"There is was no Contact selected to delete.");
                    return (false, false);
                }

            }
            catch (Exception ex)
            {
                string Line;

                Line = $"An Error Occurred in DeleteContact Command with argument ContactName={sName}.";
                _UserInterface.WriteError(Line);
                _UserInterface.WriteError("The error is " + ex.Message);
                return (false, false);
            }
        }
    }
}