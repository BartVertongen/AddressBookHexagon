//By Bart Vertongen copyright 2021.

using Microsoft.AspNetCore.Mvc;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.Mappers;
using PS.AddressBook.Hexagon.Application.Ports;


namespace AddressBook.Web.Api.Controllers
{
    /// <summary>
    /// A WEB API Controller to expose CreateCommandBuilder as a Service.
    /// </summary>
    public class CreateCommandBuilderController : Controller
    {
        /// <summary>
        /// Exposes AddName method of CreateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="name">The Name to add.</param>
        /// <returns>Builder Data</returns>
        public IActionResult AddName(CreateContactCommandBuilderDTO builder, string name)
        {
            CreateContactCommandBuilderDTOMapper oMapper = new();
            ICreateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            oBussBuilder.AddName(name);
            return (IActionResult)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddPhone method of CreateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="phone">The Phone to add.</param>
        /// <returns>Builder Data</returns>
        public IActionResult AddPhone(CreateContactCommandBuilderDTO builder, string phone)
        {
            CreateContactCommandBuilderDTOMapper oMapper = new();
            ICreateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            oBussBuilder.AddPhone(phone);
            return (IActionResult)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddEmail method of CreateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="email">The Email to add.</param>
        /// <returns>Builder Data</returns>
        public IActionResult AddEmail(CreateContactCommandBuilderDTO builder, string email)
        {
            CreateContactCommandBuilderDTOMapper oMapper = new();
            ICreateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            oBussBuilder.AddEmail(email);
            return (IActionResult)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddStreet method of CreateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="street">The Street to add.</param>
        /// <returns>Builder Data</returns>
        public IActionResult AddStreet(CreateContactCommandBuilderDTO builder, string street)
        {
            CreateContactCommandBuilderDTOMapper oMapper = new();
            ICreateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            oBussBuilder.AddStreet(street);
            return (IActionResult)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddPostalCode method of CreateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="postalCode">The PostalCode to add.</param>
        /// <returns>Builder Data</returns>
        public IActionResult AddPostalCode(CreateContactCommandBuilderDTO builder, string postalCode)
        {
            CreateContactCommandBuilderDTOMapper oMapper = new();
            ICreateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            oBussBuilder.AddPostalCode(postalCode);
            return (IActionResult)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddTown method of CreateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="town">The Town to add.</param>
        /// <returns>Builder Data</returns>
        public IActionResult AddTown(CreateContactCommandBuilderDTO builder, string town)
        {
            CreateContactCommandBuilderDTOMapper oMapper = new();
            ICreateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            oBussBuilder.AddTown(town);
            return (IActionResult)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes Build method of CreateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <returns>Command Data</returns>
        public IActionResult Build(CreateContactCommandBuilderDTO builder)
        {
            CreateContactCommandBuilderDTOMapper oMapper = new();
            ICreateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            return (IActionResult)/*(CreateContactCommand)*/oBussBuilder.Build();
        }
    }
}