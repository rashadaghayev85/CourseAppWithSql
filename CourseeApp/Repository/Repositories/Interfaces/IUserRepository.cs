using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<bool> Login(string emailOrUserName, string password);
       
        Task<User>GetByUsernameOrEmailAsync(string usernameOrEmail); 
        
    }
}
