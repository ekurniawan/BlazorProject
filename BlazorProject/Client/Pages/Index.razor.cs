using BlazorProject.Client.Services;
using BlazorProject.Shared;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Client.Pages
{
    [Authorize]
    public partial class Index
    {
        private bool loading;
        private IEnumerable<User> users;

        public IUserService UserService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            users = await UserService.GetAll();
            loading = false;
        }
    }
}
