using Data;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.FromSqlRaw("EXEC GetAllOrders").ToListAsync();

            // Explicitly load the related Customer data
            foreach (var order in orders)
            {
                await _context.Entry(order).Reference(o => o.Customer).LoadAsync();
            }

            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var parameter = new SqlParameter("@OrderId", orderId);
            // Bring the result to the client-side using AsEnumerable, and use FirstOrDefault synchronously
            return _context.Orders
                           .FromSqlRaw("EXEC GetOrderById @OrderId", parameter)
                           .AsEnumerable()  // Forces client-side composition
                           .FirstOrDefault();  // Use FirstOrDefault synchronously
        }

        public async Task AddOrderAsync(Order order)
        {
            var parameters = new[]
            {
                new SqlParameter("@OrderDate", order.OrderDate),
                new SqlParameter("@TotalAmount", order.TotalAmount),
                new SqlParameter("@OrderStatus", order.OrderStatus),
                new SqlParameter("@CustomerId", order.CustomerId)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddOrder @OrderDate, @TotalAmount, @OrderStatus, @CustomerId", parameters);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var parameters = new[]
            {
                new SqlParameter("@OrderId", order.OrderId),
                new SqlParameter("@OrderDate", order.OrderDate),
                new SqlParameter("@TotalAmount", order.TotalAmount),
                new SqlParameter("@OrderStatus", order.OrderStatus),
                new SqlParameter("@CustomerId", order.CustomerId)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateOrder @OrderId, @OrderDate, @TotalAmount, @OrderStatus, @CustomerId", parameters);
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var parameter = new SqlParameter("@OrderId", orderId);
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteOrder @OrderId", parameter);
        }
    }
}
