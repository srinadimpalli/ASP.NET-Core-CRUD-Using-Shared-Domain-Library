using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreFactory.BlazorWebAssembly.HttpRepository;
using Blazored.Toast;

namespace AspNetCoreFactory.BlazorWebAssembly
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5011/api/") });

            //builder.Services.AddHttpClient("CustomersAPI", cl =>
            //{
            //    cl.BaseAddress = new Uri("https://localhost:5011/api/");
            //});
            //builder.Services.AddScoped(sc => sc.GetService<IHttpClientFactory>().CreateClient("CustomersAPI"));

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddBlazoredToast();
            await builder.Build().RunAsync();
        }
    }
}
