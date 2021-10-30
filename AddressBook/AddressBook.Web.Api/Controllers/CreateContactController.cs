﻿// By Bart Vertongen copyright 2021

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.Ports;


namespace WebAPIAddressBook.Controllers
{
    /// <summary>
    /// WEB API Controller for Creating Contacts.
    /// </summary>
    /// <remarks>This Controller is also an WEB API adapter, it uses Ports.</remarks>
    [Produces("application/json")]
    [ApiController]
    public class CreateContactController : ControllerBase
    {
        private readonly ILogger<CreateContactController> _Logger;
        private readonly ICreateContactUseCase      _CreateContactPort;

        /// <summary>
        /// Constructor WEB API for managing Contacts.
        /// </summary>
        /// <param name="createContactService">Port for the CreateContactService</param>
        /// <param name="logger"></param>
        public CreateContactController(ICreateContactUseCase createContactService, ILogger<CreateContactController> logger)
        {
            _CreateContactPort = createContactService;
            _Logger = logger;
        }


        /// <summary>
        /// Creates a new Contact.
        /// </summary>
        /// <param name="newContact"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("api/addressbook/contact")]
        [HttpPost]
        public IActionResult Create(ContactDTO newContact)
        {
            try
            {
                CreateContactCommand oCommand;
                IContactDTO createdContact;

                oCommand = new CreateContactCommand(newContact.Name, newContact.Address.Street,
                            newContact.Address.PostalCode, newContact.Address.Town,
                                                        newContact.Phone, newContact.Email);
                createdContact = _CreateContactPort.CreateContact(oCommand);
                return CreatedAtAction(nameof(Create), new { name = newContact.Name }, createdContact);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }
    }
}