using AspNetCoreFactory.BlazorWebAssembly.Shared;
using AspNetCoreFactory.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.BlazorWebAssembly.Components
{
    public partial class CustomerTable
    {
        [Parameter]
        public List<CustomerDto> CustomersEntity { get; set; }
        [Parameter]
        public EventCallback<int> OnDelete { get; set; }
        private Confirmation _confirmation;
        private int _productIdToDelete;
        private void CallConfirmationModal(int id)
        {
            _productIdToDelete = id;
            _confirmation.Show();
        }

        private async Task DeleteCustomer()
        {
            _confirmation.Hide();
            await OnDelete.InvokeAsync(_productIdToDelete);
        }
    }
}
