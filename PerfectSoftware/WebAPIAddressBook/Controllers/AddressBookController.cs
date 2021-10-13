﻿// By Bart Vertongen copyright 2021

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS.AddressBook.Hexagon.Domain;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.UseCases;


namespace WebAPIAddressBook.Controllers
{
    /// <summary>
    /// WEB API Controller for managing Contacts.
    /// </summary>
    /// <remarks>This Controller is also an WEB API adapter, it uses Ports.</remarks>
    [Produces("application/json")]
    [ApiController]
    public class AddressBookController : ControllerBase
    {
        private readonly ILogger<AddressBookController> _logger;
        private readonly IAddressBookService    _Service;
        private readonly IGetOverviewQuery      _GetOverviewPort;

        /// <summary>
        /// Constructor WEB API for managing Contacts.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="overviewQuery">Port for the Contact OverviewQueryService</param>
        /// <param name="logger"></param>
        public AddressBookController(IAddressBookService service, IGetOverviewQuery overviewQuery, ILogger<AddressBookController> logger)
        {
            _Service = service;
            _GetOverviewPort = overviewQuery;
            _logger = logger;
        }


        /// <summary>
        /// Shows an overview of all Contacts in the addressbook filtered by filter which can be empty.
        /// </summary>
        /// <param name="filter">'a' starts with 'a'; '*de*' contains 'de'</param>
        /// <returns></returns>
        [Route("api/[controller]/overview/{filter?}")]
        [HttpGet]
        public ActionResult<List<ContactLineDTO>> GetOverview(string filter = null)
        {
            List<IContactLineDTO> ContactLines;

            if (filter is null)
                ContactLines = _GetOverviewPort.GetOverview("");
            else
                ContactLines = _GetOverviewPort.GetOverview(filter);
            return ContactLines.Cast<ContactLineDTO>().ToList();
        }


        /// <summary>
        /// Gets the Contact with the given name from the addressbook.
        /// </summary>
        /// <param name="name">The name of the wanted contact.</param>
        /// <returns>the requested contact</returns>
        [Route("api/[controller]/contact/{name}")]
        [HttpGet]
        public ActionResult<IContactDTO> Get(string name)
        {
            IContactDTO aContact;

            aContact = _Service.Get(name);
            if (aContact == null)
                return NotFound();
            else
            {
                //TODO Can we remove the need of type ContactDTO outside the hexagon?
                ContactDTO Result = new(aContact);
                return Result;
            }          
        }

        /// <summary>
        /// Creates a new Contact.
        /// </summary>
        /// <param name="newContact"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("api/[controller]/contact")]
        [HttpPost]
        public IActionResult Create(IContactDTO newContact)
        {
            try
            {
                _Service.Add(newContact);
                return CreatedAtAction(nameof(Create), new { name = newContact.Name }, newContact);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        /// <summary>
        /// Changes the Data of an existing Contact except the Name.
        /// </summary>
        /// <param name="name">The name of an existing Contact.</param>
        /// <param name="changedContact">new values for the Contact.</param>
        /// <returns></returns>
        [Route("api/[controller]/contact/{name}")]
        [HttpPut]
        public IActionResult Update(string name, IContactDTO changedContact)
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

        /// <summary>
        /// Deletes the Contact with the given Name from the AddressBook.
        /// </summary>
        /// <param name="name">Name of the Contact</param>
        /// <returns></returns>
        [Route("api/[controller]/contact/{name}")]
        [HttpDelete]
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