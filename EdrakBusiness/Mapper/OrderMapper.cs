using AutoMapper;
using Domain.Entities;
using EdrakBusiness.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdrakBusiness.Mapper
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<OrderItem, OrderItemDto>();//.ForMember(dest => dest.ProductName, opt => opt.MapFrom(s => s.Product.Name));
            CreateMap<OrderItemDto, OrderItem>();

            CreateMap<OrderItemRequestDto, OrderItem>();
            CreateMap<OrderRequestDto, Order>();

            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderDto>();//.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(s => s.Customer.Name));

 
        }
    }
}
