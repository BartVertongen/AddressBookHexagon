// By Bart Vertongen copyright 2021

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application;


namespace WebAPIAddressBook.Controllers
{
    /// <summary>
    /// WEB API Controller for Updating Contacts.
    /// </summary>
    /// <remarks>This Controller is also an WEB API adapter, it uses Ports.</remarks>
    [Produces("application/json")]
    [ApiController]
    public class UpdateContactController : ControllerBase
    {
        private readonly ILogger<UpdateContactController>   _Logger;
        private readonly IUpdateContactUseCase              _GetUpdateContactPort;
        private readonly IGetContactWithNameQuery           _GetContactWithNamePort;

        /// <summary>
        /// Constructor WEB API for managing Contacts.
        /// </summary>
        /// <param name="updateContactService">Port for the updateContactService</param>
        /// <param name="contactQueryService">Port for GetContactNameQuery</param>
        /// <param name="logger"></param>
        public UpdateContactController(IUpdateContactUseCase updateContactService, 
                    IGetContactWithNameQuery contactQueryService, ILogger<UpdateContactController> logger)
        {
            _GetUpdateContactPort = updateContactService;
            _GetContactWithNamePort = contactQueryService;
            _Logger = logger;
        }

        /// <summary>
        /// Changes the Data of an existing Contact except the Name.
        /// </summary>
        /// <param name="name">The name of an existing Contact.</param>
        /// <param name="changedContact">new values for the Contact.</param>
        /// <returns></returns>
        [Route("api/addressbook/contact/{name}")]
        [HttpPut]
        public IActionResult Update(string name, ContactDTO changedContact)
        {
            IContactDTO OldContact, updatedContact;
            UpdateContactCommand oUpdateContactCommand;

            if (name != changedContact.Name)
                return BadRequest();

            OldContact = _GetContactWithNamePort.GetContactWithName(name);
            if (OldContact is null)
                return NotFound();
            oUpdateContactCommand = new UpdateContactCommand(name,
                        changedContact.Address.Street,
                        changedContact.Address.PostalCode,
                        changedContact.Address.Town,
                        changedContact.Phone,
                        changedContact.Email);
            updatedContact = _GetUpdateContactPort.UpdateContact(oUpdateContactCommand);
            if (updatedContact == null)
                return BadRequest();
            else
                return NoContent();
        }
    }
}