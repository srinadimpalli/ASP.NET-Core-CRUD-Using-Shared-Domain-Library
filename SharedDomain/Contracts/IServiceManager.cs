using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Contracts
{
    public interface IServiceManager
    {
        ICustomerService Customer { get; }
        IOrderService Order { get; }
        IProductService Product { get; }
        void Save();
        Task SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> ExecuteSqlRawAsync(string sql, int id);
        void CommitTransaction(IDbContextTransaction transaction);
        IQueryable<Order> GetOrderAsQueryable();
        Task<List<Order>> ToListOrderAsync(IQueryable<Order> query);
        List<Order> ToListOder(IQueryable<Order> query);
    }
}
