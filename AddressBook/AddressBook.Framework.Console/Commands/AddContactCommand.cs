// By Bart Vertongen copyright 2021.

using System;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.Ports;


namespace PS.AddressBook.Framework.Console.Commands
{
    public class AddContactCommand : IUICommand
    {
        private readonly ICreateContactUseCase _CreateContactPort;       
        private readonly IConsoleUserInterface _UserInterface;

        /// <summary>
        /// The Command to create and add a Contact.
        /// </summary>
        /// <param name="createContactPort"></param>
        /// <param name="ui"></param>
        public AddContactCommand(ICreateContactUseCase createContactPort, IConsoleUserInterface ui)
        {
            _CreateContactPort = createContactPort;
            _UserInterface = ui;
        }

        public string ShortName { get; } = "a";

        public string Name { get; } = "add";

        public string Description { get; } = "Adds a new Contact to the AddressBook.";

        /// <summary>
        /// Gets the needed info from the User and holds it in a Contact object.
        /// </summary>
        private CreateContactCommand GetContactData()
        {
            string sResponse;
            CreateContactCommandBuilder oBuilder = new();

            sResponse = _UserInterface.ReadValue("Give a name for the new Contact: ");
            if (sResponse != null) oBuilder.AddName(sResponse);

            sResponse = _UserInterface.ReadValue("Give a street and number for the Address of the new Contact: ");
            if (sResponse != null) oBuilder.AddStreet(sResponse);

            sResponse = _UserInterface.ReadValue("Give a postal code for the Address of the new Contact: ");
            if (sResponse != null) oBuilder.AddPostalCode(sResponse);

            sResponse  = _UserInterface.ReadValue("Give a town for the Address of the new Contact: ");
            if (sResponse != null) oBuilder.AddTown(sResponse);

            sResponse = _UserInterface.ReadValue("Give a phone number for the new Contact: ");
            if (sResponse != null) oBuilder.AddPhone(sResponse);

            sResponse = _UserInterface.ReadValue("Give an email for the new Contact: ");
            if (sResponse != null) oBuilder.AddEmail(sResponse);
            _UserInterface.WriteMessage("");

            return (CreateContactCommand)oBuilder.Build();
        }

        public (bool WasSuccessful, bool IsTerminating) Run(out object result, string argument = null)
        {
            IContactDTO oContact = null;

            try
            {
                oContact = _CreateContactPort.CreateContact(this.GetContactData());

                _UserInterface.WriteMessage("");
                _UserInterface.WriteWarning($"The Contact with Name '{oContact.Name}' is added.");
                _UserInterface.WriteMessage("");
                result = null;
                return (true, false);
            }

            catch (Exception ex)
            {
                string Line;

                if (oContact == null)
                    Line = $"An Error Occurred in AddContact Command, The Contact could not be created!";
                else
                    Line = $"An Error Occurred in AddContact Command with argument ContactName={oContact.Name}.";
                _UserInterface.WriteError(Line);
                _UserInterface.WriteError("The error description is " + ex.Message);
                _UserInterface.WriteMessage("");
                result = null;
                return (false, false);
            }
        }
    }
}