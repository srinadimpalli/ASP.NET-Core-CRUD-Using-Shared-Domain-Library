using Microsoft.EntityFrameworkCore;

namespace SharedDomain
{
    public partial class CustOrdProdContext : DbContext
    {
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        public CustOrdProdContext() { }

        public CustOrdProdContext(DbContextOptions<CustOrdProdContext> options) : base(options)
        {

        }
    }
}
