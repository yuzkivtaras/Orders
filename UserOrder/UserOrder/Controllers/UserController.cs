using DataServices.IRepository;
using DataServices.Repository;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;

        public UserController(IUserRepository userUrepository, IOrderRepository orderRepositoey)
        {
            _userRepository = userUrepository;
            _orderRepository = orderRepositoey;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userRepository.GetUserById(user.UserID);
            if (existingUser != null)
            {
                return Conflict();
            }

            var addedUser = await _userRepository.AddUser(user);
            return Ok(addedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var existingUser = await _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            var existingsOrders = await _orderRepository.GetOrdersByUserId(id);
            if (existingsOrders.Any())
            {
                return BadRequest("Cannot delete user with existing orders.");
            }
            await _userRepository.DeleteUser(existingUser);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] User user)
        {
            var existingUser = await _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Login = user.Login;
            existingUser.Password = user.Password;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.Gender = user.Gender;

            await _userRepository.UpdateUser(existingUser);
            return Ok(existingUser);
        }
    }
}
