using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Enums
{
    public enum OperationType
    {
        EducationCreate=1,
        EducationDelete,
        EducationUpdate,
        EducationGetAll,
        EducationGetAllWithGroup,
        EducationGetById,
        EducationSortWithCreatedDate,
        EducationSearchByName,
        GroupCreate,
        GroupDelete,
        GroupUpdate,
        GroupGetAll,
        GroupSortWithCapacity,
        GroupGetById,
        GetAllGroupWithEducation,
        GroupSearchByName,

    }
}
