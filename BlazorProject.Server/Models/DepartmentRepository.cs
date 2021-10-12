using BlazorProject.Server.Data;
using BlazorProject.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private AppDbContext _appDbContext;
        public DepartmentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<Department> Add(Department obj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _appDbContext.Departments.ToListAsync();
        }

        public async Task<Department> GetById(int id)
        {
            return await _appDbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);
        }

        public Task<Department> Update(int id, Department obj)
        {
            throw new NotImplementedException();
        }
    }
}
