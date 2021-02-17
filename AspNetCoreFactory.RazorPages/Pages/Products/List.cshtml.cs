using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreFactory.InfraStructure.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedDomain;
using SharedDomain.Contracts;

namespace AspNetCoreFactory.RazorPages.Pages.Products
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
        public List<Detail> Products { get; set; } = new List<Detail>();
        public IActionResult OnGet()
        {
            GetProducts();
            return Page();
        }

        #region Handlers
        private bool GetProducts()
        {
            foreach (var product in _serviceManager.Product.GetAllProducts(trackChanges: false))
            {
                Products.Add(Map(product));
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
        #endregion
    }

    #region Models
    public class Detail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int TotalOrders { get; set; }
        public string TotalOrdersFormatted { get; set; }
        public string FormattedPrice { get; set; }
    }
    #endregion
}
