using Application.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Supplier")]
public class SupplierController: ControllerBase
{
    private readonly IProductService _productService;
    
    public SupplierController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("product")]
    public async Task<IResult> GetAllProductsAsync()
    {
        var response = await _productService.GetAllProductsAsync();
        return TypedResults.Ok(response);
    }

    [HttpGet("product/{id}")]
    public async Task<IResult> GetProductById(int id)
    {
        var response = await _productService.GetProductByIdAsync(id);
        return TypedResults.Ok(response);
    }
    
    [HttpPost("product")]
    public async Task<IResult> AddProductAsync(ProductRequestDto dto)
    {
        await _productService.AddProductAsync(dto);
        return TypedResults.Ok($"Product created with {dto}");
    }

    [HttpDelete("product/{id}")]
    public async Task<IResult> DeleteProductByIdAsync(int id)
    {
        var prod = await _productService.DeleteProductAsync(id);
        return TypedResults.Ok(new {message = "Товар удален", product = prod});
    }
}