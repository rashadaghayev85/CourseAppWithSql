using CourseeApp.Controllers;
using Service.Helpers.Extensions;
using Service.Helpers.Enums;
using Domain.Models;


//await educationController.GetAllAsync();
//await educationController.CreateAsync();
//await educationController.UpdateAsync();
//await educationController.DeleteAsync();
//await educationController.GetByIdAsync();
//await educationController.SearchByNameAsync();
//await educationController.GetAllWithGroupAsync();
//await educationController.SortWithCreatedDateAsync();

//await groupController.GetAllAsync();
//await groupController.DeleteAsync();
//await groupController.SearchByNameAsync();
//await groupController.CreateAsync();
//await groupController.GetByIdAsync();
//await groupController.UpdateAsync();
//await groupController.SortWithCapacityAsync();
//await groupController.GetAllGroupWithEducationAsync();






EducationController educationController = new EducationController();

GroupController groupController = new GroupController();

UserController userController = new UserController();

Home: ConsoleColor.Cyan.WriteConsole("1-Register,2-Login,3-Log Out");
string result=Console.ReadLine();
if (result == "1")
{
    await userController.Register();
    goto Home;
}
else if (result == "2")
{
    var response = await userController.Login();



while (true)
{
    if (response)
    {
        GetMenues();
    Operation: string operationStr = Console.ReadLine();

        int operation;

        bool isCorrectOperationFormat = int.TryParse(operationStr, out operation);
        if (isCorrectOperationFormat)
        {
            switch (operation)
            {
                case (int)OperationType.EducationCreate:
                    await educationController.CreateAsync();
                    break;
                case (int)OperationType.EducationDelete:
                    await educationController.DeleteAsync();
                    break;
                case (int)OperationType.EducationUpdate:
                    await educationController.UpdateAsync();
                    break;

                case (int)OperationType.EducationGetAll:
                    await educationController.GetAllAsync();
                    break;
                case (int)OperationType.EducationGetAllWithGroup:
                    await educationController.GetAllWithGroupAsync();
                    break;
                case (int)OperationType.EducationGetById:
                    await educationController.GetByIdAsync();
                    break;

                case (int)OperationType.EducationSortWithCreatedDate:
                    await educationController.SortWithCreatedDateAsync();
                    break;
                case (int)OperationType.EducationSearchByName:
                    await educationController.SearchByNameAsync();
                    break;
                case (int)OperationType.GroupCreate:
                    await groupController.CreateAsync();
                    break;
                case (int)OperationType.GroupDelete:
                    await groupController.DeleteAsync();
                    break;
                case (int)OperationType.GroupUpdate:
                    await groupController.UpdateAsync();
                    break;
                case (int)OperationType.GroupGetAll:
                    await groupController.GetAllAsync();
                    break;
                case (int)OperationType.GroupSortWithCapacity:
                    await groupController.SortWithCapacityAsync();
                    break;
                case (int)OperationType.GroupGetById:
                    await groupController.GetByIdAsync();
                    break;
                case (int)OperationType.GetAllGroupWithEducationId:
                    await groupController.GetAllGroupWithEducationIdAsync();
                    break;
                case (int)OperationType.GroupSearchByName:
                    await groupController.SearchByNameAsync();
                    break;
                case (int)OperationType.FilterByEducationName:
                    await groupController.FilterByEducationNameAsync();
                    break;
                default:
                    ConsoleColor.Red.WriteConsole("Operation is wrong, please choose again");
                    goto Operation;
            }
        }
        else
        {
            ConsoleColor.Red.WriteConsole("Operation format is wrong, please add operation again");
            goto Operation;
        }
    }
    else
    {
        ConsoleColor.Red.WriteConsole("Login Failed");
            goto Home;

    }
   
}
}
else
{
    return 0;
}
static void GetMenues()
{
    ConsoleColor.Cyan.WriteConsole("Choose one operation :\n" +
                                     "    -----------------------------------" + "----------------------------------\n" +
                                    "    | Education options:               |" + "Group options:                    |\n" +
                                    "    |----------------------------------|" + "----------------------------------|\n" +
                                    "    | 1-Education Create               |" + "9-Group Create                    |\n" +
                                    "    | 2-Education Delete               |" + "10-Group Delete                   |\n" +
                                    "    | 3-Education Uptade               |" + "11-Group Update                   |\n" +
                                    "    | 4-Education Get All              |" + "12-Group Get All                  |\n" +
                                    "    | 5-Education Get All With Group   |" + "13-Group Sort With Capacity       |\n" +
                                    "    | 6-Education Get By Id            |" + "14-Group Get By Id                |\n" +
                                    "    | 7-Education Sort With CreatedDate|" + "15-Get All Group With Education Id|\n" +
                                    "    | 8-Education Search By Name       |" + "16-Group Search By Name           |\n" +
                                    "    |                                  |" + "17-Filter By Education Name Async |\n" +
                                    "    ----------------------------------------------------------------------\n");


}
