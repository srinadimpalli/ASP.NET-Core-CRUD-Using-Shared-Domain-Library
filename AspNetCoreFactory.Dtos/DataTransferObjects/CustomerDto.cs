using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public string TotalOrdersFormatted { get; set; }
        public int TotalOrders { get; set; }
        public string FullName { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}
