using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Contracts
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrderss(bool trackChanges);
        Order GetOrder(int customerId, int orderId, bool trackChanges);
        Order GetOrderById(int orderId, bool trackChanges);
        void CreateOrder(Order order);
        Task CreateOrderAsync(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync(bool trackChanges);
        Task<Order> GetOrderAsync(int orderId, bool trackChanges);
        int GetCount(bool trackChanges);
    }
}
