using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.Api.DataTransferObjects
{
    public class ProductForCreateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        public int TotalOrders { get; set; }
        public string TotalOrdersFormatted { get; set; }
        public string FormattedPrice { get; set; }
    }
}
