
using AddressBook.Web.Razor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PS.AddressBook.Hexagon.Application;
using PS.AddressBook.Hexagon.Application.Commands;
using PS.AddressBook.Hexagon.Application.Mappers;
using PS.AddressBook.Hexagon.Application.UseCases;


namespace AddressBook.Web.Razor.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ICreateContactUseCase _CreateContactPort;

        [BindProperty]
        public Contact Contact { get; set; }

        public CreateModel(ICreateContactUseCase createPort)
        {
            _CreateContactPort = createPort;
        }

        /// <summary>
        /// The Get for Create has nothing to initialize
        /// </summary>
        public void OnGet()
        { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            CreateContactCommand oCommand;
            CreateContactCommandDTOMapper oMapper = new();
            CreateContactCommandBuilder oCommandBuilder = new();
            oCommandBuilder.AddName(Contact.Name);
            oCommandBuilder.AddPhone(Contact.Phone??"");
            oCommandBuilder.AddEmail(Contact.Email??"");

            oCommandBuilder.AddStreet(Contact.Address.Street??"")
                    .AddPostalCode(Contact.Address.PostalCode??"").AddTown(Contact.Address.Town??"");
            oCommand = (CreateContactCommand)oCommandBuilder.Build();

            _CreateContactPort.CreateContact(oMapper.MapTo(oCommand));

            return RedirectToPage("./Index");
        }
    }
}