using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.DTOs.Education;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        private readonly AppDbContext _context;
        public EducationRepository()
        {
            _context = new AppDbContext();
        }

        public async Task<List<EducationWithGroupsDto>> GetAllWithGroupAsync()
        {
            var education = await _context.Educations.Include(m => m.Group).ToListAsync();
            var datas = education.Select(m => new EducationWithGroupsDto
            {
                Education = m.Name,
                Groups = m.Group.Select(m => m.Name).ToList()
            });
            return datas.ToList();
        }

        public async Task<Education> GetByNameAsync(string name)
        {
            return await _context.Educations.FirstOrDefaultAsync(m => m.Name == name);
        }

        public async Task<List<Education>> SearchByNameAsync(string searchText)
        {
            return await _context.Educations.Where(m => m.Name.ToLower().Trim().Contains(searchText.ToLower().Trim())).ToListAsync();
        }

        public async Task<List<Education>> SortWithCreatedDateAsync(string text)
        {
            if (text.ToLower().Trim() == "asc")
            {
                return await _context.Educations.OrderBy(m => m.CreatedDate).ToListAsync();
            }
            else if (text.ToLower().Trim()=="desc")
            {
                return await _context.Educations.OrderByDescending(m => m.CreatedDate).ToListAsync();
            }
            else
            {
                throw new Exception("Incorrect Operation");
            }
           
        }
    }
}
