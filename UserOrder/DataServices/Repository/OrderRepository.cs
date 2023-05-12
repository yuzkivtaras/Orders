using DataServices.Data;
using DataServices.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataServices.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;       

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAndDate(int userId, DateTime date)
        {
            return await _context.Orders.Where(o => o.UserID == userId && o.OrderDate.Date == date).ToListAsync();
        }

        //public async Task<Order> GetOrderByUserId(int userId)
        //{
        //    return await _context.Orders.FirstOrDefault(u => u.UserID == userId);
        //}

        public async Task<Order> CreateOrder(Order order, DateTime date)
        {
            bool userHasOrderToday = await _context.Orders.AnyAsync(o => o.UserID == order.UserID && o.OrderDate.Date == date.Date);
            if (userHasOrderToday)
            {
                throw new Exception("User already has an order for today.");
            }

            var user = await _context.Users.FindAsync(order.UserID);
            if (user == null)
            {
                throw new Exception("User does not exist.");
            }

            order.User = user;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task DeleteOrderById(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserID == userId)
                .ToListAsync();
        }

        //public async Task<bool> UserHasOrders(int userId)
        //{
        //    return await _context.Orders.AnyAsync(o => o.UserID == userId);
        //}
    }
}
