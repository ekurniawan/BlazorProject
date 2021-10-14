using AutoMapper;
using BlazorProject.Client.Models;
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

        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int.TryParse(Id, out int employeeId);
            //jika update data
            if (employeeId != 0)
            {
                Employee = await EmployeeService.GetById(Convert.ToInt32(Id));
            }
            else
            {
                Employee = new Employee
                {
                    DepartmentId = 1,
                    DateOfBirth = DateTime.Now,
                    PhotoUrl = "images/nophoto.jpg"
                };
            }
            Departments = (await DepartmentService.GetAll()).ToList();
            Mapper.Map(Employee, EditEmployeeModel);
        }

        protected async Task HandleValidSubmit()
        {
            Mapper.Map(EditEmployeeModel, Employee);

            Employee result = null;

            if(Employee.EmployeeId != 0)
            {
                result = await EmployeeService.Update(int.Parse(Id),Employee);
            }
            else
            {
                result = await EmployeeService.Add(Employee);
            }

            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
