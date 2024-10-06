using ProductServiceApp.Application.DTOs;

namespace ProductServiceApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task AddProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(UpdateProductDto updatPproductDto);
        Task DeleteProductAsync(int id);
        Task<ProductDto> GetLastCreatedProductAsync();
        Task<int> UpdateProductPriceAsync(int id, decimal newPrice);
    }
}
