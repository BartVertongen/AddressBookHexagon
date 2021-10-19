using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Web.Razor.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _Logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _Logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
