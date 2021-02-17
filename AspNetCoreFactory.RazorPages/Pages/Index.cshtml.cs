using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedDomain;
using SharedDomain.Contracts;

namespace AspNetCoreFactory.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;
        //private readonly IServiceManager _serviceManager;
        public IEnumerable<SharedDomain.Customer> Customers;
        //public IndexModel(ILogger<IndexModel> logger, IServiceManager serviceManager)
        //{
        //    _logger = logger;
        //    _serviceManager = serviceManager;
        //}

        //public void OnGet()
        //{
        //    Customers = _serviceManager.Customer.GetAllCustomers(trackChanges: false);
        //}
    }
}
