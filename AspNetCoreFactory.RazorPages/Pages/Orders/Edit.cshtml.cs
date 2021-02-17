using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreFactory.InfraStructure.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedDomain;
using SharedDomain.Contracts;

namespace AspNetCoreFactory.RazorPages.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly ICache _cache;

        public EditModel(IServiceManager serviceManager, ICache cache)
        {
            _serviceManager = serviceManager;
            _cache = cache;
        }
        public int Id { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Customer is required")]
        public int CustomerId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }
        public IActionResult OnGet(int customerId, int productId)//int customerId, int id
        {
            CustomerId = customerId;
            ProductId = productId;
            //if (id != 0) // Edit Order
            //{
            //    var order = _serviceManager.Order.GetOrderById(id, trackChanges: false);
            //    if (order != null)
            //    {
            //        Id = order.Id;
            //        CustomerId = order.CustomerId;
            //        ProductId = order.ProductId;
            //    }
            //}
            //else
            //{
            //    CustomerId = customerId;
            //}
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Id == 0)
            {
                var orderEntity = new Order
                {
                    CustomerId = CustomerId,
                    OrderDate = DateTime.Now,
                    ProductId = ProductId
                };
                _serviceManager.Order.CreateOrder(orderEntity);
                _serviceManager.Save();
            }
            return RedirectToPage("/Orders/List");
        }
    }


}
