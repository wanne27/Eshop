using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductServiceApp.Domain.Entities;
using ProductServiceApp.Domain.Interfaces;
using ProductServiceApp.Infrastructure.Data;

namespace ProductServiceApp.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }


        public async Task AddAsync(Product product)
        {
            var nameParam = new SqlParameter("@Name", product.Name);
            var priceParam = new SqlParameter("@Price", product.Price);
            var descriptionParam = new SqlParameter("@Description", product.Description);
            var createdDateParam = new SqlParameter("@CreatedDate", product.CreatedDate);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddProduct @Name, @Price, @Description, @CreatedDate",
                nameParam, priceParam, descriptionParam, createdDateParam);
        }

        public async Task UpdateAsync(Product product)
        {
            var productIdParam = new SqlParameter("@ProductId", product.Id);
            var nameParam = new SqlParameter("@Name", product.Name);
            var priceParam = new SqlParameter("@Price", product.Price);
            var descriptionParam = new SqlParameter("@Description", product.Description);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateProduct @ProductId, @Name, @Price, @Description",
                productIdParam, nameParam, priceParam, descriptionParam);
        }

        public async Task DeleteAsync(int id)
        {
            var productIdParam = new SqlParameter("@ProductId", id);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteProduct @ProductId",
                productIdParam);
        }

        public async Task<Product> GetLastCreatedProductAsync()
        {
            var product = await _context.Products
                .OrderByDescending(p => p.CreatedDate) 
                .FirstOrDefaultAsync();

            if (product == null)
            {
                throw new InvalidOperationException("No product found.");
            }
            return product;
        }

        public async Task<int> UpdateProductPriceAsync(int productId, decimal newPrice)
        {
            var productIdParam = new SqlParameter("@ProductId", productId);
            var newPriceParam = new SqlParameter("@NewPrice", newPrice);

            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateProductPrice @ProductId, @NewPrice",
                productIdParam, newPriceParam);

            return result;
        }

    }
}
