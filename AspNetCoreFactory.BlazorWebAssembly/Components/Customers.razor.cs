using AspNetCoreFactory.BlazorWebAssembly.HttpRepository;
using AspNetCoreFactory.Dtos;
using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.BlazorWebAssembly.Components
{
    public partial class Customers
    {

        public List<CustomerDto> CustomersDtoList { set; get; } = new List<CustomerDto>();

        [Inject]
        public ICustomerRepository CustomerRepository { get; set; }

        protected async override Task OnInitializedAsync()
        {
            CustomersDtoList = await CustomerRepository.GetCustomers();
        }
        private async Task DeleteCustomer(int id)
        {
            await CustomerRepository.DeleteCustomer(id);
            CustomersDtoList = await CustomerRepository.GetCustomers();

        }
    }
}
