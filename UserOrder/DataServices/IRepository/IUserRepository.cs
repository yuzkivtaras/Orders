using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<User> AddUser(User user);
        Task DeleteUser(User user);
        Task UpdateUser(User user);
        Task<User> GetUserByLogin(string login);
    }
}
