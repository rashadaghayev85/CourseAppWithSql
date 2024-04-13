using Service.Services.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
using Repository.DTOs.Group;

namespace CourseeApp.Controllers
{
    public class GroupController
    {
        private readonly IGroupService _groupService;
        private readonly IEducationService _educationService;

        public GroupController()
        {
            _groupService = new GroupService();
            _educationService=new EducationService();
        }


        public async Task GetAllAsync()
        {
            var data = await _groupService.GetAllAsync();
            
            foreach (var item in data)
            {
                var education = await _educationService.GetByIdAsync(item.EducationId);
                Console.WriteLine("Group-" + item.Name + " Capacity-" + item.Capacity + " Education-"+education.Name+" CreatedDate-" + item.CreatedDate);
            }

        }
        public async Task GetAllGroupWithEducationAsync()
        {
            var groups = await _groupService.GetAllWithEducationAsync();
            foreach (var item in groups)
            {
                string result = item.Group + "-" + string.Join(",", item.Education);
                Console.WriteLine(result);
            }
        }
        public async Task CreateAsync()
        {
            GroupName: Console.WriteLine("Add Group Name");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto GroupName;
            }
            ConsoleColor.DarkYellow.WriteConsole("Imposible variants");
            var response =await _educationService.GetAllAsync();
            foreach(var item in response)
            {
               
                ConsoleColor.Cyan.WriteConsole("Id-"+item.Id+" Name-"+item.Name);
            }
           EducationName: Console.WriteLine("Choose Education ID ");
            string idStr= Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idStr))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto EducationName;
            }
            int id;
            bool isCorrectIdFormat=int.TryParse(idStr, out id);

            if (isCorrectIdFormat)
            {

                Education education = await _educationService.GetByIdAsync(id);
                if (education == null)
                {
                    ConsoleColor.Red.WriteConsole("This Education not exist");
                    return;
                }


            Capacity: Console.WriteLine("Add Group Capacity");
                string capacityStr = Console.ReadLine();
                int capacity;
                bool isCorrectCapacityFormat = int.TryParse(capacityStr, out capacity);
                if (isCorrectCapacityFormat)
                {
                    DateTime time = DateTime.Now;
                  //DateOnly time=new DateOnly();
                    
                    await _groupService.CreateAsync(new Group { Name = name.Trim().ToLower(), EducationId =education.Id, Capacity = capacity, CreatedDate = time });
                    ConsoleColor.Green.WriteConsole("Data succesfuly added");
                }
                else
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                    goto Capacity;
                }
            }     
            
        }

        public async Task DeleteAsync()
        {
            var data = await _groupService.GetAllAsync();
            foreach (var item in data)
            {
                Console.WriteLine("Id-" + item.Id + " Name-" + item.Name + " CreatedDate-" + item.CreatedDate);
            }

        Id: Console.WriteLine("Select the id you want to delete");
            string idStr = Console.ReadLine();
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
                Group response = await _groupService.GetByIdAsync(id);
               await _groupService.DeleteAsync(response);
                ConsoleColor.Green.WriteConsole(ResponseMessages.SuccesOperation);
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
                var item = await _groupService.GetByIdAsync(id);
                Console.WriteLine("Group-" + item.Name + " Education-" + item.Education +" Capacity-"+item.Capacity+" CreatedDate-" + item.CreatedDate);
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                goto Id;
            }

        }
        public async Task UpdateAsync()
        {
            var datas = await _groupService.GetAllAsync();
            foreach (var item in datas)
            {
                Console.WriteLine("Id-" + item.Id + "Group-" + item.Name + " Education-" + item.Education + " Capacity-" + item.Capacity + " CreatedDate-" + item.CreatedDate);
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
                    var data = _groupService.GetByIdAsync(id);
                     //Console.WriteLine(data.Result.Name);
                    if (data is null)
                    {
                        throw new NotFoundException(ResponseMessages.DataNotFound);
                        //ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    }
                    Console.WriteLine("Enter new Group name ");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        var response = await _groupService.SearchByNameAsync(newName);
                        if (response.Count == 0)
                        {
                            if (data.Result.Name.ToLower().Trim() != newName.ToLower().Trim())
                            {
                                data.Result.Name = newName;
                                update = false;
                            }
                        }

                    }
                    Console.WriteLine("Enter new Education");
                    string newEducation = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newEducation))
                    {
                        if (data.Result.Education.Name.ToLower().Trim() != newEducation.ToLower().Trim())
                        {
                            data.Result.Education.Name = newEducation;
                            update = false;
                        }


                    }
                    Console.WriteLine("Enter new capacity");
                    string capacityStr = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(capacityStr))
                    {
                        int capacity;
                        bool isCorrectCapacityFormat = int.TryParse(capacityStr, out capacity);
                        if (isCorrectCapacityFormat)
                        {

                            if (data.Result.Capacity != capacity)
                            {
                                data.Result.Capacity = capacity;
                                update = false;
                            }

                        }
                    }

                    if (update)
                    {
                        ConsoleColor.DarkYellow.WriteConsole("there was no change");
                    }
                    else
                    {
                        data.Result.CreatedDate = DateTime.Now;
                        _groupService.UpdateAsync(data.Result);
                        ConsoleColor.Green.WriteConsole("Data update succes");

                        //Console.WriteLine(_groupService.GetAllAsync().Result);//.Capacity);
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
            try
            {
                Console.WriteLine("Enter search text");
                string seacrhText = Console.ReadLine();
                var data = await _groupService.SearchByNameAsync(seacrhText);
                foreach (var item in data)
                {
                    Console.WriteLine("Group-" + item.Name + " Education-" + item.Education + " Capacity-" + item.Capacity + " CreatedDate-" + item.CreatedDate);
                }
            }
            catch (Exception ex)
            {

                ConsoleColor.Red.WriteConsole(ex.Message);
            }
           
        }
        public async Task SortWithCapacityAsync()
        {
            try
            {
                ConsoleColor.Cyan.WriteConsole("Choose Sort Type\n Asc or Desc");
                string text = Console.ReadLine();
                var datas = await _groupService.SortWithCapacityAsync(text);
                foreach (var data in datas)
                {
                    Console.WriteLine("Name-" + data.Name + " CreateDate-" + data.Capacity);
                }
            }
            catch (Exception ex)
            {

                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }
        public async Task FilterByEducationNameAsync()
        {
            var datas = await _educationService.GetAllAsync();  
            foreach (var data in datas)
            {
                Console.WriteLine("Id-"+data.Id+" Education-"+data.Name);
            }
            Id: ConsoleColor.Yellow.WriteConsole("Add Education Id");
            string idStr = Console.ReadLine();
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
                var data = await _groupService.GetGroupByEducationIdAsync(id);
                foreach (var item in data)
                {
                    Console.WriteLine(item.Name);
                }
               
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                goto Id;
            }
        }
    }
}
