using BlazorProject.Server.Data;
using BlazorProject.Server.Helpers;
using BlazorProject.Server.Models;
using BlazorProject.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IUserService, UserService>();


            services.AddControllers();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlazorProject.Server", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlazorProject.Server v1"));
            }

            
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(policy => policy.WithOrigins("https://localhost:5001", "https://localhost:44384")
            .AllowAnyMethod().WithHeaders(HeaderNames.ContentType)
            );

            app.UseMiddleware<JwtMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
