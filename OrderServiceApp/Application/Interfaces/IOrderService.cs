using OrderServiceApp.Application.DTOs;

namespace OrderServiceApp.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int orderId);
        Task CreateOrderAsync(CreateOrderDto orderDto);
        Task DeleteOrderAsync(int orderId);
    }
}
