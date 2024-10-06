using System.ComponentModel.DataAnnotations;

namespace OrderServiceApp.Application.DTOs
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public Guid UserId { get; set; }

        [MinLength(1, ErrorMessage = "At least one order item is required.")]
        public List<CreateOrderItemDto> Items { get; set; }
    }
}
