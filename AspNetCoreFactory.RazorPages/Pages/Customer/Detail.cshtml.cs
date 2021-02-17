using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedDomain.Contracts;

namespace AspNetCoreFactory.RazorPages.Pages.Customer
{
    public class DetailModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        public DetailModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        public SharedDomain.Customer CustomerObj { get; set; } = new SharedDomain.Customer();


        public void OnGet(int id)
        {
            CustomerObj = _serviceManager.Customer.GetCustomer(id, trackChanges: false);

        }
    }
}
