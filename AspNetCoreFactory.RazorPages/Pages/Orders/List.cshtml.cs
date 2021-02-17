using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedDomain.Contracts;
using SharedDomain;
using AspNetCoreFactory.InfraStructure.Caching;
using SharedDomain.Services;

namespace AspNetCoreFactory.RazorPages.Pages.Orders
{
    [BindProperties]
    public class ListModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        private readonly ICache _cache;
        [BindProperty(SupportsGet = true)]
        public int? CustomerId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? ProductId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string OrderDateFrom { get; set; }
        [BindProperty(SupportsGet = true)]
        public string OrderDateThru { get; set; }
        public List<Detail> Orders { get; set; } = new List<Detail>();
        public ListModel(IServiceManager serviceManager, ICache cache)
        {
            _serviceManager = serviceManager;
            _cache = cache;
        }

        public void OnGet()
        {
            //var orders = _serviceManager.Order.GetAllOrderss(trackChanges: false);
            //foreach (var order in orders)
            //{
            //    Orders.Add(Map(order));
            //}
            BuildDynamicOrderQuery(this);

        }

        #region Mappers
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

        //private Edit Map(Order order, Edit model)
        //{
        //    if (order != null)
        //    {
        //        model.Id = order.Id;
        //        model.ProductId = order.ProductId;
        //        model.CustomerId = order.CustomerId;
        //    }

        //    return model;
        //}

        #endregion
        #region Handlers
        private void BuildDynamicOrderQuery(ListModel model)
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
            foreach (var order in _serviceManager.ToListOder(query))
            {
                model.Orders.Add(Map(order));
            }
        }
        #endregion
    }


    #region ViewModels
    public class Detail
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }

        public DateTime OrderDate { get; set; }
        public string OrderDateFormatted { get; set; }

        public string CustomerName { get; set; }
        public string ProductName { get; set; }
    }
    #endregion



}
