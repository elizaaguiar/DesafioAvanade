using AutoMapper;
using Ms_Order.DTOs;
using Ms_Order.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderResponseDTO>();
        CreateMap<OrderResponseDTO, Order>();
        CreateMap<CreateOrderDTO, Order>();
        CreateMap<Order, CreateOrderDTO>();

    }
}