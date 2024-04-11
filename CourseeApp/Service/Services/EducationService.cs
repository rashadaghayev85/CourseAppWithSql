using Repository.Repositories.Interfaces;
using Repository.Repositories;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace Service.Services
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepo;
        public EducationService()
        {
            _educationRepo = new EducationRepository();
        }

        public async Task CreateAsync(Education entity)
        {
            await _educationRepo.CreateAsync(entity);
        }

        public async Task DeleteAsync(Education entity)
        {
           await _educationRepo.DeleteAsync(entity);
        }

        public async Task<List<Education>>GetAllAsync()
        {
            return await _educationRepo.GetAllAsync();
        }

        public async Task<Education> GetByIdAsync(int id)
        {
            return await _educationRepo.GetByIdAsync(id);
        }


        public async Task<List<Education>> SearchByNameAsync(string searchText)
        {
            return await _educationRepo.SearchByNameAsync(searchText);
        }

        public async Task UpdateAsync(Education entity)
        {
            await _educationRepo.UpdateAsync(entity);
        }
    }
}
