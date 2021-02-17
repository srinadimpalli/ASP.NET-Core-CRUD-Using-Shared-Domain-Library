using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedDomain.Contracts;

namespace AspNetCoreFactory.RazorPages.Pages.Customer
{
    public class DeleteModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        public DeleteModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [BindProperty]
        public int Id { get; set; }
        public IActionResult OnGet(int id)
        {
            var custoObj = _serviceManager.Customer.GetCustomer(id, trackChanges: false);
            _serviceManager.Customer.DeleteCustomer(custoObj);
            _serviceManager.Save();
            return RedirectToPage("/customer/list");
        }
    }
}
