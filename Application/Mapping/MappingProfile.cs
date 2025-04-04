using Application.DTOs.Response;
using Domain.Entities;
using AutoMapper;
using Domain.DTOs;

namespace Api.Middlewares.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Маппинг для Category
        CreateMap<Category, CategoryResponseDto>();
            // Если нужно передать список продуктов, то маппинг для коллекции выполнится автоматически,
            // если настроен маппинг для Product -> ProductResponseDto.

        // Маппинг для Order и OrderItem
        CreateMap<Order, OrderResponseDto>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
        
        CreateMap<OrderItem, OrderItemResponseDto>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

        // Маппинг для Product
        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));

        // Маппинг для Review
        CreateMap<Review, ReviewResponseDto>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

        // Маппинг для RefreshToken
        CreateMap<RefreshToken, RefreshTokenResponseDto>();

        // Маппинг для Role
        CreateMap<Role, RoleResponseDto>();

        // Маппинг для ShoppingCart и ShoppingCartItem
        CreateMap<ShoppingCart, ShoppingCartResponseDto>()
            .ForMember(dest => dest.ShoppingCartItems, opt => opt.MapFrom(src => src.ShoppingCartItems));
        
        CreateMap<ShoppingCartItem, ShoppingCartItemResponseDto>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

        // Маппинг для User
        CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.ShoppingCart, opt => opt.MapFrom(src => src.ShoppingCart));
        
        CreateMap<ProductRequestDto, Product>()
            .ForMember(dest => dest.Category, 
                opt => opt.Ignore()) // Игнорируем, так как CategoryId будет обрабатываться отдельно
            .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt,
                opt => opt.MapFrom(_ => DateTime.UtcNow));

        // Маппинг для ответа (Entity -> DTO)
        CreateMap<Product, ProductResponseDto>();
    }
}