using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreFactory.BlazorWebAssembly.HttpRepository;
using AspNetCoreFactory.Dtos;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AspNetCoreFactory.BlazorWebAssembly.Pages
{
    public partial class UpdateCustomer : IDisposable
    {
        private CustomerDto _customerForUpdateDto;// { get; set; } = new CustomerDto();
        private EditContext _editContext;
        private bool formInvalid = false;
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        public ICustomerRepository CustomerRepository { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }
        [Parameter]
        public int Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _customerForUpdateDto = await CustomerRepository.GetCustomer(Id);
            //_customerForUpdateDto.Id = customer.Id;
            //_customerForUpdateDto.FirstName = customer.FirstName;
            //_customerForUpdateDto.LastName = customer.LastName;
            //_customerForUpdateDto.Email = customer.Email;
            _editContext = new EditContext(_customerForUpdateDto);
            _editContext.OnFieldChanged += HandleFieldChanged;
            base.OnInitialized();
        }

        private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            formInvalid = !_editContext.Validate();
            StateHasChanged();
        }

        private async Task Update()
        {
            var customer = new CustomerForUpdateDto
            {
                Id = _customerForUpdateDto.Id,
                FirstName = _customerForUpdateDto.FirstName,
                LastName = _customerForUpdateDto.LastName,
                Email = _customerForUpdateDto.Email

            };

            await CustomerRepository.UpdateCustomer(customer);
            ToastService.ShowSuccess($"Action successful. " +
                $"Product \"{_customerForUpdateDto.FirstName}\" successfully updated.");
            NavigationManager.NavigateTo("/customers");
        }

        public void Dispose()
        {
            _editContext.OnFieldChanged -= HandleFieldChanged;

        }
    }
}
