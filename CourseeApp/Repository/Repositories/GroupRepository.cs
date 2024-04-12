using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        private readonly AppDbContext _context;
        public GroupRepository()
        {
            _context = new AppDbContext();
        }

        public async Task<Group> GetByNameAsync(string name)
        {
            return await _context.Groups.FirstOrDefaultAsync(m => m.Name == name);
        }

        public async Task<List<Group>> SearchByNameAsync(string searchText)
        {
            return await _context.Groups.Where(m => m.Name.ToLower().Trim().Contains(searchText.ToLower().Trim())).ToListAsync();
        }

    }
}
