using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Login(string emailOrUserName,string password);
        Task Register(User entity);
        Task<User> GetByUsernameOrEmailAsync(string usernameOrEmail);
        Task<List<User>> GetAllAsync();
        Task DeleteAsync(User entity);
    }
}
