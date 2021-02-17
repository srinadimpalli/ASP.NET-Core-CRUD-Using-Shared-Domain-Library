using Microsoft.EntityFrameworkCore;
using SharedDomain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Services
{
    public class OrderService : ServiceBase<Order>, IOrderService
    {
        public OrderService(CustOrdProdContext custOrdProdContext) : base(custOrdProdContext)
        {

        }
        public void CreateOrder(Order order)
        {
            Create(order);
        }

        public async Task CreateOrderAsync(Order order)
        {
            await CreateAsync(order);
        }

        public void DeleteOrder(Order order)
        {
            Delete(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
          .OrderBy(c => c.OrderDate)
          .ToListAsync();
        }

        public IEnumerable<Order> GetAllOrderss(bool trackChanges)
        {
            return FindAll(trackChanges)
                 .OrderBy(c => c.OrderDate)
                 .ToList();
        }

        public Order GetOrder(int customerId, int orderId, bool trackChanges)
        {
            return FindByCondition(c => c.CustomerId.Equals(customerId) && c.Id.Equals(orderId), trackChanges)
                   .SingleOrDefault();
        }

        public async Task<Order> GetOrderAsync(int orderId, bool trackChanges)
        {
            return await FindByCondition(c => c.Id.Equals(orderId), trackChanges)
                   .SingleOrDefaultAsync();
        }

        public Order GetOrderById(int orderId, bool trackChanges)
        {
            return FindByCondition(o => o.Id.Equals(orderId), trackChanges)
                    .SingleOrDefault();
        }

        public void UpdateOrder(Order order)
        {
            Update(order);
        }
        public int GetCount(bool trackChanges)
        {
            return FindAll(trackChanges).Count();
        }
    }
}
