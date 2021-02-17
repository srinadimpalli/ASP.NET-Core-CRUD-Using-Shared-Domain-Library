using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedDomain
{
    public partial class Product
    {
        public Product()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        public int TotalOrders { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
