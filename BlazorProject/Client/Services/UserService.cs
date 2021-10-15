using BlazorProject.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorProject.Client.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
    }
    public class UserService : IUserService
    {
        private HttpClient _httpClient;
        private ILocalStorageService _localStorageService;
        private NavigationManager _navigationManager;

        public UserService(HttpClient httpClient,ILocalStorageService localStorageService,
            NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Users");
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

            return await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
        }
    }
}
