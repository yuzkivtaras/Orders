using DataServices.IRepository;
using DataServices.Repository;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public OrderController(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            var existingOrder = await _orderRepository.GetOrdersByUserIdAndDate(order.UserID, order.OrderDate.Date);
            if (existingOrder.Any())
            {
                return Conflict("Only one order may be created for user per day.");
            }

            var addedOrder = await _orderRepository.CreateOrder(order, order.OrderDate);
            return CreatedAtAction(nameof(GetOrderById), new { id = addedOrder.OrderID }, addedOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }

            var existingOrder = await _orderRepository.GetOrderByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            var ordersForDate = await _orderRepository.GetOrdersByUserIdAndDate(existingOrder.UserID, existingOrder.OrderDate.Date);
            if (ordersForDate.Any(o => o.OrderID != existingOrder.OrderID))
            {
                return Conflict("Only one order may be created for user per day.");
            }

            var user = await _userRepository.GetUserByLogin(order.User.Login);
            if (user == null)
            {
                return NotFound("User with specified login does not exist.");
            }

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.OrderCost = order.OrderCost;
            existingOrder.ItemsDescription = order.ItemsDescription;
            existingOrder.ShippingAddress = order.ShippingAddress;

            await _orderRepository.UpdateOrder(existingOrder);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteOrderById(id);
            return NoContent();
        }
    }
}
