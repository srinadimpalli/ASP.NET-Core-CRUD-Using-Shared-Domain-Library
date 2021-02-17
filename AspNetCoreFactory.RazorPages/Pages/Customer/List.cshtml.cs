using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SharedDomain;
using SharedDomain.Contracts;

namespace AspNetCoreFactory.RazorPages.Pages.Customer
{
    public class ListModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        public IEnumerable<SharedDomain.Customer> Customers;
        public ListModel(IServiceManager serviceManager)
        {

            _serviceManager = serviceManager;
        }

        public void OnGet()
        {
            Customers = _serviceManager.Customer.GetAllCustomers(trackChanges: false);
        }
    }
}
