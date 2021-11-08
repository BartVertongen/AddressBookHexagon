// By Bart Vertongen copyright 2021

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.Ports;


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
        /// <param name="command">The update command.</param>
        /// <returns></returns>
        [Route("api/addressbook/contact/update")]
        [HttpPut]
        public IActionResult Update(UpdateContactCommandDTO command)
        {
            IContactDTO OldContact, updatedContact;

            OldContact = _GetContactWithNamePort.GetContactWithName(command.Name);
            if (OldContact is null)
                return NotFound();

            updatedContact = _GetUpdateContactPort.UpdateContact(command);
            if (updatedContact == null)
                return BadRequest();
            else
                return NoContent();
        }
    }
}