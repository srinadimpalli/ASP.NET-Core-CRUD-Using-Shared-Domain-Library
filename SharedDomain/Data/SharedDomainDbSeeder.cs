using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedDomain
{
    public class SharedDomainDbSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var context = new CustOrdProdContext(serviceProvider.GetRequiredService<DbContextOptions<CustOrdProdContext>>());
            if (context.Customer.Any()) { return; }
            context.Customer.AddRange(
                new Customer
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john@smith.com",
                    TotalOrders = 2
                },
                 new Customer
                 {
                     Id = 2,
                     FirstName = "Miren",
                     LastName = "Castro",
                     Email = "mcastro@hotmail.com",
                     TotalOrders = 1
                 },
                 new Customer
                 {
                     Id = 3,
                     FirstName = "Craig",
                     LastName = "Wue",
                     Email = "craigwu@outlook.com",
                     TotalOrders = 0
                 },
                 new Customer
                 {
                     Id = 4,
                     FirstName = "King",
                     LastName = "Vanderbilt",
                     Email = "kingv@gamail.com",
                     TotalOrders = 1
                 },
                  new Customer
                  {
                      Id = 5,
                      FirstName = "Thomas",
                      LastName = "Kipp",
                      Email = "thomas@kipp.com",
                      TotalOrders = 0
                  }

            );

            // Oder seed
            if (context.Order.Any()) { return; }
            context.Order.AddRange(
                new Order
                {
                    Id = 5,
                    CustomerId = 2,
                    ProductId = 3,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    Id = 6,
                    CustomerId = 1,
                    ProductId = 2,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    Id = 7,
                    CustomerId = 4,
                    ProductId = 8,
                    OrderDate = DateTime.Now
                },
                 new Order
                 {
                     Id = 8,
                     CustomerId = 1,
                     ProductId = 3,
                     OrderDate = DateTime.Now
                 }

            );

            // product seed
            if (context.Product.Any()) { return; }
            context.Product.AddRange(
                new Product
                {
                    Id = 1,
                    Name = "Microsoft Office 365",
                    Price = 99,
                    TotalOrders = 0
                },
                 new Product
                 {
                     Id = 2,
                     Name = "Adobe Photoshop",
                     Price = 299,
                     TotalOrders = 1
                 },
                  new Product
                  {
                      Id = 3,
                      Name = "Adobe Illustrator",
                      Price = 299,
                      TotalOrders = 2
                  },
                  new Product
                  {
                      Id = 4,
                      Name = "Adobe Acrobat",
                      Price = 119,
                      TotalOrders = 0
                  },
                  new Product
                  {
                      Id = 5,
                      Name = "Quicken Home and Business",
                      Price = 169,
                      TotalOrders = 0
                  },
                  new Product
                  {
                      Id = 6,
                      Name = "Turbo Tax Deluxe",
                      Price = 59,
                      TotalOrders = 0
                  },
                  new Product
                  {
                      Id = 7,
                      Name = "AutoDesk AutoCAD",
                      Price = 1259,
                      TotalOrders = 0
                  },
                  new Product
                  {
                      Id = 8,
                      Name = "Toad for ORACLE",
                      Price = 199,
                      TotalOrders = 1
                  },
                   new Product
                   {
                       Id = 9,
                       Name = "Visual Studio 2019",
                       Price = 129,
                       TotalOrders = 1
                   },
                   new Product
                   {
                       Id = 10,
                       Name = "Oracle SE",
                       Price = 29,
                       TotalOrders = 0
                   }

            );
            context.SaveChanges();
        }
    }
}
