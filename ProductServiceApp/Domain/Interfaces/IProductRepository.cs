using ProductServiceApp.Domain.Entities;

namespace ProductServiceApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<Product> GetLastCreatedProductAsync();
        Task<int> UpdateProductPriceAsync(int productId, decimal newPrice);
    }
}
