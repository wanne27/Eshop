using Microsoft.AspNetCore.Mvc;
using ProductServiceApp.Application.DTOs;
using ProductServiceApp.Application.Interfaces;

namespace ProductServiceApp.API.Controllers
{
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
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateProductDto createProductDto)
        {
            await _productService.AddProductAsync(createProductDto);
            var createdProduct = await _productService.GetLastCreatedProductAsync();
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateProductDto updatPproductDto)
        {
            if (id != updatPproductDto.Id) return BadRequest();
            await _productService.UpdateProductAsync(updatPproductDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/update-price")]
        public async Task<IActionResult> UpdatePrice(int id, [FromBody] decimal newPrice)
        {
            try
            {
                var rowsAffected = await _productService.UpdateProductPriceAsync(id, newPrice);
                return Ok(new { Message = "Price updated successfully", RowsAffected = rowsAffected });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { ex.Message });
            }
        }

    }
}
