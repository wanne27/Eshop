using System.ComponentModel.DataAnnotations;

namespace OrderServiceApp.Application.DTOs
{
    public class OrderItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The quantity must be a positive number.")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be positive.")]
        public decimal UnitPrice { get; set; }
    }
}
