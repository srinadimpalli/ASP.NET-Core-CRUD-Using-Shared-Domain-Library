using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Contracts
{
    public interface ICustomerServiceAsync
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync(bool trackChanges);
        Task<Customer> GetCustomerAsync(int customerId, bool trackChanges);
        void CreateCustomer(Customer customer);
        Task<IEnumerable<Customer>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void DeleteCustomer(Customer customer);
    }
}
