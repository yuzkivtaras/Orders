using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.IRepository
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByUserIdAndDate(int userId, DateTime date);
        Task<Order> CreateOrder(Order order, DateTime date);
        Task DeleteOrderById(int id);
        Task<Order> UpdateOrder(Order order);
        //Task<bool> UserHasOrders(int userId);
        Task<IEnumerable<Order>> GetOrdersByUserId(int userId);
    }
}
