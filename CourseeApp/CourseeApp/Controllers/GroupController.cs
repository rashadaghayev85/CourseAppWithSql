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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Repository.Migrations;

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
        public async Task GetAllGroupWithEducationIdAsync()
        {
            var edu=await _educationService.GetAllAsync();
            foreach(var item in edu)
            {
                Console.WriteLine("Id-"+item.Id+" Name-"+item.Name);
            }
            Id: ConsoleColor.DarkYellow.WriteConsole("Choose Id");
            string idStr=Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idStr))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Id;
            }
            int id;
            bool isCorrectIdFormat=int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
                var groups = await _groupService.GetGroupByEducationIdAsync(id);
                if (groups.Count==0)
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                }
                foreach (var item in groups)
                {
                    string result = $"Name-{ item.Name} CreatedDate-{item.CreatedDate}" ; 
                    Console.WriteLine(result);
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                goto Id;
            }
            
        }
        public async Task CreateAsync()
        {
            var edu=await _educationService.GetAllAsync();
            if (edu.Count==0)
            {
                ConsoleColor.Red.WriteConsole("You cannot create a group because there is no education");
                return;
            }
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
                if (string.IsNullOrWhiteSpace(capacityStr))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty");
                    goto Capacity;
                }
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
            else
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                goto EducationName;
            }
            
        }

        public async Task DeleteAsync()
        {
            var data = await _groupService.GetAllAsync();
            foreach (var item in data)
            {
               
                Console.WriteLine("Id-" + item.Id + " Name-" + item.Name + " CreatedDate-" + item.CreatedDate);
            }

        Id: ConsoleColor.Yellow.WriteConsole("Select the id you want to delete");
            string idStr = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idStr))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Id;
            }
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
                Group response = await _groupService.GetByIdAsync(id);
                if (response is not null)
                {
                    await _groupService.DeleteAsync(response);
                    ConsoleColor.Green.WriteConsole(ResponseMessages.SuccesOperation);
                }
                else
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                }
              
            }
            else
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                goto Id;
            }


        }
        public async Task GetByIdAsync()
        {
            var gru = await _groupService.GetAllAsync();
            foreach (var item in gru)
            {
                ConsoleColor.DarkMagenta.WriteConsole("Id-" + item.Id + " Name-" + item.Name);
            }
        Id: ConsoleColor.Yellow.WriteConsole("Enter Id");
            string idStr = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idStr))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Id;
            }
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
                var item = await _groupService.GetByIdAsync(id);
                if (item is null )
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    goto Id;
                }
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
                var education = await _educationService.GetByIdAsync(item.EducationId);
                Console.WriteLine("Id-"+item.Id+" Group-" + item.Name + " Capacity-" + item.Capacity + " Education-" + education.Name + " CreatedDate-" + item.CreatedDate);
            }
            bool update = true;
        Id: ConsoleColor.Yellow.WriteConsole("Select the Id you want to update:");
            string idStr = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idStr))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Id;
            }
            int id;
            bool isCorrectIdFormat = int.TryParse(idStr, out id);
            if (isCorrectIdFormat)
            {
                try
                {
                    var data = _groupService.GetByIdAsync(id);
                    
                     //Console.WriteLine(data.Result.Name);
                    if (data.Result is null)
                    {
                        ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                        //ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                        goto Id;
                    }
                    Group:Console.WriteLine("Enter new Group name ");
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
                        else
                        {
                            ConsoleColor.Red.WriteConsole("This group is already available");
                            goto Group;
                        }

                    }
                    Education:Console.WriteLine("Enter new Education");
                    string newEducation = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newEducation))
                    {
                        var edu = await _educationService.SearchByNameAsync(newEducation);
                        if (edu.Count != 0)
                        {
                            if (data.Result.Education.Name.ToLower().Trim() != newEducation.ToLower().Trim())
                            {
                                data.Result.Education.Name = newEducation;
                                update = false;
                            }
                        }

                        else
                        {
                            ConsoleColor.Red.WriteConsole("This Education is not Exist");
                            goto Education;
                        }

                    }
                  Capacity: Console.WriteLine("Enter new capacity");
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
                        else
                        {
                            ConsoleColor.Red.WriteConsole(ResponseMessages.IncorrectFormat);
                            goto Capacity;
                        }
                    }

                    if (update)
                    {
                        ConsoleColor.DarkYellow.WriteConsole("there was no change");
                    }
                    else
                    {
                       // data.Result.CreatedDate = DateTime.Now;
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
                Text:ConsoleColor.Yellow.WriteConsole("Enter search text");
                string seacrhText = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(seacrhText))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty");
                    goto Text;
                }
                var data = await _groupService.SearchByNameAsync(seacrhText);
                if (data.Count==0)
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                }
                foreach (var item in data)
                {
                    var education = await _educationService.GetByIdAsync(item.EducationId);
                    Console.WriteLine("Group-" + item.Name + " Education-" + education.Name + " Capacity-" + item.Capacity + " CreatedDate-" + item.CreatedDate);
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
                Choose: ConsoleColor.Cyan.WriteConsole("Choose Sort Type\n Asc or Desc");
                string text = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(text))
                {
                    ConsoleColor.Red.WriteConsole("Input can't be empty");
                    goto Choose;
                }
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
                Console.WriteLine("Id-" + data.Id + " Education-" + data.Name);
            }
            Name: ConsoleColor.Yellow.WriteConsole("Add Education Name");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleColor.Red.WriteConsole("Input can't be empty");
                goto Name;
            }
            var response = await _educationService.GetByNameAsync(name);

            if (response is null )
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                goto Name;
            }
           
               
                var groups = await _groupService.GetGroupByEducationIdAsync(response.Id);
            if (groups != null)
            {
                foreach (var item in groups)
                {
                    Console.WriteLine("Group-" + item.Name + " Capacity-" + item.Capacity + " CreatedDate-" + item.CreatedDate);

                }
            }

            else
            {
                ConsoleColor.Red.WriteConsole("Incorrect Education Name");
                goto Name;
            }


            


        }
    }
}
