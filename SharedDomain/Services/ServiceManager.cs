using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SharedDomain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly CustOrdProdContext _custOrdProdContext;
        private ICustomerService _customerService;
        private IOrderService _orderService;
        private IProductService _productService;
        public ServiceManager(CustOrdProdContext custOrdProdContext)
        {
            _custOrdProdContext = custOrdProdContext;
        }

        public ICustomerService Customer
        {
            get
            {
                if (_customerService == null)
                    _customerService = new CustomerService(_custOrdProdContext);
                return _customerService;
            }
        }

        public IOrderService Order
        {
            get
            {
                if (_orderService == null)
                    _orderService = new OrderService(_custOrdProdContext);
                return _orderService;
            }
        }
        public IProductService Product
        {
            get
            {
                if (_productService == null)
                    _productService = new ProductService(_custOrdProdContext);
                return _productService;
            }
        }

        /// <summary>
        /// Createa and begin a transaction
        /// </summary>
        /// <returns>IDbContextTransaction </returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _custOrdProdContext.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Execute the give Sql script against the DB
        /// </summary>
        /// <param name="sql">Sql string</param>
        /// <param name="id"> Id of the object</param>
        /// <returns>No of rows effected</returns>
        public async Task<int> ExecuteSqlRawAsync(string sql, int id)
        {
            return await _custOrdProdContext.Database.ExecuteSqlRawAsync(sql, id);
        }

        public void CommitTransaction(IDbContextTransaction transaction)
        {
            transaction.CommitAsync();
        }

        public void Save() => _custOrdProdContext.SaveChanges();

        public async Task SaveAsync() => await _custOrdProdContext.SaveChangesAsync();
        /// <summary>
        /// Return IQueryable<Order> object
        /// </summary>
        /// <returns>IQueryable<T> to query</returns>
        public IQueryable<Order> GetOrderAsQueryable()
        {
            return _custOrdProdContext.Order.AsQueryable();
        }

        public async Task<List<Order>> ToListOrderAsync(IQueryable<Order> query)
        {
            return await query.ToListAsync();
        }


        public List<Order> ToListOder(IQueryable<Order> query)
        {
            return query.ToList();
        }


    }
}
