using Domain.Models;
using Repository.DTOs.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IEducationRepository:IBaseRepository<Education>
    {
        Task<List<Education>> SearchByNameAsync(string searchText);
        Task<Education> GetByNameAsync(string name);
        Task<List<EducationWithGroupsDto>> GetAllWithGroupAsync();
        Task<List<Education>> SortWithCreatedDateAsync(string text);

        Task<Education> GetByColor(string color);
    }
}
