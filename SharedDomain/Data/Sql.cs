using System;
using System.Collections.Generic;
using System.Text;

namespace SharedDomain.Data
{
    // ** Trasaction Script Pattern
    public static class Sql
    {
        public static string UpdateAllTotalOrders { get { return totalOrders; } }
        public static string UpdateTotalOrdersForCustomerAndProduct { get { return singleCustomerAndProduct; } }
        public static string UpdateTotalOrdersForCustomer { get { return singleCustomer; } }
        public static string UpdateTotalOrdersForProduct { get { return singleProduct; } }

        private static readonly string totalOrders =
               @"UPDATE Customer 
                    SET TotalOrders = (SELECT COUNT([Order].Id) FROM [Order] WHERE [Order].CustomerId = [Customer].Id);
                 UPDATE Product
                    SET TotalOrders = (SELECT COUNT([Order].Id) FROM [Order] WHERE [Order].ProductId = [Product].Id);";

        private static readonly string singleCustomerAndProduct =
               @"UPDATE Customer 
                    SET TotalOrders = (SELECT COUNT([Order].Id) FROM [Order] WHERE [Order].CustomerId = [Customer].Id)
                  WHERE [Customer].Id = {0};
                 UPDATE Product
                    SET TotalOrders = (SELECT COUNT([Order].Id) FROM [Order] WHERE [Order].ProductId = [Product].Id)
                  WHERE [Product].Id = {1};";

        private static readonly string singleCustomer =
               @"UPDATE Customer 
                    SET TotalOrders = (SELECT COUNT([Order].Id) FROM [Order] WHERE [Order].CustomerId = [Customer].Id)
                  WHERE [Customer].Id = {0};";

        private static readonly string singleProduct =
              @"UPDATE Product
                    SET TotalOrders = (SELECT COUNT([Order].Id) FROM[Order] WHERE[Order].ProductId = [Product].Id)
                  WHERE[Product].Id = {0};";
    }
}
