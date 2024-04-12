using Repository.Repositories.Interfaces;
using Repository.Repositories;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _GroupRepo;
        public GroupService()
        {
            _GroupRepo = new GroupRepository();
        }

        public Task CreateAsync(Group group)
        {
            throw new NotImplementedException();
        }

        public Task<List<Group>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
