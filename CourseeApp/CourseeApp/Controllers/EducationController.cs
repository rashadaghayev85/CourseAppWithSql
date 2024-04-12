using Service.Services.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Service.Helpers.Extensions;
using Service.Helpers.Exceptions;
using Service.Helpers.Constants;

namespace CourseeApp.Controllers
{
    public class EducationController
    {
        private readonly IEducationService _educationService;
      
        public EducationController()
        {
            _educationService = new EducationService();
           
        }
        public async Task GetAllAsync()
        {
            var data=await _educationService.GetAllAsync();
            foreach (var item in data)
            {
                 Console.WriteLine("Education-" + item.Name + " Color-" + item.Color + " CreatedDate-" + item.CreatedDate);
            }
            
        }
        public async Task CreateAsync()
        {
            Education:Console.WriteLine("Add Education");
             string name =Console.ReadLine();
            var data = await _educationService.SearchByNameAsync(name);
            if (data is not null)
            {
                ConsoleColor.Red.WriteConsole("The Education already exists ");
                goto Education;
            }
             Console.WriteLine("Add Education color");
             string color = Console.ReadLine();

            
             DateTime time = DateTime.Now;

           
            await _educationService.CreateAsync(new Education { Name = name.Trim().ToLower(), Color = color.Trim().ToLower(), CreatedDate =time });


            await Console.Out.WriteLineAsync("Data succesfuly added");
        }

        public async Task DeleteAsync()
        {
            var data = await _educationService.GetAllAsync();
            foreach (var item in data)
            {
                Console.WriteLine("Id-"+item.Id+" Name-"+item.Name + " CreatedDate-" + item.CreatedDate);
            }

           Id: Console.WriteLine("Select the id you want to delete");
            string idStr = Console.ReadLine();
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
                var response=await _educationService.GetByIdAsync(id);
                _educationService.DeleteAsync(response);
            }
            else
            {
                goto Id;
            }

            
        }
        public async Task GetByIdAsync()

        {
        Id: Console.WriteLine("Add Id");
            string idStr = Console.ReadLine();
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
               var item= await _educationService.GetByIdAsync(id);
                 Console.WriteLine("Education-" + item.Name + " Color-" + item.Color + " CreatedDate-" + item.CreatedDate);
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                goto Id;
            }
            
        }
        public async Task UpdateAsync()
        {
            var datas=await _educationService.GetAllAsync();
            foreach (var item in datas)
            {
                Console.WriteLine("Id-"+item.Id+" Education-" + item.Name + " Color-" + item.Color + " CreatedDate-" + item.CreatedDate);
            }
            bool update = true;
        Id: ConsoleColor.Yellow.WriteConsole("Select the Id you want to update:");
            string idStr = Console.ReadLine();
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
                try
                {
                    var data = _educationService.GetByIdAsync(id);
                    if (data is null)
                    {
                        throw new NotFoundException(ResponseMessages.DataNotFound);
                        //ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    }
                    Console.WriteLine("Enter new Education ");
                    string newEducation = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newEducation))
                    {
                        var response = await _educationService.SearchByNameAsync(newEducation);
                        if (response.Count==0)
                        {
                            if (data.Result.Name.ToLower().Trim() != newEducation.ToLower().Trim())
                            {
                                data.Result.Name = newEducation;
                                update = false;
                            }
                        }

                    }
                    Console.WriteLine("Enter new color");
                    string newColor = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newColor))
                    {
                        if (data.Result.Color.ToLower().Trim() != newColor.ToLower().Trim())
                        {
                            data.Result.Color = newColor;
                            update = false;
                        }



                    }

                    if (update)
                    {
                        ConsoleColor.DarkYellow.WriteConsole("there was no change");
                    }
                    else
                    {
                        data.Result.CreatedDate = DateTime.Now;
                        _educationService.UpdateAsync(data.Result);
                        ConsoleColor.Green.WriteConsole("Data update succes");
                    }

                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    ConsoleColor.Blue.WriteConsole("Do you want to continue the process?\n1-Yes(press any button)   2-No,Back Menu");
                    string choose = Console.ReadLine();
                    if (choose == "2")
                    {
                        return;
                    }
                    goto Id;
                }

            }
            else
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                goto Id;
            }
        }

        public async Task SearchByNameAsync()
        {
             Console.WriteLine("Enter search text");
            string seacrhText=Console.ReadLine();
            var data =await _educationService.SearchByNameAsync(seacrhText);
           foreach (var item in data)
            {
                Console.WriteLine("Education-"+item.Name+" Color-"+item.Color+" CreatedDate-"+item.CreatedDate);
            }
        }


    }
}
