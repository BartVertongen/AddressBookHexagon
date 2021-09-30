// By Bart Vertongen copyright 2021

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS.AddressBook.Business;
using PS.AddressBook.Business.Interfaces;
using PS.AddressBook.Data;
using PS.AddressBook.Data.Interfaces;


namespace WebAPIAddressBook.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressBookController : ControllerBase
    {
        private readonly ILogger<AddressBookController> _logger;
        private IAddressBookService _Service;

        public AddressBookController(IAddressBookService service, ILogger<AddressBookController> logger)
        {
            _Service = service;
            _logger = logger;
        }


        /// <summary>
        /// Shows an overview of all Contacts in the addressbook filtered by filter which can be empty.
        /// </summary>
        /// <param name="filter">'a' starts with 'a'; '*de*' contains 'de'</param>
        /// <returns></returns>
        [HttpGet("{filter?}")]
        public ActionResult<List<ContactLineDTO>> GetOverview(string filter = null)
        {
            List<ContactLineDTO> Result;
            List<IContactLineDTO> ContactLines;

            if (filter is null)
                ContactLines = (List<IContactLineDTO>)_Service.GetOverview("");
            else
                ContactLines = (List<IContactLineDTO>)_Service.GetOverview(filter);
            Result = ContactLines.Cast<ContactLineDTO>().ToList();
            return Result;
        }


        /// <summary>
        /// Gets the Contact with the given name from the addressbook.
        /// </summary>
        /// <param name="name">Th namer of the wanted contact.</param>
        /// <returns>the requested contact</returns>
        [HttpGet("{name}")]
        public ActionResult<ContactDTO> Get(string name)
        {
            ContactDTO aContact = (ContactDTO)_Service.Get(name);

            if (aContact == null)
                return NotFound();

            return aContact;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult Create(ContactDTO newContact)
        {
            _Service.Add(newContact);
            return CreatedAtAction(nameof(Create), new { name = newContact.Name }, newContact);
        }

        [HttpPut("{name}")]
        public IActionResult Update(string name, ContactDTO changedContact)
        {
            IContactDTO OldContact;

            if (name != changedContact.Name)
                return BadRequest();
            OldContact = _Service.Get(name);
            if (OldContact is null)
                return NotFound();

            _Service.Update(changedContact);

            return NoContent();
        }


        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            var ToRemoveContact = _Service.Get(name);

            if (ToRemoveContact is null)
                return NotFound();

            _Service.Delete(name);

            return NoContent();
        }
    }
}