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

    [HttpGet("allProducts")]
    public async Task<IResult> GetAllProductsAsync()
    {
        var response = await _productService.GetAllProductsAsync();
        return TypedResults.Ok(response);
    }

    [HttpPost("addProduct")]
    public async Task<IResult> AddProductAsync(ProductRequestDto dto)
    {
        await _productService.AddProductAsync(dto);
        return TypedResults.Ok($"Product created with {dto}");
    }
}