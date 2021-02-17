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

namespace AspNetCoreFactory.RazorPages.Pages.Products
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
        [BindProperty]
        public Edit Edit { get; set; } = new Edit();

        [BindProperty]
        public int Id { get; set; }
        public IActionResult OnGet(int id)
        {
            if (id != 0)
            {
                var edit = new Edit { Id = id };
                GetProduct(edit);
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Edit.Id = id;
            PostProduct(Edit);
            return RedirectToPage("/products/list");

        }

        #region Handlers
        private bool GetProduct(Edit model)
        {
            var product = _serviceManager.Product.GetProduct(model.Id, trackChanges: false);
            Map(product, Edit);
            return true;
        }
        private bool PostProduct(Edit model)
        {
            if (model.Id == 0)// new product
            {
                var product = new Product();
                Map(model, product);
                _serviceManager.Product.CreateProduct(product);
                _serviceManager.Save();
                _cache.AddProduct(product);
            }
            else
            {
                var product = _serviceManager.Product.GetProduct(model.Id, trackChanges: false);
                if (product != null)
                {
                    Map(model, product);
                    _serviceManager.Product.UpdateProduct(product);
                    _serviceManager.Save();
                    _cache.UpdateProduct(product);
                }
            }
            return true;
        }
        #endregion

        #region Mappers
        private Edit Map(Product product)
        {
            var model = new Edit();
            Map(product, model);

            return model;
        }

        private void Map(Product product, Edit model)
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
        private void Map(Edit model, Product product)
        {
            product.Name = model.Name;
            product.Price = model.Price;
        }
        #endregion
    }

    #region Models
    public class Edit
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        public int TotalOrders { get; set; }
        public string TotalOrdersFormatted { get; set; }
        public string FormattedPrice { get; set; }
    }
    #endregion
}
