using Application.DTOs.Response;
using Domain.DTOs;

namespace Application.Interfaces;

public interface IProductService
{
    Task<ICollection<ProductResponseDto>> GetAllProductsAsync();
    Task<ProductResponseDto> GetProductByIdAsync(int id);
    Task AddProductAsync(ProductRequestDto productDto);
    Task DeleteProductAsync(int id);
}