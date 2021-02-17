using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreFactory.InfraStructure.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedDomain.Contracts;

namespace AspNetCoreFactory.RazorPages.Pages.Admin
{
    public class ListModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly ICache _cache;

        public ListModel(IServiceManager serviceManager, ICache cache)
        {
            _serviceManager = serviceManager;
            _cache = cache;
        }
        public string Work { get; set; }
        public string Result { get; set; }

        public int TotalCustomers { get; set; }
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }

        public void OnGet()
        {
            TotalCustomers = _serviceManager.Customer.GetCount(trackChanges: false);
            TotalProducts = _serviceManager.Product.GetCount(trackChanges: false);
            TotalOrders = _serviceManager.Order.GetCount(trackChanges: false);
            if (Work == "calculate")
            {

            }
        }
    }
}
