using System.ComponentModel.DataAnnotations;

namespace ProductServiceApp.Application.DTOs
{
    public class ProductDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
