using Domain.Models;
using Repository.DTOs.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IEducationService
    {
        Task CreateAsync(Education entity);
        Task UpdateAsync(Education entity);
        Task DeleteAsync(Education entity);
        Task<Education> GetByIdAsync(int id);
        Task<List<Education>> GetAllAsync();
        Task<List<Education>> SearchByNameAsync(string searchText);
        Task<Education> GetByNameAsync(string name);
        Task<List<EducationWithGroupsDto>>GetEducationWithGroupsAsync();
        Task<List<Education>> SortWithCreatedDateAsync(string text);
    }
}
