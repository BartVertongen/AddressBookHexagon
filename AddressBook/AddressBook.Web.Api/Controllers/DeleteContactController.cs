// By Bart Vertongen copyright 2021

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application;


namespace WebAPIAddressBook.Controllers
{
    /// <summary>
    /// WEB API Controller for managing Contacts.
    /// </summary>
    /// <remarks>This Controller is also an WEB API adapter, it uses Ports.</remarks>
    [Produces("application/json")]
    [ApiController]
    public class DeleteContactController : ControllerBase
    {
        private readonly ILogger<DeleteContactController> _Logger;
        private readonly IDeleteContactUseCase          _DeleteContactService;

        /// <summary>
        /// Constructor WEB API adapter for Deleting a Contact.
        /// </summary>
        /// <param name="service">Port for the Delete Contact Service</param>
        /// <param name="logger"></param>
        public DeleteContactController(IDeleteContactUseCase service, ILogger<DeleteContactController> logger)
        {
            _DeleteContactService = service;
            _Logger = logger;
        }

        /// <summary>
        /// Deletes the Contact with the given Name from the AddressBook.
        /// </summary>
        /// <param name="name">Name of the Contact</param>
        /// <returns></returns>
        [Route("api/addressbook/contact/delete/{name}")]
        [HttpDelete]
        public IActionResult Delete(string name)
        {
            IContactDTO ToRemoveContact;
            DeleteContactCommandBuilder oBuilder = new();

            oBuilder.AddName(name);
            ToRemoveContact = _DeleteContactService.DeleteContact((DeleteContactCommand)oBuilder.Build());
            if (ToRemoveContact is null)
                return NotFound();
            else
                return NoContent();
        }

        /// <summary>
        /// Deletes the Contact with the given Name from the AddressBook.
        /// </summary>
        /// <returns></returns>
        [Route("api/addressbook/contact/delete/")]
        [HttpDelete]
        public IActionResult Delete2([FromBody] DeleteContactCommandDTO command)
        {
            IContactDTO ToRemoveContact;
            DeleteContactCommand oCommand;
            DeleteContactCommandBuilder oBuilder = new();

            oCommand = (DeleteContactCommand)oBuilder.AddName(command.Name).Build();
            ToRemoveContact = _DeleteContactService.DeleteContact(oCommand);

            if (ToRemoveContact is null)
                return NotFound();
            else
                return NoContent();
        }
    }
}