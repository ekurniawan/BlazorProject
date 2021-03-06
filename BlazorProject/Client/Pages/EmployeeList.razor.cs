using BlazorProject.Client.Services;
using BlazorProject.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Client.Pages
{
    public partial class EmployeeList
    {
        public IEnumerable<Employee> Employees { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public int SelectEmployeeCount { get; set; } = 0;

        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeService.GetAll()).ToList();
        }

        protected void EmployeeSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {
                SelectEmployeeCount++;
            }
            else
            {
                SelectEmployeeCount--;
            }
        }

        protected async Task EmployeeDeleted()
        {
            Employees = (await EmployeeService.GetAll()).ToList();
        }
    }
}
