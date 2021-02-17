using Microsoft.Extensions.Caching.Memory;
using SharedDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetCoreFactory.InfraStructure.Caching
{
    public class Cache : ICache
    {
        #region Dependency Injection
        private readonly CustOrdProdContext _db;
        private readonly IMemoryCache _memoryCache;
        public Cache(CustOrdProdContext db, IMemoryCache memoryCache)
        {
            _db = db;
            _memoryCache = memoryCache;
        }
        #endregion

        #region Cache Management
        private static object locker = new object();
        private static readonly string CustomersKey = "CustomerKey";
        private static readonly string ProductsKey = "ProductsKey";

        private static readonly HashSet<string> UsedKeys = new HashSet<string>();
        #endregion

        #region Cache
        // ** Identity Map Pattern
        public Dictionary<int, Customer> Customers
        {
            get
            {
                // ** Lazy Load Pattern
                var dictionary = _memoryCache.Get(CustomersKey) as Dictionary<int, Customer>;
                if (dictionary == null)
                {
                    lock (locker)
                    {
                        dictionary = _db.Customer.OrderBy(c => c.LastName).ToDictionary(a => a.Id);
                        Add(CustomersKey, dictionary, DateTime.Now.AddHours(2));
                    }
                }
                return dictionary;
            }
        }

        public void AddCustomer(Customer customer)
        {
            lock (locker)
            {
                if (!Customers.ContainsKey(customer.Id))
                    Customers.Add(customer.Id, customer);
            }
        }

        public void ClearCustomers()
        {
            Clear(CustomersKey);
        }

        public void DeleteCustomer(Customer customer)
        {
            lock (locker)
            {
                Customers.Remove(customer.Id);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            lock (locker)
            {
                Customers[customer.Id] = customer;
            }
        }

        // ** Identity Map pattern

        public Dictionary<int, Product> Products
        {
            get
            {
                // ** Lazy Load pattern 

                var dictionary = _memoryCache.Get(ProductsKey) as Dictionary<int, Product>;
                if (dictionary == null)
                {
                    lock (locker)
                    {
                        dictionary = _db.Product.OrderBy(c => c.Name).ToDictionary(a => a.Id);
                        Add(ProductsKey, dictionary, DateTime.Now.AddHours(2));
                    }
                }

                return dictionary;
            }
        }

        public void AddProduct(Product product)
        {
            lock (locker)
            {
                if (!Products.ContainsKey(product.Id))
                    Products.Add(product.Id, product);
            }
        }

        public void UpdateProduct(Product product)
        {
            lock (locker)
            {
                Products[product.Id] = product;
            }
        }

        public void DeleteProduct(Product product)
        {
            lock (locker)
            {
                Products.Remove(product.Id);
            }
        }

        // Clears all products

        public void ClearProducts()
        {
            Clear(ProductsKey);
        }
        #endregion
        #region Cache Helpers
        // Clears single cache entry
        private void Clear(string key)
        {
            lock (locker)
            {
                _memoryCache.Remove(key);
                UsedKeys.Remove(key);
            }
        }
        // Clears entire cache
        private void Clear()
        {
            lock (locker)
            {
                foreach (var usedKey in UsedKeys)
                {
                    _memoryCache.Remove(usedKey);
                }
                UsedKeys.Clear();
            }
        }

        // Add to Cache
        private void Add(string key, object value, DateTimeOffset expiration)
        {
            _memoryCache.Set(key, value, new MemoryCacheEntryOptions().SetAbsoluteExpiration(expiration));
            UsedKeys.Add(key);
        }
        #endregion
    }
}
