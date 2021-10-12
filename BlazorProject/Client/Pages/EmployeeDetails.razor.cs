using BlazorProject.Client.Services;
using BlazorProject.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Client.Pages
{
    public partial class EmployeeDetails
    {
        public Employee Employee { get; set; } = new Employee();

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Parameter]
        public string id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            id = id ?? "1";
            Employee = await EmployeeService.GetById(Convert.ToInt32(id));
        }
    }
}
