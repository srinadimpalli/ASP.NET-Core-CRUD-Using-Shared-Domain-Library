using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedDomain.Contracts;

namespace AspNetCoreFactory.RazorPages.Pages.Orders
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
            var orderEntity = _serviceManager.Order.GetOrderById(id, trackChanges: false);
            _serviceManager.Order.DeleteOrder(orderEntity);
            _serviceManager.Save();
            return RedirectToPage("/Orders/List");


        }
    }
}
