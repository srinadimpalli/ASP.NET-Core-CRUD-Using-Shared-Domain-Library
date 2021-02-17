using AspNetCoreFactory.InfraStructure.Attributes;
using AspNetCoreFactory.InfraStructure.Caching;
using AspNetCoreFactory.Mvc.Areas.Products.Models;
using Microsoft.AspNetCore.Mvc;
using SharedDomain;
using SharedDomain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.Mvc.Areas.Products
{
    [Menu("Products")]
    [Route("products")]
    [Area("Products")]
    public class ProductController : Controller
    {
        #region Dependency Injection
        private readonly IServiceManager _serviceManager;
        private readonly ICache _cache;

        public ProductController(IServiceManager serviceManager, ICache cache)
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
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Delete model)
        {
            await PostAsync(model);
            return RedirectToAction("List");
        }

        #endregion

        #region Handlers
        private async Task<bool> GetAsync(List model)
        {
            foreach (var product in await _serviceManager.Product.GetAllProductsAsync(trackChanges: false))
            {
                model.Products.Add(Map(product));
            }
            return true;
        }

        private async Task<bool> GetAsync(Detail model)
        {
            var product = await _serviceManager.Product.GetProductAsync(model.Id, trackChanges: false);
            Map(product, model);

            return true;
        }

        private async Task<bool> GetAsync(Edit model)
        {
            var product = await _serviceManager.Product.GetProductAsync(model.Id, trackChanges: false);
            Map(product, model);
            return true;
        }
        private async Task<bool> PostAsync(Edit model)
        {
            if (model.Id == 0) // New Product
            {
                var product = new Product();
                Map(model, product);

                await _serviceManager.Product.CreateProductAsync(product);
                await _serviceManager.SaveAsync();
                _cache.AddProduct(product);

            }
            else
            {
                var product = await _serviceManager.Product.GetProductAsync(model.Id, trackChanges: false);
                Map(model, product);

                _serviceManager.Product.UpdateProduct(product);
                _serviceManager.Save();

                _cache.UpdateProduct(product);

            }
            return true;
        }

        private async Task<bool> PostAsync(Delete model)
        {
            var product = await _serviceManager.Product.GetProductAsync(model.Id, trackChanges: false);

            if (product.TotalOrders == 0)
            {
                _serviceManager.Product.DeleteProduct(product);
                _serviceManager.Save();
                _cache.DeleteProduct(product);
            }
            return true;
        }
        #endregion

        #region Mappers
        // ** Data Mapper pattern

        private Detail Map(Product product)
        {
            var model = new Detail();
            Map(product, model);

            return model;
        }

        private void Map(Product product, Detail model)
        {
            if (product != null)
            {
                model.Id = product.Id;
                model.Name = product.Name;
                model.Price = product.Price;
                model.FormattedPrice = product.Price.ToString("c");
                model.TotalOrders = product.TotalOrders;
                model.TotalOrdersFormatted = product.TotalOrders == 0 ? "none" : product.TotalOrders.ToString();
            }
        }

        private void Map(Product product, Edit model)
        {
            if (product != null)
            {
                model.Id = product.Id;
                model.Name = product.Name;
                model.Price = product.Price;
            }
        }

        // ** Data Mapper pattern

        private void Map(Edit model, Product product)
        {
            product.Name = model.Name;
            product.Price = model.Price;
        }
        #endregion
    }
}
