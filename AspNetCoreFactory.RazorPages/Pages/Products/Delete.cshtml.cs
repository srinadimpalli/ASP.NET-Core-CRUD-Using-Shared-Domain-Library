using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreFactory.InfraStructure.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedDomain.Contracts;

namespace AspNetCoreFactory.RazorPages.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly ICache _cache;
        public DeleteModel(IServiceManager serviceManager, ICache cache)
        {
            _serviceManager = serviceManager;
            _cache = cache;
        }
        [BindProperty]
        public int Id { get; set; }
        public IActionResult OnGet(int id)
        {
            var product = _serviceManager.Product.GetProduct(id, trackChanges: false);
            _serviceManager.Product.DeleteProduct(product);
            _serviceManager.Save();

            _cache.DeleteProduct(product);
            return RedirectToPage("/products/list");
        }
    }
}
