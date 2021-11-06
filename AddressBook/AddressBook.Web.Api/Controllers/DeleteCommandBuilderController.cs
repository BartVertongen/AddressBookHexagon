//By Bart Vertongen copyright 2021.

using Microsoft.AspNetCore.Mvc;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.Mappers;
using PS.AddressBook.Hexagon.Application.Ports;


namespace AddressBook.Web.Api.Controllers
{
    /// <summary>
    /// A WEB API Controller to expose DeleteCommandBuilder as a Service.
    /// </summary>
    public class DeleteCommandBuilderController : Controller
    {
        /// <summary>
        /// Exposes AddName method of DeleteContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <param name="name">The Name to add.</param>
        /// <returns>Builder Data</returns>
        public IActionResult AddName(DeleteContactCommandBuilderDTO builder, string name)
        {
            DeleteContactCommandBuilderDTOMapper oMapper = new();
            IDeleteContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            oBussBuilder.AddName(name);
            return (IActionResult)oMapper.MapTo(oBussBuilder);
        }

        /// <summary>
        /// Exposes Build method of DeleteContactCommandBuilder.
        /// </summary>
        /// <param name="builder">The Builder Data</param>
        /// <returns>Command Data</returns>
        public IActionResult Build(DeleteContactCommandBuilderDTO builder)
        {
            DeleteContactCommandBuilderDTOMapper oMapper = new();
            IDeleteContactCommandBuilder oBussBuilder;

            oBussBuilder = oMapper.MapFrom(builder);
            return (IActionResult)oBussBuilder.Build();
        }
    }
}