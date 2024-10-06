using Microsoft.EntityFrameworkCore;
using OrderServiceApp.Domain.Entities;
using OrderServiceApp.Domain.Interfaces;
using OrderServiceApp.Infrastructure.Data;

namespace OrderServiceApp.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                return await _context.Orders.Include(o => o.Items).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the orders.", ex);
            }
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
