namespace OrderServiceApp.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
