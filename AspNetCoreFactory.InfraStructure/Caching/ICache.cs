using System;
using System.Collections.Generic;
using System.Text;
using SharedDomain;

namespace AspNetCoreFactory.InfraStructure.Caching
{
    public interface ICache
    {
        Dictionary<int, Customer> Customers { get; }
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        void ClearCustomers();

        Dictionary<int, Product> Products { get; }
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        void ClearProducts();
    }
}
