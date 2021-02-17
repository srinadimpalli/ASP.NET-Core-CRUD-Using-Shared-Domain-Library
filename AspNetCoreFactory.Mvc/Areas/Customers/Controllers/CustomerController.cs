using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedDomain;
using AspNetCoreFactory.InfraStructure.Attributes;
using AspNetCoreFactory.InfraStructure.Caching;
using Microsoft.EntityFrameworkCore;
using AspNetCoreFactory.Mvc.Areas.Customers.Models;
using SharedDomain.Services;
using SharedDomain.Contracts;
using SharedDomain.Data;

namespace AspNetCoreFactory.Mvc.Areas.Customers.Controllers
{
    [Menu("Customers")]
    [Route("Customers")]
    [Area("Customers")]
    public class CustomerController : Controller
    {
        #region Dependency Injection
        private readonly IServiceManager _serviceManager;
        private readonly ICache _cache;
        // ** Dependency Injection Pattern
        public CustomerController(IServiceManager serviceManager, ICache cache)
        {
            _cache = cache;
            _serviceManager = serviceManager;
        }
        #endregion

        #region Pages
        [HttpGet]
        public async Task<IActionResult> List(List model)
        {
            await GetAsync(model);
            return View(model);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(Detail model)
        {
            await GetAsync(model);
            return View(model);
        }

        [HttpGet("edit/{id?}")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = new Edit { Id = id };
            await GetAsync(model);
            return View(model);

        }

        [HttpPost("edit/{id?}")]
        public async Task<IActionResult> Edit(Edit model)
        {
            if (ModelState.IsValid)
            {
                await PostAsync(model);
                return RedirectToAction("List");
            }
            return View(model);
        }
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Delete model)
        {
            await PostAsync(model);
            return RedirectToAction("List");
        }

        #endregion

        #region Handlers
        private async Task<bool> GetAsync(List model)
        {
            var customers = await _serviceManager.Customer.GetAllCustomersAsync(trackChanges: false);
            foreach (var customer in customers)
            {
                model.Customers.Add(Map(customer));
            }

            return true;
        }

        private async Task<bool> GetAsync(Detail model)
        {
            var customer = await _serviceManager.Customer.GetCustomerAsync(model.Id, trackChanges: false);
            Map(customer, model);
            return true;
        }

        private async Task<bool> GetAsync(Edit model)
        {
            var customer = await _serviceManager.Customer.GetCustomerAsync(model.Id, trackChanges: false);
            Map(customer, model);
            return true;
        }

        private async Task<bool> PostAsync(Edit model)
        {
            if (model.Id == 0) // New Customer
            {
                await CreateNewCustomer(model);
            }
            else
            {
                await UpdateCustomer(model);
            }

            return true;
        }

        private async Task UpdateCustomer(Edit model)
        {
            var customer = await _serviceManager.Customer.GetCustomerAsync(model.Id, trackChanges: false);
            Map(model, customer);

            // ** Unit-of-work pattern

            /// Transaction management works for SqlServer EF core not in-memory db.
            /// uncomment the below lines if you use SqlServer 

            //using (var transaction = await _serviceManager.BeginTransactionAsync())
            //{
            _serviceManager.Customer.UpdateCustomer(customer);
            await _serviceManager.SaveAsync();

            //await _serviceManager.ExecuteSqlRawAsync(Sql.UpdateTotalOrdersForCustomer, customer.Id);
            //  _serviceManager.CommitTransaction(transaction);
            // }

            _cache.UpdateCustomer(customer);
        }

        private async Task CreateNewCustomer(Edit model)
        {
            var customer = new Customer();
            Map(model, customer);

            // ** Unit of work pattern
            //using (var transaction = await _serviceManager.BeginTransactionAsync())
            //{
            await _serviceManager.Customer.CreateCustomerAsync(customer);
            await _serviceManager.SaveAsync();

            //await _serviceManager.ExecuteSqlRawAsync(Sql.UpdateTotalOrdersForCustomer, customer.Id);
            //  _serviceManager.CommitTransaction(transaction);

            // }
            _cache.AddCustomer(customer);
        }

        private async Task PostAsync(Delete model)
        {
            var customer = await _serviceManager.Customer.GetCustomerAsync(model.Id, trackChanges: false);
            if (customer != null)
            {
                _serviceManager.Customer.DeleteCustomer(customer);
                _serviceManager.Save();

                _cache.DeleteCustomer(customer);
            }
        }
        #endregion

        #region Mapping
        // ** Data Mapper pattern
        private Detail Map(Customer customer)
        {
            var model = new Detail();
            Map(customer, model);
            return model;

        }

        private void Map(Customer customer, Detail model)
        {
            if (customer != null)
            {
                model.Id = customer.Id;
                model.FirstName = customer.FirstName;
                model.LastName = customer.LastName;
                model.FullName = customer.FirstName + " " + customer.LastName;
                model.Email = customer.Email;
                model.TotalOrders = customer.TotalOrders;
                model.TotalOrdersFormatted = customer.TotalOrders == 0 ? "none" : customer.TotalOrders.ToString();
            }
        }
        private void Map(Customer customer, Edit model)
        {
            if (customer != null)
            {
                model.Id = customer.Id;
                model.FirstName = customer.FirstName;
                model.LastName = customer.LastName;
                model.Email = customer.Email;
            }
        }

        private void Map(Edit model, Customer customer)
        {
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;
        }
        #endregion
    }
}
