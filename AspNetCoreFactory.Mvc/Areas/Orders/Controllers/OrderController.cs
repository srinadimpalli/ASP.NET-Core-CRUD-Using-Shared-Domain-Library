using AspNetCoreFactory.InfraStructure.Attributes;
using AspNetCoreFactory.InfraStructure.Caching;
using AspNetCoreFactory.Mvc.Areas.Orders.Models;
using Microsoft.AspNetCore.Mvc;
using SharedDomain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedDomain.Services;
using SharedDomain;

namespace AspNetCoreFactory.Mvc.Areas.Orders.Controllers
{
    [Menu("Orders")]
    [Route("Orders")]
    [Area("Orders")]
    public class OrderController : Controller
    {
        #region Dependency Injection
        private readonly IServiceManager _serviceManager;
        private readonly ICache _cache;
        public OrderController(IServiceManager serviceManager, ICache cache)
        {
            _serviceManager = serviceManager;
            _cache = cache;
        }
        #endregion
        #region Pages
        [HttpGet]
        public async Task<IActionResult> List(List model)
        {
            await GetAsync(model);
            return View(model);
        }

        [HttpGet("edit/{customerId?}/{productId?}")]
        public async Task<IActionResult> Edit(int customerId, int productId)
        {
            var model = new Edit { CustomerId = customerId, ProductId = productId };
            await GetAsync(model);
            return View(model);
        }
        [HttpPost("edit/{customerId?}/{orderId?}")]
        public async Task<IActionResult> Edit([FromForm] Edit model)
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
        #endregion Pages

        #region Handlers

        private async Task<bool> GetAsync(List model)
        {
            // Build dynamic search query
            await BuildDynamicOrderQuery(model);
            return true;
        }
        private async Task<bool> GetAsync(Edit model)
        {
            var order = await _serviceManager.Order.GetOrderAsync(model.Id, trackChanges: false);
            Map(order, model);
            return true;
        }

        private async Task BuildDynamicOrderQuery(List model)
        {
            var query = _serviceManager.GetOrderAsQueryable();
            if (model.CustomerId != null)
            {
                query = query.Where(o => o.CustomerId == model.CustomerId);
            }
            if (model.ProductId != null)
            {
                query = query.Where(o => o.ProductId == model.ProductId);
            }
            if (model.OrderDateFrom != null && DateTime.TryParse(model.OrderDateFrom, out DateTime dateFrom))
            {
                query = query.Where(o => o.OrderDate >= dateFrom);
            }
            if (model.OrderDateThru != null && DateTime.TryParse(model.OrderDateThru,
                                                   out DateTime dateThru))
            {
                query = query.Where(o => o.OrderDate <= dateThru);
            }
            foreach (var order in await _serviceManager.ToListOrderAsync(query))
            {
                model.Orders.Add(Map(order));
            }
        }

        private async Task PostAsync(Edit model)
        {
            var order = new Order
            {
                CustomerId = model.CustomerId,
                ProductId = model.ProductId,
                OrderDate = DateTime.Now
            };

            await _serviceManager.Order.CreateOrderAsync(order);
            await _serviceManager.SaveAsync();

        }

        private async Task PostAsync(Delete model)
        {
            var order = await _serviceManager.Order.GetOrderAsync(model.Id, trackChanges: false);
            if (order != null)
            {
                _serviceManager.Order.DeleteOrder(order);
                _serviceManager.Save();

            }
        }
        #endregion

        #region Mappers
        // ** Data Mapper pattern

        private Detail Map(Order order)
        {
            var model = new Detail();
            Map(order, model);

            return model;
        }

        private void Map(Order order, Detail model)
        {
            if (order != null)
            {
                model.Id = order.Id;
                model.ProductId = order.ProductId;
                model.CustomerId = order.CustomerId;
                model.OrderDateFormatted = order.OrderDate.ToString("dd MMM, yyyy");

                var customer = _cache.Customers[order.CustomerId];
                var product = _cache.Products[order.ProductId];

                model.CustomerName = customer.FullName;
                model.ProductName = product.Name;
            }
        }

        private Edit Map(Order order, Edit model)
        {
            if (order != null)
            {
                model.Id = order.Id;
                model.ProductId = order.ProductId;
                model.CustomerId = order.CustomerId;
            }

            return model;
        }
        #endregion
    }
}
