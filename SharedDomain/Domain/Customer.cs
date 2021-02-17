using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedDomain
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public int TotalOrders { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
