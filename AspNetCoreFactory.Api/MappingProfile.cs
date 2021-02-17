using AspNetCoreFactory.Api.DataTransferObjects;
using AutoMapper;
using SharedDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFactory.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(c => c.TotalOrdersFormatted, opt => opt.MapFrom(x => x.TotalOrders == 0 ? "none" : x.TotalOrders.ToString()))
                .ForMember(c => c.FullName, opt => opt.MapFrom(x => string.Join(' ', x.FirstName, x.LastName)));
            CreateMap<CustomerForCreateDto, Customer>();
            CreateMap<CustomerForUpdateDto, Customer>();
            // Orders
            CreateMap<Order, OrderDto>()
                .ForMember(o => o.OrderDateFormatter, opt => opt.MapFrom(x => x.OrderDate.ToShortDateString()));
            CreateMap<OrderForCreateDto, Order>();
            CreateMap<OrderForUpdateDto, Order>();

            //Product

            CreateMap<Product, ProductDto>();
            CreateMap<ProductForCreateDto, Product>();
            CreateMap<ProductForUpdateDto, Product>();
        }
    }
}
