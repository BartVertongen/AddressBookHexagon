//By Bart Vertongen copyright 2021.

using System.Net;
using Microsoft.AspNetCore.Mvc;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.Mappers;
using PS.AddressBook.Hexagon.Application.Ports;


namespace AddressBook.Web.Api.Controllers
{
    /// <summary>
    /// A WEB API Controller to expose CreateCommandBuilder as a Service.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class CreateCommandBuilderController : Controller
    {
        /// <summary>
        /// Exposes AddName method of CreateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="name">The Name to add.</param>
        /// <returns>Builder Data</returns>
        [Route("api/addressbook/createcontactcommand/addname/{name}")]
        [HttpGet]
        public ActionResult<CreateContactCommandBuilderDTO> AddName([FromBody]CreateContactCommandBuilderDTO builder, string name)
        {
            CreateContactCommandBuilderDTOMapper oMapper = new();
            ICreateContactCommandBuilder oBussBuilder;

            builder.Name = WebUtility.UrlDecode(name);
            //Remark: Validation is done in the Mapper
            oBussBuilder = oMapper.MapFrom(builder);
            return (CreateContactCommandBuilderDTO)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddPhone method of CreateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="phone">The Phone to add.</param>
        /// <returns>Builder Data</returns>
        [Route("api/addressbook/createcontactcommand/addphone/{phone}")]
        [HttpGet]
        public ActionResult<CreateContactCommandBuilderDTO> AddPhone([FromBody] CreateContactCommandBuilderDTO builder, string phone)
        {
            CreateContactCommandBuilderDTOMapper oMapper = new();
            ICreateContactCommandBuilder oBussBuilder;

            builder.Phone = WebUtility.UrlDecode(phone);
            oBussBuilder = oMapper.MapFrom(builder);
            return (CreateContactCommandBuilderDTO)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddEmail method of CreateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="email">The Email to add.</param>
        /// <returns>Builder Data</returns>
        [Route("api/addressbook/createcontactcommand/addemail/{email}")]
        [HttpGet]
        public ActionResult<CreateContactCommandBuilderDTO> AddEmail([FromBody] CreateContactCommandBuilderDTO builder, string email)
        {
            CreateContactCommandBuilderDTOMapper oMapper = new();
            ICreateContactCommandBuilder oBussBuilder;

            builder.Email = WebUtility.UrlDecode(email);
            oBussBuilder = oMapper.MapFrom(builder);
            return (CreateContactCommandBuilderDTO)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddStreet method of CreateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="street">The Street to add.</param>
        /// <returns>Builder Data</returns>
        [Route("api/addressbook/createcontactcommand/addstreet/{street}")]
        [HttpGet]
        public ActionResult<CreateContactCommandBuilderDTO> AddStreet([FromBody] CreateContactCommandBuilderDTO builder, string street)
        {
            CreateContactCommandBuilderDTOMapper oMapper = new();
            ICreateContactCommandBuilder oBussBuilder;

            builder.Street = WebUtility.UrlDecode(street);
            oBussBuilder = oMapper.MapFrom(builder);
            return (CreateContactCommandBuilderDTO)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddPostalCode method of CreateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="postalCode">The PostalCode to add.</param>
        /// <returns>Builder Data</returns>
        [Route("api/addressbook/createcontactcommand/addpostalcode/{postalcode}")]
        [HttpGet]
        public ActionResult<CreateContactCommandBuilderDTO> AddPostalCode([FromBody] CreateContactCommandBuilderDTO builder, string postalCode)
        {
            CreateContactCommandBuilderDTOMapper oMapper = new();
            ICreateContactCommandBuilder oBussBuilder;

            builder.PostalCode = WebUtility.UrlDecode(postalCode);
            oBussBuilder = oMapper.MapFrom(builder);
            return (CreateContactCommandBuilderDTO)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddTown method of CreateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Create Contact Command Builder Data</param>
        /// <param name="town">The Town to add.</param>
        /// <returns>Create Contact Command Builder Data</returns>
        [Route("api/addressbook/createcontactcommand/addtown/{town}")]
        [HttpGet]
        public ActionResult<CreateContactCommandBuilderDTO> AddTown([FromBody] CreateContactCommandBuilderDTO builder, string town)
        {
            CreateContactCommandBuilderDTOMapper oMapper = new();
            ICreateContactCommandBuilder oBussBuilder;

            builder.Town = WebUtility.UrlDecode(town);
            oBussBuilder = oMapper.MapFrom(builder);
            return (CreateContactCommandBuilderDTO)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes Build method of CreateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Create Contact Command Builder Data</param>
        /// <returns>Create Contact Command Data</returns>
        [Route("api/addressbook/createcontactcommand/build")]
        [HttpGet]
        public ActionResult<CreateContactCommandDTO> Build([FromBody] CreateContactCommandBuilderDTO builder)
        {
            CreateContactCommandBuilderDTOMapper oBuilderMapper = new();
            CreateContactCommandDTOMapper oCommandMapper = new();
            ICreateContactCommandBuilder oBuilder;

            oBuilder = oBuilderMapper.MapFrom(builder);
            CreateContactCommand oCommand = (CreateContactCommand)oBuilder.Build();
            return (CreateContactCommandDTO)oCommandMapper.MapTo(oCommand);
        }
    }
}