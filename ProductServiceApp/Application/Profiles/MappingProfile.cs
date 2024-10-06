using AutoMapper;
using ProductServiceApp.Application.DTOs;
using ProductServiceApp.Domain.Entities;

namespace ProductServiceApp.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, CreateProductDto>();

            CreateMap<UpdateProductDto, Product>();
            CreateMap<Product, UpdateProductDto>();
        }
    }
}
