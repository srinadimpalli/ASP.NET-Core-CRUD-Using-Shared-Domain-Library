
using AspNetCoreFactory.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspNetCoreFactory.BlazorWebAssembly.HttpRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options =
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        private readonly NavigationManager _navManager;

        public CustomerRepository(HttpClient client, NavigationManager navManager)
        {
            _client = client;
            _navManager = navManager;
        }

        public async Task CreateCustomer(CustomerForCreateDto customerForCreateDto)
        {
            var customerToCreateJson = new StringContent(
                JsonSerializer.Serialize(customerForCreateDto, _options),
                Encoding.UTF8,
                "application/json");

            using var product =
                await _client.PostAsync("customers", customerToCreateJson);
            _navManager.NavigateTo("/customers");
            //httpResponse.EnsureSuccessStatusCode();
        }

        public async Task DeleteCustomer(int id)
        => await _client.DeleteAsync($"customers/{id}");

        public async Task<CustomerDto> GetCustomer(int id)
        {
            var customer = await _client.GetFromJsonAsync<CustomerDto>($"customers/{id}");
            return customer;
        }

        public async Task<List<CustomerDto>> GetCustomers()
        {
            using var response = await _client.GetAsync("customers");
            var content = await response.Content.ReadAsStringAsync();
            var customers = JsonSerializer.Deserialize<List<CustomerDto>>(content, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return customers;

        }

        public async Task UpdateCustomer(CustomerForUpdateDto customerForUpdateDto)
        {
            //var customerToUpdateJson = new StringContent(
            //   JsonSerializer.Serialize(customerForUpdateDto, _options),
            //   Encoding.UTF8,
            //   "application/json");
            await _client.PutAsJsonAsync(Path.Combine("customers",
                customerForUpdateDto.Id.ToString()), customerForUpdateDto);
            //using var product =
            //    await _client.PutAsync(Path.Combine("customers", customerForUpdateDto.Id.ToString()), customerForUpdateDto);
            _navManager.NavigateTo("/customers");
        }
    }
}
