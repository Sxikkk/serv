using Application.DTOs.Response;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IProductService
{
    Task<ICollection<ProductResponseDto>> GetAllProductsAsync();
    Task<ProductResponseDto> GetProductByIdAsync(int id);
    Task AddProductAsync(ProductRequestDto productDto);
    Task<Product> DeleteProductAsync(int id);
}