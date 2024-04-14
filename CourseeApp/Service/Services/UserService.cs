using Repository.Repositories.Interfaces;
using Repository.Repositories;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService()
        {
            _userRepo = new UserRepository();
        }

        public async Task DeleteAsync(User entity)
        {
            await _userRepo.DeleteAsync(entity);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepo.GetAllAsync();
        }

        public async Task<User> GetByUsernameOrEmailAsync(string usernameOrEmail)
        {
            return await _userRepo.GetByUsernameOrEmailAsync(usernameOrEmail);
        }

        public async Task<bool> Login(string emailOrUserName,string password)
        {
            return await _userRepo.Login(emailOrUserName,password);
        }

        public async Task Register(User user)
        {
            await _userRepo.CreateAsync(user);
        }
    }
}
