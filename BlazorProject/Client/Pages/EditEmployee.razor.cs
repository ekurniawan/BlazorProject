using BlazorProject.Client.Services;
using BlazorProject.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Client.Pages
{
    public partial class EditEmployee
    {
        public Employee Employee { get; set; } = new Employee();

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetById(Convert.ToInt32(Id));
            Departments = (await DepartmentService.GetAll()).ToList();
        }
    }
}
