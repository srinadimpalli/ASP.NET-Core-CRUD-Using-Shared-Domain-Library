using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedDomain
{
    public static class SharedDomainDbServiceExtension
    {
        public static void AddInMemoryDatabaseService(this IServiceCollection services, string dbName)
        {
            services.AddDbContext<CustOrdProdContext>(options => options.UseInMemoryDatabase(dbName));
        }
        public static void InitializeSeededData(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var service = serviceScope.ServiceProvider;
            SharedDomainDbSeeder.Seed(service);
        }
    }
}
