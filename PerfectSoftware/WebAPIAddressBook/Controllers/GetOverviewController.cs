// By Bart Vertongen copyright 2021

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.UseCases;


namespace WebAPIAddressBook.Controllers
{
    /// <summary>
    /// WEB API Controller for managing Contacts.
    /// </summary>
    /// <remarks>This Controller is also an WEB API adapter, it uses Ports.</remarks>
    [Produces("application/json")]
    [ApiController]
    public class GetOverviewController : ControllerBase
    {
        private readonly ILogger<UpdateContactController> _Logger;
        private readonly IGetOverviewQuery      _GetOverviewPort;

        /// <summary>
        /// Constructor WEB API GetOverview Adapter for managing Contacts.
        /// </summary>
        /// <param name="overviewQuery">Port for the Contact OverviewQueryService</param>
        /// <param name="logger"></param>
        public GetOverviewController(IGetOverviewQuery overviewQuery, ILogger<UpdateContactController> logger)
        {
            _GetOverviewPort = overviewQuery;
            _Logger = logger;
        }


        /// <summary>
        /// Shows an overview of all Contacts in the addressbook filtered by filter which can be empty.
        /// </summary>
        /// <param name="filter">'a' starts with 'a'; '*de*' contains 'de'</param>
        /// <returns></returns>
        [Route("api/addressbook/overview/{filter?}")]
        [HttpGet]
        public ActionResult<List<IContactLineDTO>> GetOverview(string filter = null)
        {
            List<IContactLineDTO> ContactLines;

            if (filter is null)
                ContactLines = _GetOverviewPort.GetOverview("");
            else
                ContactLines = _GetOverviewPort.GetOverview(filter);
            return ContactLines.Cast<IContactLineDTO>().ToList();
        }
    }
}