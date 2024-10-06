namespace OrderServiceApp.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }    
}
