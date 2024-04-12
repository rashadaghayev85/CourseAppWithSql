using CourseeApp.Controllers;

EducationController educationController = new EducationController();
//await educationController.GetAllAsync();
//await educationController.CreateAsync();
//await educationController.UpdateAsync();
//await educationController.DeleteAsync();
//await educationController.GetByIdAsync();
//await educationController.SearchByNameAsync();
//await educationController.GetAllAsync();

GroupController groupController = new GroupController();
//await groupController.GetAllAsync();
//await groupController.DeleteAsync();
//await groupController.UpdateAsync();
//await groupController.SearchByNameAsync();
await groupController.CreateAsync();
//await groupController.GetByIdAsync();