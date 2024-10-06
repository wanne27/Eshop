using AutoMapper;
using ProductServiceApp.Application.DTOs;
using ProductServiceApp.Application.Interfaces;
using ProductServiceApp.Domain.Entities;
using ProductServiceApp.Domain.Interfaces;

namespace ProductServiceApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning($"Product with ID {id} not found.");
                return null;
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task AddProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            product.CreatedDate = DateTime.UtcNow;
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var product = _mapper.Map<Product>(updateProductDto);
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<ProductDto> GetLastCreatedProductAsync()
        {
            var product = await _productRepository.GetLastCreatedProductAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<int> UpdateProductPriceAsync(int id, decimal newPrice)
        {
            var rowsAffected = await _productRepository.UpdateProductPriceAsync(id, newPrice);
            if (rowsAffected == 0)
            {
                _logger.LogWarning($"Product ID {id} was not found for update.");
                throw new InvalidOperationException("Product not found.");
            }

            return rowsAffected;
        }
    }
}
