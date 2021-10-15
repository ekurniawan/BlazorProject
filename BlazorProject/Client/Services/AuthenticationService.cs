using BlazorProject.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorProject.Client.Services
{
    public interface IAuthenticationService
    {
        User User { get;  }
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
    }
    public class AuthenticationService : IAuthenticationService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;

        public User User { get; private set; }

        public AuthenticationService(HttpClient httpClient,
            NavigationManager navigationManager,ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<User>("user");
        }

        public async Task Login(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Users/authenticate",
                new { username,password });
            if (response.IsSuccessStatusCode)
            {
                User = await response.Content.ReadFromJsonAsync<User>();
            }
            await _localStorageService.SetItem("user", User);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login");
        }
    }
}
