using AspNetCoreFactory.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.BlazorWebAssembly.HttpRepository
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDto>> GetCustomers();
        Task<CustomerDto> GetCustomer(int id);
        Task CreateCustomer(CustomerForCreateDto customerForCreateDto);
        Task UpdateCustomer(CustomerForUpdateDto customerForUpdateDto);
        Task DeleteCustomer(int id);
    }
}
