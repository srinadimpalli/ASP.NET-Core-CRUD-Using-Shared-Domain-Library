using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreFactory.InfraStructure.Caching
{
    public class Lookup : ILookup
    {
        private readonly ICache _cache;
        public Lookup(ICache cache)
        {
            _cache = cache;
        }

        public List<SelectListItem> CustomerItems
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "", Text = "-- Select --", Selected = true });
                foreach (var customer in _cache.Customers.Values)
                {
                    list.Add(new SelectListItem { Value = customer.Id.ToString(), Text = customer.FullName });
                }
                return list;
            }
        }
        // Dropdown selection list for products

        public List<SelectListItem> ProductItems
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "", Text = "-- Select --", Selected = true });
                foreach (var product in _cache.Products.Values)
                    list.Add(new SelectListItem { Value = product.Id.ToString(), Text = product.Name });

                return list;
            }
        }
    }
}
