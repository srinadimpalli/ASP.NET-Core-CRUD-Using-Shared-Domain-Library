using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.Mvc.Areas.Products.Models
{
    public class List
    {
        public List<Detail> Products { get; set; } = new List<Detail>();
    }
}
