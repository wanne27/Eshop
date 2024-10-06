using System.ComponentModel.DataAnnotations;

namespace ProductServiceApp.Application.DTOs
{
    public class CreateProductDto
    {
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The price of the product must be positive.")]
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
