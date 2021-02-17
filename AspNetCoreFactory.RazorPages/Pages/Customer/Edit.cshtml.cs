using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedDomain.Contracts;

namespace AspNetCoreFactory.RazorPages.Pages.Customer
{
    public class EditModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        public EditModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [BindProperty]
        public SharedDomain.Customer CustomerObj { get; set; } = new SharedDomain.Customer();
        [BindProperty]
        public int Id { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id != 0)
            {
                CustomerObj = _serviceManager.Customer.GetCustomer(id, trackChanges: false);
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Id == 0) //new
            {
                var custObj = new SharedDomain.Customer
                {
                    FirstName = CustomerObj.FirstName,
                    LastName = CustomerObj.LastName,
                    Email = CustomerObj.Email,
                    TotalOrders = CustomerObj.TotalOrders
                };
                _serviceManager.Customer.CreateCustomer(custObj);
                _serviceManager.Save();
            }
            else
            {
                var customer = _serviceManager.Customer.GetCustomer(Id, trackChanges: false);
                var custObj = new SharedDomain.Customer
                {
                    Id = customer.Id,
                    FirstName = CustomerObj.FirstName,
                    LastName = CustomerObj.LastName,
                    Email = CustomerObj.Email,
                    TotalOrders = CustomerObj.TotalOrders
                };
                _serviceManager.Customer.UpdateCustomer(custObj);
                _serviceManager.Save();

            }

            return RedirectToPage("/customer/list");
        }

        #region Mapping
        // ** Data Mapper Pattern

        #endregion
    }
}
