
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS.AddressBook.Hexagon.Application.UseCases;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application;
using AddressBook.Web.Mvc.Models;


namespace AddressBook.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController>    _Logger;
        private readonly IGetContactWithNameQuery   _GetContactPort;
        private readonly IGetOverviewQuery          _GetOverviewPort;
        private readonly IDeleteContactUseCase      _DeletePort;
        private readonly IUpdateContactUseCase      _UpdatePort;
        private readonly ICreateContactUseCase      _CreateContactPort;

        public HomeController(ILogger<HomeController> logger, IGetOverviewQuery overviewPort, 
                ICreateContactUseCase createPort, IUpdateContactUseCase updatePort,
                    IGetContactWithNameQuery contactPort, IDeleteContactUseCase deletePort)
        {
            _Logger = logger;
            _GetOverviewPort = overviewPort;
            _GetContactPort = contactPort;
            _CreateContactPort = createPort;
            _UpdatePort = updatePort;
            _DeletePort = deletePort;
        }

        public IActionResult Index()
        {
            IList<IContactLineDTO> ContactLines = _GetOverviewPort.GetOverview("");
            return View(ContactLines);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            CreateContactCommandBuilder oCommandBuilder = new();
            oCommandBuilder.AddName(contact.Name);
            oCommandBuilder.AddPhone(contact.Phone ?? "");
            oCommandBuilder.AddEmail(contact.Email ?? "");

            oCommandBuilder.AddStreet(contact.Address.Street ?? "")
                    .AddPostalCode(contact.Address.PostalCode ?? "").AddTown(contact.Address.Town ?? "");
            _CreateContactPort.CreateContact((CreateContactCommand)oCommandBuilder.Build());

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string name)
        {
            IContactDTO oContact;
            oContact = _GetContactPort.GetContactWithName(name);

            if (oContact is null)
                return RedirectToAction(nameof(Index));
            else
            {
                Contact CurrentContact = new Contact
                {
                    Name = name,
                    Address = new Address
                    {
                        Street = oContact.Address.Street,
                        PostalCode = oContact.Address.PostalCode,
                        Town = oContact.Address.Town
                    },
                    Phone = oContact.Phone,
                    Email = oContact.Email
                };
                return View(CurrentContact);
            }
        }

        [HttpPost]
        public IActionResult Edit(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                UpdateContactCommandBuilder oCommandBuilder = new();
                oCommandBuilder.AddName(contact.Name).AddPhone(contact.Phone ?? "")
                        .AddEmail(contact.Email ?? "").AddStreet(contact.Address.Street ?? "")
                        .AddPostalCode(contact.Address.PostalCode ?? "").AddTown(contact.Address.Town ?? "");
                _UpdatePort.UpdateContact((UpdateContactCommand)oCommandBuilder.Build());

                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Delete(string name)
        {
            IContactDTO oContact;
            oContact = _GetContactPort.GetContactWithName(name);

            if (oContact != null)
            {
                DeleteContactCommand oCommand = new(oContact.Name);
                if (_DeletePort.DeleteContact(oCommand) is null)
                {
                    //REM Something went wrong
                    //Go to error Page ?
                    return RedirectToAction(nameof(Error));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}