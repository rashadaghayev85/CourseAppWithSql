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
using System.Xml.Linq;

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
                Console.WriteLine("Enter Password");
                string password = Console.ReadLine();
                var login = await _userService.Login(emailOrUserName, password);

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
        FullName: Console.WriteLine("Enter FullName");
            string fullName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(fullName))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.EmptyString);
                goto FullName;
            }
            foreach (var item in fullName)
            {
                if (char.IsDigit(item)||char.IsPunctuation(item)||char.IsSymbol(item))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                    goto FullName;
                }
            }
            
        Email: Console.WriteLine("Enter Email");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.EmptyString);
                goto Email;
            }
            if (!email.Contains("@"))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                goto Email;

            }


            var response = await _userService.GetByUsernameOrEmailAsync(email);
            if (response is not null)
            {
                ConsoleColor.Red.WriteConsole("This email is no longer available");
                goto Email;
            }
        Username: Console.WriteLine("Enter Username");
            string username = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(username))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.EmptyString);
                goto Username;
            }
            var result = await _userService.GetByUsernameOrEmailAsync(username);
            if (result is not null)
            {
                ConsoleColor.Red.WriteConsole("This Username is no longer available");
                goto Username;
            }
        Password: Console.WriteLine("Enter Password");
            string password = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(password))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.EmptyString);
                goto Password;
            }
            if (password.Length >= 8)
            {
                DateTime time= DateTime.Now;    
                _userService.Register(new User { FullName = fullName, Email = email, UserName = username, Password = password,CreatedDate=time });
                ConsoleColor.Green.WriteConsole("Register succesfully");
            }
            else
            {
                ConsoleColor.Red.WriteConsole("The length of the password should not be less than 8");
                goto Password;
            }

            
        }
        public async Task GetAll()
        {
            var datas = await _userService.GetAllAsync();
            foreach (var data in datas)
            {
                Console.WriteLine("User FullName-" + data.FullName + " UserName-" + data.UserName + " User Email-" + data.Email);
            }
        }
        //public async Task DeleteAsync()
        //{

        //    _userService.DeleteAsync();
        //}       
    }
}
