using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Contracts
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers(bool trackChanges);
        Customer GetCustomer(int customerId, bool trackChanges);
        void CreateCustomer(Customer customer);
        Task CreateCustomerAsync(Customer customer);
        void UpdateCustomer(Customer customer);
        IEnumerable<Customer> GetByIds(IEnumerable<int> ids, bool trackChanges);
        void DeleteCustomer(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync(bool trackChanges);
        Task<Customer> GetCustomerAsync(int customerId, bool trackChanges);
        int GetCount(bool trackChanges);
    }
}
