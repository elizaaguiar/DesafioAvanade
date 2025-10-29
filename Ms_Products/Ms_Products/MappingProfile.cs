using AutoMapper;
using Ms_Products.DTOs;
using Ms_Products.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, Product>();
    }
}