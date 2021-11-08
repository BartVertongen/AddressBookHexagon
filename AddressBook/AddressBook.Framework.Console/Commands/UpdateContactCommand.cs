// By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.UseCases;
//using AppUpdateContactCommand = PS.AddressBook.Hexagon.Application.Commands.UpdateContactCommand;


namespace PS.AddressBook.Framework.Console.Commands
{
    public class UpdateContactCommand : IUICommand
    {
        private readonly IGetContactWithNameQuery   _GetContactPort;
        private readonly IUpdateContactUseCase      _UpdateContactPort;
        private readonly IConsoleUserInterface      _UserInterface;
        private readonly IAddressBookUICommandFactory   _CommandFactory;
        

        /// <summary>
        /// The Command to create and add a Contact.
        /// </summary>
        /// <param name="book"></param>
        /// <param name="newContact"></param>
        public UpdateContactCommand(IUpdateContactUseCase updateContactPort, IGetContactWithNameQuery getContactPort, 
                                                    IConsoleUserInterface ui, IAddressBookUICommandFactory commandFactory)
        {
            _UpdateContactPort = updateContactPort;
            _GetContactPort = getContactPort;
            _UserInterface = ui;
            _CommandFactory = commandFactory;
        }

        public string ShortName { get; } = "u";

        public string Name { get; } = "update";

        public string Description { get; } = "Update an existing Contact from the AddressBook.";

        /// <summary>
        /// Gets the needed data from the userinterface.
        /// </summary>
        private IUpdateContactCommandDTO GetUpdateContactCommand(IContactDTO oldContact)
        {
            string sNewStreet, sNewPostalCode, sNewTown, sNewPhone, sNewEmail;
            UpdateContactCommandBuilder oBuilder = new();

            oBuilder.AddName(oldContact.Name);

            //Street
            _UserInterface.WriteMessage($"The current value for the street and number is {oldContact.Address.Street}.");
            sNewStreet = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (sNewStreet.ToUpper() != "XX")
                oBuilder.AddStreet(sNewStreet);

            //Postal Code.
            _UserInterface.WriteMessage($"The current value for the postal code is {oldContact.Address.PostalCode}.");
            sNewPostalCode = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (sNewPostalCode.ToUpper() != "XX")
                oBuilder.AddPostalCode(sNewPostalCode);

            //Town
            _UserInterface.WriteMessage($"The current value for the town is {oldContact.Address.Town}.");
            sNewTown = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (sNewTown.ToUpper() != "XX")
                oBuilder.AddTown(sNewTown);

            //Phone
            _UserInterface.WriteMessage($"The current value for the phone number is {oldContact.Phone}.");
            sNewPhone = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (sNewPhone.ToUpper() != "XX")
                oBuilder.AddPhone(sNewPhone);

            //Email
            _UserInterface.WriteMessage($"The current value for the email is {oldContact.Email}.");
            sNewEmail = _UserInterface.ReadValue("Give in 'XX' to keep this value or type in another value: ");
            if (sNewEmail.ToUpper() != "XX")
                oBuilder.AddEmail(sNewEmail);

            return oBuilder.Build();
        }

        public (bool WasSuccessful, bool IsTerminating) Run(out object result, string argument = "")
        {
            IContactDTO oOldContact = null;

            try
            {
                //Select an existing Contact
                IContactDTO oNewContact;
                IUICommand SelectCommand = _CommandFactory.GetCommand("s");
                SelectCommand.Run(out object oSelectedContactName);

                //Get the original selected Contact
                oOldContact = _GetContactPort.GetContactWithName((string)oSelectedContactName);

                oNewContact = _UpdateContactPort.UpdateContact(this.GetUpdateContactCommand(oOldContact));
                _UserInterface.WriteMessage($"The Contact with Name '{oOldContact.Name}' is updated.");
                result = null;
                return (true, false);
            }
            catch (Exception ex)
            {
                string Line;

                if (oOldContact == null)
                    Line = $"The Contact with could not be found in the System!";
                else
                    Line = $"An Error Occurred in UpdateContact Command with ContactName={oOldContact.Name}.";
                _UserInterface.WriteError(Line);
                _UserInterface.WriteError("The error description is " + ex.Message);
                result = null;
                return (false, false);
            }
        }
    }
}