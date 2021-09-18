// By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Data.Interfaces;
using PS.AddressBook.Business.Interfaces;
using BussAddressBook = PS.AddressBook.Business.AddressBook;


namespace PS.AddressBook.UI.Commands
{
    public class DeleteContactCommand : IChangeCommand
    {
        private readonly BussAddressBook _AddressBook;
        private readonly IConsoleUserInterface _UserInterface;
        private readonly IAddressBookUICommandFactory _CommandFactory;

        public DeleteContactCommand(IAddressBook book, IConsoleUserInterface ui, IAddressBookUICommandFactory commandFactory)
        {
            _AddressBook = (BussAddressBook)book;
            _UserInterface = ui;
            _CommandFactory = commandFactory;
        }

        public string ShortName { get; } = "d";

        public string Name { get; } = "delete";

        public string Description { get; } = "Deletes a Contact from the AddressBook.";


        public (bool WasSuccessful, bool IsTerminating) Run(string argument="")
        {
            string sName = "";

            try
            {               
                SelectContactCommand SelectCommand = new SelectContactCommand(_AddressBook, _UserInterface);

                SelectCommand.Run();
                sName = SelectCommand.SelectedContactName;
                if (!string.IsNullOrEmpty(sName))
                {
                    _AddressBook.Delete(sName);
                    _AddressBook.Save();
                    _UserInterface.WriteMessage($"The Contact with Name {sName} is deleted.");
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