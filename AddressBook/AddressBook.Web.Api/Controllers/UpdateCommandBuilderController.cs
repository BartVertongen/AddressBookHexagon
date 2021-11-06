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
    public class UpdateCommandBuilderController : Controller
    {
        /// <summary>
        /// Exposes AddName method of UpdateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="name">The Name to add.</param>
        /// <returns>Builder Data</returns>
        public IActionResult AddName(UpdateContactCommandBuilderDTO builder, string name)
        {
            UpdateContactCommandBuilderDTOMapper oMapper = new();
            IUpdateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            oBussBuilder.AddName(name);
            return (IActionResult)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddPhone method of UpdateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="phone">The Phone to add.</param>
        /// <returns>Builder Data</returns>
        public IActionResult AddPhone(UpdateContactCommandBuilderDTO builder, string phone)
        {
            UpdateContactCommandBuilderDTOMapper oMapper = new();
            IUpdateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            oBussBuilder.AddPhone(phone);
            return (IActionResult)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddEmail method of UpdateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="email">The Email to add.</param>
        /// <returns>Builder Data</returns>
        public IActionResult AddEmail(UpdateContactCommandBuilderDTO builder, string email)
        {
            UpdateContactCommandBuilderDTOMapper oMapper = new();
            IUpdateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            oBussBuilder.AddEmail(email);
            return (IActionResult)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddStreet method of UpdateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="street">The Street to add.</param>
        /// <returns>Builder Data</returns>
        public IActionResult AddStreet(UpdateContactCommandBuilderDTO builder, string street)
        {
            UpdateContactCommandBuilderDTOMapper oMapper = new();
            IUpdateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            oBussBuilder.AddStreet(street);
            return (IActionResult)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddPostalCode method of UpdateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="postalCode">The PostalCode to add.</param>
        /// <returns>Builder Data</returns>
        public IActionResult AddPostalCode(UpdateContactCommandBuilderDTO builder, string postalCode)
        {
            UpdateContactCommandBuilderDTOMapper oMapper = new();
            IUpdateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            oBussBuilder.AddPostalCode(postalCode);
            return (IActionResult)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes AddTown method of UpdateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="town">The Town to add.</param>
        /// <returns>Builder Data</returns>
        public IActionResult AddTown(UpdateContactCommandBuilderDTO builder, string town)
        {
            UpdateContactCommandBuilderDTOMapper oMapper = new();
            IUpdateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            oBussBuilder.AddTown(town);
            return (IActionResult)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes Build method of UpdateContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <returns>Command Data</returns>
        public IActionResult Build(UpdateContactCommandBuilderDTO builder)
        {
            UpdateContactCommandBuilderDTOMapper oMapper = new();
            IUpdateContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            return (IActionResult)/*(CreateContactCommand)*/oBussBuilder.Build();
        }
    }
}