using BlazorProject.Client.Models;
using BlazorProject.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var uri = new Uri("https://localhost:6001");
            //var uri = new Uri("https://localhost:44328");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = uri });
            builder.Services
                .AddScoped<IEmployeeService, EmployeeService>()
                .AddScoped<IDepartmentService, DepartmentService>()
                .AddScoped<IAuthenticationService,AuthenticationService>()
                .AddScoped<IUserService,UserService>()
                .AddScoped<ILocalStorageService,LocalStorageService>();

            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddMudServices();

            var host = builder.Build();

            var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
            await authenticationService.Initialize();
            await host.RunAsync();
        }
    }
}
