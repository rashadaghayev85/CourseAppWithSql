using Service.Services.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

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
                 Console.WriteLine(item.Name+" "+item.CreatedDate);
            }
            
        }
        public async Task CreateAsync()
        {
             Console.WriteLine("Add Education");
             string name =Console.ReadLine();
             Console.WriteLine("Add Education color");
             string color = Console.ReadLine();

            
             DateTime time = DateTime.Now;

           
            await _educationService.CreateAsync(new Education { Name = name.Trim().ToLower(), Color = color.Trim(), CreatedDate =time });


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
                await _educationService.GetByIdAsync(id);
            }
            else
            {
                goto Id;
            }
            
        }


    }
}
