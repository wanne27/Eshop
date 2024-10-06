using OrderServiceApp.Domain.Entities;

namespace OrderServiceApp.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task CreateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
    }
}
