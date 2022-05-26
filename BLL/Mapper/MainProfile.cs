using AutoMapper;
using BLL.DTOs;
using BLL.DTOs.User;
using DAL.Entities;

namespace BLL.Mapper;

public class MainProfile : Profile
{
    public MainProfile()
    {
        // User main data
        CreateMap<User, UserMainDataDTO>();
        CreateMap<UserMainDataDTO, User>();

        // User register
        CreateMap<UserRegisterDTO, User>();

        // Product
        CreateMap<ProductDTO, Product>();
        CreateMap<Product, ProductDTO>();

        // Order
        CreateMap<OrderDTO, Order>();
        CreateMap<Order, OrderDTO>();
    }
}