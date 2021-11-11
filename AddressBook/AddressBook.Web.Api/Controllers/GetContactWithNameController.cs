// By Bart Vertongen copyright 2021

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.Ports;


namespace WebAPIAddressBook.Controllers
{
    /// <summary>
    /// WEB API Controller for managing Contacts.
    /// </summary>
    /// <remarks>This Controller is also an WEB API adapter, it uses Ports.</remarks>
    [Produces("application/json")]
    [ApiController]
    public class GetContactWithNameController : ControllerBase
    {
        private readonly ILogger<UpdateContactController> _Logger;
        private readonly IGetContactWithNameQuery       _GetContactWithNamePort;

        /// <summary>
        /// Constructor WEB API for managing Contacts.
        /// </summary>
        /// <param name="contactWithNameQuery">Port for the Contact GetContactWithNameQueryService</param>
        /// <param name="logger"></param>
        public GetContactWithNameController(IGetContactWithNameQuery contactWithNameQuery, ILogger<UpdateContactController> logger)
        {
            _GetContactWithNamePort = contactWithNameQuery;
            _Logger = logger;
        }


        /// <summary>
        /// Gets the Contact with the given name from the addressbook.
        /// </summary>
        /// <param name="name">The name of the wanted contact.</param>
        /// <returns>the requested contact</returns>
        [Route("api/addressbook/contact/view/{name}")]
        [HttpGet]
        public ActionResult<IContactDTO> Get(string name)
        {
            IContactDTO aContact;

            aContact = _GetContactWithNamePort.GetContactWithName(name);
            if (aContact == null)
                return NotFound();
            else
            {
                ContactDTO Result = new(aContact);
                return Result;
            }          
        }
    }
}