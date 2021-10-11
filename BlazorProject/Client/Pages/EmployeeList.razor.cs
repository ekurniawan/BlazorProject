using BlazorProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Client.Pages
{
    public partial class EmployeeList
    {
        public IEnumerable<Employee> Employees { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        private void LoadEmployee()
        {
            Employee e1 = new Employee
            {
                EmployeeId = 1,
                FirstName = "Erick",
                LastName = "Kurniawan",
                Email = "erick@gmail.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = Gender.Male,
                Department = new Department { DepartmentId = 1, DepartmentName = "IT" },
                PhotoUrl = "images/pic1.jpg"
            };
            Employee e2 = new Employee
            {
                EmployeeId = 2,
                FirstName = "Peter",
                LastName = "Parker",
                Email = "peter@gmail.com",
                DateOfBirth = new DateTime(1990, 2, 2),
                Gender = Gender.Male,
                Department = new Department { DepartmentId = 1, DepartmentName = "HRD" },
                PhotoUrl = "images/pic2.jpg"
            };

            Employees = new List<Employee> { e1, e2 };
        }
    }
}
