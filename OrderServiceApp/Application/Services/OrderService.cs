using AutoMapper;
using OrderServiceApp.Application.DTOs;
using OrderServiceApp.Application.Interfaces;
using OrderServiceApp.Domain.Entities;
using OrderServiceApp.Domain.Interfaces;

namespace OrderServiceApp.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                _logger.LogWarning($"Заказ с ID {orderId} не найден.");
                return null;
            }

            return _mapper.Map<OrderDto>(order);
        }

        public async Task CreateOrderAsync(CreateOrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            order.TotalAmount = order.Items.Sum(item => item.TotalPrice);
            await _orderRepository.CreateOrderAsync(order);
        }
        
        public async Task DeleteOrderAsync(int orderId)
        {
            await _orderRepository.DeleteOrderAsync(orderId);
        }

    }
}
