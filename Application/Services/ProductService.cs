using Application.DTOs.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(
        IProductRepository productRepository,
        IMapper mapper,
        ICategoryRepository categoryRepository
        )
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }
    
    public async Task<ICollection<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return _mapper.Map<ICollection<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task AddProductAsync(ProductRequestDto productDto)
    {
        if (!await _categoryRepository.ExistsAsync(productDto.CategoryId))
            throw new Exception("Категория не существует");
    
        var product = _mapper.Map<Product>(productDto);
        await _productRepository.AddProductAsync(product);
    }

    public async Task<Product> DeleteProductAsync(int id)
    {
        var prod = await _productRepository.DeleteProductAsync(id);
        return prod;
    }
}