using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedDomain.Contracts;

namespace AspNetCoreFactory.RazorPages.Pages.Products
{
    public class DetailModel : PageModel
    {
        private readonly IServiceManager _serviceManager;

        public DetailModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public decimal Price { get; set; }

        public void OnGet(int id)
        {
            var product = _serviceManager.Product.GetProduct(id, trackChanges: false);
            if (product != null)
            {
                Name = product.Name;
                Price = product.Price;
            }
        }
    }
}
