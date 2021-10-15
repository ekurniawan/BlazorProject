using BlazorProject.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorProject.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private HttpClient _httpClient;
        private ILocalStorageService _localStorageService;
        private NavigationManager _navigationManager;

        public EmployeeService(HttpClient httpClient, ILocalStorageService localStorageService,
            NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
        }
        public async Task<Employee> Add(Employee obj)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Employees",obj);
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Employee>(
                    await response.Content.ReadAsStreamAsync());
            }
            else
            {
                throw new Exception("Gagal tambah data Employee");
            }
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/Employees/{id}");
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Employees");
            var user = await _localStorageService.GetItem<User>("user");
            if (user != null)
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", user.Token);

            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout");
                return default;
            }

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["message"]);
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
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
