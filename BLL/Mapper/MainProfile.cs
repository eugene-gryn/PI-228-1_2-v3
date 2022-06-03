using AutoMapper;
using BLL.DTOs;
using BLL.DTOs.Product;
using BLL.DTOs.User;
using DAL.Entities;
using static BLL.DTOs.Product.ProductAmountDTO;

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
        CreateMap<User, UserRegisterDTO>();

        // ProductID
        CreateMap<ProductDTO, Product>();
        CreateMap<Product, ProductDTO>();

        CreateMap<ProductShortDTO, Product>();
        CreateMap<Product, ProductShortDTO>();


        CreateMap<Product, ProductCreateDTO>();
        CreateMap<ProductCreateDTO, Product>();


        // Order
        CreateMap<OrderDTO, Order>();
        CreateMap<Order, OrderDTO>();

        // Product Amount
        CreateMap<ProductAmount, ProductAmountDTO>();
        CreateMap<ProductAmountDTO, ProductAmount>();
    }
}