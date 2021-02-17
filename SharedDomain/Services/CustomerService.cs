using Microsoft.EntityFrameworkCore;
using SharedDomain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Services
{
    public class CustomerService : ServiceBase<Customer>, ICustomerService
    {
        public CustomerService(CustOrdProdContext custOrdProdContext) : base(custOrdProdContext)
        {

        }
        public void CreateCustomer(Customer customer)
        {
            Create(customer);
        }
        public async Task CreateCustomerAsync(Customer customer)
        {
            await CreateAsync(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            Delete(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            Update(customer);

        }

        public IEnumerable<Customer> GetAllCustomers(bool trackChanges)
        {
            return FindAll(trackChanges)
                  .OrderBy(c => c.FirstName)
                  .ToList();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
           .OrderBy(c => c.FirstName)
           .ToListAsync();
        }
        public async Task<Customer> GetCustomerAsync(int customerId, bool trackChanges)
        {
            return await FindByCondition(c => c.Id.Equals(customerId), trackChanges)
                        .SingleOrDefaultAsync();
        }

        public IEnumerable<Customer> GetByIds(IEnumerable<int> ids, bool trackChanges)
        {
            return FindByCondition(c => ids.Contains(c.Id), trackChanges).ToList();
        }

        public Customer GetCustomer(int customerId, bool trackChanges)
        {
            return FindByCondition(c => c.Id.Equals(customerId), trackChanges)
                    .SingleOrDefault();
        }

        public int GetCount(bool trackChanges)
        {
            return FindAll(trackChanges).Count();
        }
    }
}
