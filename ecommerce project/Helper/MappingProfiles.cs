using AutoMapper;
using ecommerce_project.Dto;
using ecommerce_project.Models;

namespace ecommerce_project.Helper;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateUserDto,User>();
        CreateMap<UserDto,User>();
        CreateMap<User,CreateUserDto>();
        CreateMap<User,UserDto>();
        CreateMap<Product_category, CategoryDto>();
        CreateMap<CategoryDto, Product_category> ();
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDTO, Product>();
    }
}
