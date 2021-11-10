//By Bart Vertongen copyright 2021.

using Microsoft.AspNetCore.Mvc;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.Mappers;
using PS.AddressBook.Hexagon.Application.Ports;


namespace AddressBook.Web.Api.Controllers
{
    /// <summary>
    /// A WEB API Controller to expose DeleteCommandBuilder as a Service.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class DeleteCommandBuilderController : Controller
    {
        /// <summary>
        /// Exposes AddName method of DeleteContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="name">The Name to add.</param>
        /// <returns>Builder Data</returns>
        [Route("api/addressbook/deletecontactcommand/addname/{name}")]
        [HttpGet]
        public ActionResult<DeleteContactCommandBuilderDTO> AddName([FromBody] DeleteContactCommandBuilderDTO builder, string name)
        {
            DeleteContactCommandBuilderDTOMapper oMapper = new();
            IDeleteContactCommandBuilder oBussBuilder;

            builder.Name = name;
            oBussBuilder = oMapper.MapFrom(builder);
            return (DeleteContactCommandBuilderDTO)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes Build method of DeleteContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <returns>Command Data</returns>
        [Route("api/addressbook/deletecontactcommand/build")]
        [HttpGet]
        public ActionResult<DeleteContactCommandDTO> Build([FromBody] DeleteContactCommandBuilderDTO builder)
        {
            DeleteContactCommandBuilderDTOMapper oBuilderMapper = new();
            DeleteContactCommandDTOMapper oCommandMapper = new();
            IDeleteContactCommandBuilder oBussBuilder;

            oBussBuilder = oBuilderMapper.MapFrom(builder);
            DeleteContactCommand oCommand = (DeleteContactCommand)oBussBuilder.Build();
            return (DeleteContactCommandDTO)oCommandMapper.MapTo(oCommand);
        }
    }
}