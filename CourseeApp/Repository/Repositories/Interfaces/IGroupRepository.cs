using Domain.Models;
using Repository.DTOs.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IGroupRepository:IBaseRepository<Group>
    {
        Task<List<Group>> SearchByNameAsync(string searchText);
        Task<Group> GetByNameAsync(string name);
        Task<List<GroupWithEducationDto>> GetAllWithEducationAsync();

        Task Delete(int id);
        Task<List<Group>> SortWithCapacityAsync(string text);
    }
}
