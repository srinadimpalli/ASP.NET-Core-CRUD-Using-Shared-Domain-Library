using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedDomain
{
    public partial class Order
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Customer is required")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
