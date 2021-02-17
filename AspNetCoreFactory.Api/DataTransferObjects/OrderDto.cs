using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.Api.DataTransferObjects
{
    [BindProperties]
    public class OrderDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDateFormatter { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }

        public string OrderDateFrom { get; set; }
        public string OrderDateThru { get; set; }

        public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }
}
