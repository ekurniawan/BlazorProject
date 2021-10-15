using BlazorProject.Client.Helpers;
using BlazorProject.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Client.Pages
{
    public partial class Login
    {
        private Model model = new Model();
        private bool loading;
        private string error;

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            if (AuthenticationService.User != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        private async void HandleValidSubmit()
        {
            loading = true;
            try
            {
                await AuthenticationService.Login(model.Username, model.Password);
                var returnUrl = NavigationManager.QueryString("returnUrl") ?? "";
                NavigationManager.NavigateTo(returnUrl);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                loading = false;
                StateHasChanged();
            }
        }

        private class Model
        {
            [Required]
            public string Username { get; set; }
            [Required]
            public string Password { get; set; }
        }
    }

    
}
