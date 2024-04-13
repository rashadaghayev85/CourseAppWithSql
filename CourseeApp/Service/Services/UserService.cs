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
        private readonly IUserRepository _UserRepo;
        public UserService()
        {
            _UserRepo = new UserRepository();
        }

        public async Task<bool> Login(string emailOrUserName,string password)
        {
            return await _UserRepo.Login(emailOrUserName,password);
        }

        public async Task Register(User user)
        {
            await _UserRepo.Register(user);
        }
    }
}
