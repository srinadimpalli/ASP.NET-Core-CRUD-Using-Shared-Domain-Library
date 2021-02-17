using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.Mvc.Areas.Orders.Models
{
    public class List
    {
        // Search parameters

        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }
        public string OrderDateFrom { get; set; }
        public string OrderDateThru { get; set; }

        // Search results

        public List<Detail> Orders { get; set; } = new List<Detail>();
    }
}
