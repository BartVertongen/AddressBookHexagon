//By Bart Vertongen copyright 2021.

using Microsoft.AspNetCore.Mvc;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.Mappers;
using PS.AddressBook.Hexagon.Application.Ports;


namespace AddressBook.Web.Api.Controllers
{
    /// <summary>
    /// A WEB API Controller to expose UpdateCommandBuilder as a Service.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class UpdateCommandBuilderController : Controller
    {
        /// <summary>
        /// Exposes AddName method of UpdateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="name">The Name to add.</param>
        /// <returns>Builder Data</returns>
        [Route("api/addressbook/updatecontactcommand/addname/{name}")]
        [HttpGet]
        public ActionResult<UpdateContactCommandBuilderDTO> AddName([FromBody] UpdateContactCommandBuilderDTO builder, string name)
        {
            UpdateContactCommandBuilderDTOMapper oMapper = new();
            IUpdateContactCommandBuilder oBuilder;

            builder.Name = name;
            oBuilder = oMapper.MapFrom(builder);
            return (UpdateContactCommandBuilderDTO)oMapper.MapTo(oBuilder);
        }

        /// <summary>
        /// Exposes AddPhone method of UpdateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="phone">The Phone to add.</param>
        /// <returns>Builder Data</returns>
        [Route("api/addressbook/updatecontactcommand/addphone/{phone}")]
        [HttpGet]
        public ActionResult<UpdateContactCommandBuilderDTO> AddPhone([FromBody] UpdateContactCommandBuilderDTO builder, string phone)
        {
            UpdateContactCommandBuilderDTOMapper oMapper = new();
            IUpdateContactCommandBuilder oBussBuilder;

            builder.Phone = phone;
            oBussBuilder = oMapper.MapFrom(builder);
            return (UpdateContactCommandBuilderDTO)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddEmail method of UpdateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="email">The Email to add.</param>
        /// <returns>Builder Data</returns>
        [Route("api/addressbook/updatecontactcommand/addemail/{email}")]
        [HttpGet]
        public ActionResult<UpdateContactCommandBuilderDTO> AddEmail([FromBody] UpdateContactCommandBuilderDTO builder, string email)
        {
            UpdateContactCommandBuilderDTOMapper oMapper = new();
            IUpdateContactCommandBuilder oBussBuilder;

            builder.Email = email;
            oBussBuilder = oMapper.MapFrom(builder);
            return (UpdateContactCommandBuilderDTO)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddStreet method of UpdateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="street">The Street to add.</param>
        /// <returns>Builder Data</returns>
        [Route("api/addressbook/updatecontactcommand/addstreet/{street}")]
        [HttpGet]
        public ActionResult<UpdateContactCommandBuilderDTO> AddStreet([FromBody] UpdateContactCommandBuilderDTO builder, string street)
        {
            UpdateContactCommandBuilderDTOMapper oMapper = new();
            IUpdateContactCommandBuilder oBussBuilder;

            builder.Street = street;
            oBussBuilder = oMapper.MapFrom(builder);
            return (UpdateContactCommandBuilderDTO)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddPostalCode method of UpdateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="postalCode">The PostalCode to add.</param>
        /// <returns>Builder Data</returns>
        [Route("api/addressbook/updatecontactcommand/addpostalcode/{postalCode}")]
        [HttpGet]
        public ActionResult<UpdateContactCommandBuilderDTO> AddPostalCode([FromBody] UpdateContactCommandBuilderDTO builder, string postalCode)
        {
            UpdateContactCommandBuilderDTOMapper oMapper = new();
            IUpdateContactCommandBuilder oBussBuilder;

            builder.PostalCode = postalCode;
            oBussBuilder = oMapper.MapFrom(builder);
            return (UpdateContactCommandBuilderDTO)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddTown method of UpdateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="town">The Town to add.</param>
        /// <returns>Builder Data</returns>
        [Route("api/addressbook/updatecontactcommand/addpostalcode/{town}")]
        [HttpGet]
        public ActionResult<UpdateContactCommandBuilderDTO> AddTown([FromBody] UpdateContactCommandBuilderDTO builder, string town)
        {
            UpdateContactCommandBuilderDTOMapper oMapper = new();
            IUpdateContactCommandBuilder oBussBuilder;

            builder.Town = town;
            oBussBuilder = oMapper.MapFrom(builder);
            return (UpdateContactCommandBuilderDTO)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes Build method of UpdateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <returns>Command Data</returns>
        [Route("api/addressbook/updatecontactcommand/build")]
        [HttpGet]
        public ActionResult<UpdateContactCommandDTO> Build([FromBody] UpdateContactCommandBuilderDTO builder)
        {
            UpdateContactCommandBuilderDTOMapper oMapper = new();
            IUpdateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            return (UpdateContactCommandDTO)oBussBuilder.Build();
        }
    }
}