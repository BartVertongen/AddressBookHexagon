//By Bart Vertongen copyright 2021

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.UseCases;


namespace AddressBook.Web.Razor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _Logger;
        private readonly IGetOverviewQuery _GetOverviewPort;
        private readonly IDeleteContactUseCase _DeletePort;

        public IndexModel(IGetOverviewQuery getOverviewPort, IDeleteContactUseCase deletePort, ILogger<IndexModel> logger)
        {
            _Logger = logger;
            _GetOverviewPort = getOverviewPort;
            _DeletePort = deletePort;
        }

        public IList<IContactLineDTO> Contacts { get; set; }

        public void OnGet()
        {
            Contacts = _GetOverviewPort.GetOverview("");
        }

        public IActionResult OnPostDelete(int id)
        {
            var contact = Contacts[id];

            if (contact != null)
            {
                DeleteContactCommand oCommand = new(contact.Name);
                if (_DeletePort.DeleteContact(oCommand) is null)
                {
                    //REM Something went wrong
                    //Go to error Page ?
                }
            }
            return RedirectToPage();
        }
    }
}