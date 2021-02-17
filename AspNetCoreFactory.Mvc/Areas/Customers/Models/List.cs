using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.Mvc.Areas.Customers.Models
{
    public class List
    {
        public List<Detail> Customers { get; set; } = new List<Detail>();
    }
}
