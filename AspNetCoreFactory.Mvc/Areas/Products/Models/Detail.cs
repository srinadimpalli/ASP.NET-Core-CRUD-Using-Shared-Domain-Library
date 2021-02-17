using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.Mvc.Areas.Products.Models
{
    public class Detail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int TotalOrders { get; set; }
        public string TotalOrdersFormatted { get; set; }
        public string FormattedPrice { get; set; }
    }
}
