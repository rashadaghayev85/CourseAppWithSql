using Service.Services.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Helpers.Extensions;
using Service.Helpers.Constants;
using Domain.Models;

namespace CourseeApp.Controllers
{
    public class UserController
    {
        private readonly IUserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }
        public async Task<bool> Login()
        {
            try
            {
                Console.WriteLine("Enter Email Or Username");
                string emailOrUserName = Console.ReadLine();
                Console.WriteLine("Enter Email Or Username");
                string password = Console.ReadLine();
                var login = await _userService.Login(emailOrUserName,password);

                if (!login)
                {
                    return false;
                    throw new Exception(ResponseMessages.LoginFailed);
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                
                ConsoleColor.Red.WriteConsole(ex.Message);
                return false;
            }
           
           
        }
        public async Task Register()
        {
            Console.WriteLine("Enter FullName");
            string fullName = Console.ReadLine();
            Console.WriteLine("Enter Email");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Username");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            string password = Console.ReadLine();

            _userService.Register(new User {FullName=fullName,Email=email,UserName=username,Password=password });
            ConsoleColor.Green.WriteConsole("Register succesfully");
        }
    }
}
