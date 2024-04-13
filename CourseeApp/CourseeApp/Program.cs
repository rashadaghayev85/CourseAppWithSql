using CourseeApp.Controllers;
using Service.Helpers.Extensions;
using Service.Helpers.Enums;


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

while (true)
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
                educationController.CreateAsync();
                break;
            case (int)OperationType.EducationDelete:
                educationController.DeleteAsync();
                break;
            case (int)OperationType.EducationUpdate:
                educationController.UpdateAsync();
                break;

            case (int)OperationType.EducationGetAll:
                educationController.GetAllAsync();
                break;
            case (int)OperationType.EducationGetAllWithGroup:
                educationController.GetAllWithGroupAsync();
                break;
            case (int)OperationType.EducationGetById:
                educationController.GetByIdAsync();
                break;

            case (int)OperationType.EducationSortWithCreatedDate:
                educationController.SortWithCreatedDateAsync();
                break;
            case (int)OperationType.EducationSearchByName:
                educationController.SearchByNameAsync();
                break;
            case (int)OperationType.GroupCreate:
                groupController.CreateAsync();
                break;
            case (int)OperationType.GroupDelete:
                groupController.DeleteAsync();
                break;
            case (int)OperationType.GroupUpdate:
                groupController.UpdateAsync();
                break;
            case (int)OperationType.GroupGetAll:
                groupController.GetAllAsync();
                break;
            case (int)OperationType.GroupSortWithCapacity:
                groupController.GetAllGroupWithEducationAsync();
                break;
            case (int)OperationType.GroupGetById:
                groupController.GetByIdAsync();
                break;
            case (int)OperationType.GetAllGroupWithEducation:
                groupController.GetAllGroupWithEducationAsync();
                break;
            case (int)OperationType.GroupSearchByName:
                groupController.SearchByNameAsync();
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
static void GetMenues()
{
    ConsoleColor.Cyan.WriteConsole("Choose one operation :\n" +
                                     "    -----------------------------------" + "--------------------------------\n" +
                                    "    | Education options:               |" + "Group options:                  |\n" +
                                    "    |----------------------------------|" + "--------------------------------|\n" +
                                    "    | 1-Education Create               |" + "9-Group Create                  |\n" +
                                    "    | 2-Education Delete               |" + "10-Group Delete                 |\n" +
                                    "    | 3-Education Uptade               |" + "11-Group Update                 |\n" +
                                    "    | 4-Education Get All              |" + "12-Group Get All                |\n" +
                                    "    | 5-Education Get All With Group   |" + "13-Group Sort With Capacity     |\n" +
                                    "    | 6-Education Sort With CreatedDate|" + "14-Group Get By Id              |\n" +
                                    "    | 7-Education Get By Id            |" + "15-Get All Group With Education |\n" +
                                    "    | 8-Education Search By Name       |" + "16-Group Search By Name         |\n" +
                                    "    --------------------------------------------------------------------------\n");


}