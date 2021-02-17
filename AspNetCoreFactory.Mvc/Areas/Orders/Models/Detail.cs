using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.Mvc.Areas.Orders.Models
{
    // ** DTO (Date Transfer Object) pattern

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
}
