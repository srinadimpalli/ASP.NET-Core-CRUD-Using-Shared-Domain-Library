using AspNetCoreFactory.BlazorWebAssembly.HttpRepository;
using AspNetCoreFactory.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.BlazorWebAssembly.Pages
{
    public partial class CustomerDetail
    {
        public CustomerDto Customer { get; set; } = new CustomerDto();
        [Parameter]
        public int customerId { get; set; }
        [Inject]
        public ICustomerRepository CustomerRepository { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Customer = await CustomerRepository.GetCustomer(customerId);
        }
    }
}
