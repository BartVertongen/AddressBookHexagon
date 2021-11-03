// By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.UseCases;


namespace PS.AddressBook.Framework.Console.Commands
{
    /// <summary>
    /// Gets a Contact based on the given Name.
    /// </summary>
    public class ViewCommand : IUICommand
    {
        private readonly IGetContactWithNameQuery       _GetContactPort;
        private readonly IConsoleUserInterface          _UserInterface;
        private readonly IAddressBookUICommandFactory   _CommandFactory;
        

        /// <summary>
        /// The Command to get en  an existing Contact with a given Name.
        /// </summary>
        /// <param name="book"></param>
        /// <param name="newContact"></param>
        public ViewCommand(IGetContactWithNameQuery getContactPort, 
                                                    IConsoleUserInterface ui, IAddressBookUICommandFactory commandFactory)
        {
            _GetContactPort = getContactPort;
            _UserInterface = ui;
            _CommandFactory = commandFactory;
        }

        public string ShortName { get; } = "v";

        public string Name { get; } = "view";

        public string Description { get; } = "View an existing Contact from the AddressBook.";

        /// <summary>
        /// Shows the Contact on the User Interface.
        /// </summary>
        private void ShowContact(IContactDTO contact)
        {
            string sNameLabel = "Name";
            string sAddressLabel = "Address";
            string sStreetLabel = "Street";
            string sPostalCodeLabel = "Postal Code";
            string sTownLabel = "Town";
            string sPhoneLabel = "Phone";
            string sEmailLabel = "Email";

            //Name
            _UserInterface.WriteMessage($"The Contact with Name '{contact.Name}':");
            _UserInterface.WriteMessage($"{sNameLabel}: {contact.Address.Street}");
            //Address
            _UserInterface.WriteMessage($"{sAddressLabel}:");
            //Street
            _UserInterface.WriteMessage($"\t{sStreetLabel}: {contact.Address.Street}");
            //Postal Code.
            _UserInterface.WriteMessage($"\t{sPostalCodeLabel}: {contact.Address.PostalCode}");
            //Town
            _UserInterface.WriteMessage($"\t{sTownLabel}: {contact.Address.Town}");
            //Phone
            _UserInterface.WriteMessage($"{sPhoneLabel}: {contact.Phone}.");
            //Email
            _UserInterface.WriteMessage($"{sEmailLabel}: {contact.Email}.");
        }

        public (bool WasSuccessful, bool IsTerminating) Run(out object result, string argument = "")
        {
            IContactDTO oContact = null;
            object oSelectedContactName = "";

            try
            {
                //Select an existing Contact
                IUICommand SelectCommand = _CommandFactory.GetCommand("s");
                SelectCommand.Run(out oSelectedContactName);

                //Get the original selected Contact
                oContact = _GetContactPort.GetContactWithName((string)oSelectedContactName);
                ShowContact(oContact);
                result = null;
                return (true, false);
            }
            catch (Exception ex)
            {
                string Line;

                if (oContact == null)
                    Line = $"The Contact with Name '{oSelectedContactName}' could not be found in the System!";
                else
                    Line = $"An Error Occurred in GetContact Command with ContactName={oContact.Name}.";
                _UserInterface.WriteError(Line);
                _UserInterface.WriteError("The error description is " + ex.Message);
                result = null;
                return (false, false);
            }
        }
    }
}