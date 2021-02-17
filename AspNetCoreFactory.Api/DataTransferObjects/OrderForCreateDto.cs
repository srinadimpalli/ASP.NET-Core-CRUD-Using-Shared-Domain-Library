using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.Api.DataTransferObjects
{
    public class OrderForCreateDto
    {
        [Required(ErrorMessage = "Customer Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Customer Id should be greater than 0.")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Product Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Product Id should be greater than 0.")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Order Date is required.")]
        public DateTime OrderDate { get; set; }
    }
}
