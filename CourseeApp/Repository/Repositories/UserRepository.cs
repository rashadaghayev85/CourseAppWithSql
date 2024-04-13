using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository()
        {
            _context = new AppDbContext();
        }
        public async Task<bool> Login(string emailOrUserName,string password)
        {
            var data = await _context.Users.FirstOrDefaultAsync(m => m.Email == emailOrUserName && m.UserName == emailOrUserName||m.Password==password);
        if (data is null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       

        public async Task Register(User user)
        {
           _context.Users.Add(user);    
        }
    }
}
