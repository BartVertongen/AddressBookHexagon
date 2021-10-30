//By Bart Vertongen copyright 2021.

using AddressBook.Web.Razor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.Ports;
using PS.AddressBook.Hexagon.Application.UseCases;


namespace AddressBook.Web.Razor.Pages
{
    public class EditModel : PageModel
    {
        private readonly IGetContactWithNameQuery _GetContactPort;
        private readonly IUpdateContactUseCase _UpdatePort;


        [BindProperty]
        public Contact Contact { get; set; }


        public EditModel(IGetContactWithNameQuery getContactPort, IUpdateContactUseCase updatePort)
        {
            _GetContactPort = getContactPort;
            _UpdatePort = updatePort;
        }

        public IActionResult  OnGet(string name)
        {
            IContactDTO oContact;
            oContact = _GetContactPort.GetContactWithName(name);

            if (oContact is null)
                return RedirectToPage("./Index");
            else
            {
                Contact = new Contact
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
                return Page();
            }               
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            UpdateContactCommandBuilder oCommandBuilder = new();
            oCommandBuilder.AddName(Contact.Name).AddPhone(Contact.Phone??"")
                    .AddEmail(Contact.Email??"").AddStreet(Contact.Address.Street??"")
                    .AddPostalCode(Contact.Address.PostalCode??"").AddTown(Contact.Address.Town??"");
            _UpdatePort.UpdateContact((UpdateContactCommand)oCommandBuilder.Build());

            return RedirectToPage("./Index");
        }
    }
}