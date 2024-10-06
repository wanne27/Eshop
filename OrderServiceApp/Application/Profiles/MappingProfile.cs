using AutoMapper;
using OrderServiceApp.Application.DTOs;
using OrderServiceApp.Domain.Entities;

namespace OrderServiceApp.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<CreateOrderDto, Order>();
            CreateMap<CreateOrderItemDto, OrderItem>();
        }
    }
}
