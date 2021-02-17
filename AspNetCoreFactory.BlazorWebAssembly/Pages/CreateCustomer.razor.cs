using AspNetCoreFactory.BlazorWebAssembly.HttpRepository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using AspNetCoreFactory.Dtos;

namespace AspNetCoreFactory.BlazorWebAssembly.Pages
{
    public partial class CreateCustomer : IDisposable
    {
        private CustomerForCreateDto CustomerForCreateDto { get; set; }
        private string TotalOrdersSelected { get; set; }
        private EditContext _editContext;
        private bool formInvalid = true;
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        public ICustomerRepository CustomerRepository { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }

        protected override void OnInitialized()
        {
            CustomerForCreateDto = new CustomerForCreateDto();
            _editContext = new EditContext(CustomerForCreateDto);
            _editContext.OnFieldChanged += HandleFieldChanged;
        }

        private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            formInvalid = !_editContext.Validate();

            StateHasChanged();
        }

        private async Task Create()
        {
            await CustomerRepository.CreateCustomer(CustomerForCreateDto);
            ToastService.ShowSuccess($"Action successful. " +
                $"Product \"{CustomerForCreateDto.FirstName}\" successfully added.");
            CustomerForCreateDto = new CustomerForCreateDto();
            _editContext.OnValidationStateChanged += ValidationChanged;
            _editContext.NotifyValidationStateChanged();
            NavigationManager.NavigateTo("/customers");
        }

        private void ValidationChanged(object sender, ValidationStateChangedEventArgs e)
        {
            formInvalid = true;
            _editContext.OnFieldChanged -= HandleFieldChanged;
            _editContext = new EditContext(CustomerForCreateDto);
            _editContext.OnFieldChanged += HandleFieldChanged;
            _editContext.OnValidationStateChanged -= ValidationChanged;
        }

        public void Dispose()
        {
            _editContext.OnFieldChanged -= HandleFieldChanged;
            _editContext.OnValidationStateChanged -= ValidationChanged;

        }
    }
}
