using BlazorProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorProject.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private HttpClient _httpClient;
        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<Employee> Add(Employee obj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Employee>>("api/Employees");
        }

        public async Task<Employee> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Employee>($"api/Employees/{id}");
        }

        public async Task<Employee> Update(int id, Employee obj)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Employees/{id}", obj);
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Employee>(
                    await response.Content.ReadAsStreamAsync());
            }
            else
            {
                throw new Exception("Gagal update Employee");
            }
        }
    }
}
