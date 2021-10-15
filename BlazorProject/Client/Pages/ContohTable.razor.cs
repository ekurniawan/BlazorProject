using BlazorProject.Client.Services;
using BlazorProject.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Client.Pages
{
    public partial class ContohTable
    {
        private bool _hidePosition;
        private bool _loading;
        protected IEnumerable<Department> Departments = new List<Department>();

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Departments = await DepartmentService.GetAll();
        }
    }
}
