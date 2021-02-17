using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public int TotalOrders { get; set; }
        public string TotalOrdersFormatted { get; set; }
        public string FormattedPrice { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}
