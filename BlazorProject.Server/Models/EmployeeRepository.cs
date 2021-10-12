using BlazorProject.Server.Data;
using BlazorProject.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private AppDbContext _appDbContext;
        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Employee> Add(Employee obj)
        {
            try
            {
                var result = await _appDbContext.Employees.AddAsync(obj);
                await _appDbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task Delete(int id)
        {
            var result = await GetById(id);
            if(result!=null)
            {
                _appDbContext.Employees.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Data tidak ditemukan");
            }
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var results = await _appDbContext.Employees.OrderBy(e=>e.FirstName).ToListAsync();
            return results;
        }

        public async Task<Employee> GetById(int id)
        {
            var result = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            return result;
        }

        public async Task<Employee> Update(int id, Employee obj)
        {
            var result = await GetById(id);
            if (result != null)
            {
                result.FirstName = obj.FirstName;
                result.LastName = obj.LastName;
                result.Email = obj.Email;
                result.DateOfBirth = obj.DateOfBirth;
                result.Gender = obj.Gender;
                result.DepartmentId = obj.DepartmentId;
                result.PhotoUrl = obj.PhotoUrl;
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            else
            {
                throw new Exception("Data tidak ditemukan");
            }
        }
    }
}
