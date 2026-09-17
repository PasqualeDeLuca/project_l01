

using Microsoft.AspNetCore.Mvc;
using Project_l01.Api.DTOs.Categories;
using Project_l01.Api.DTOs.Products;
using Project_l01.Api.Services;

namespace Project_l01.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _productService.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
    {
        var product = await _productService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            product
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductDto dto)
    {
        var updated = await _productService.UpdateAsync(id, dto);
        
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}