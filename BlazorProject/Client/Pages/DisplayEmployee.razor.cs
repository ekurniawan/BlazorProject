using BlazorProject.Client.Services;
using BlazorProject.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Client.Pages
{
    public partial class DisplayEmployee
    {
        [Parameter]
        public Employee Employee { get; set; }

        protected bool IsSelected { get; set; }

        [Parameter]
        public EventCallback<bool> OnEmployeeSelection { get; set; }

        [Parameter]
        public EventCallback OnEmployeeDeleted { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Act.Components.Confirm DeleteConfirmation { get; set; }

        protected async Task CheckBoxChange(ChangeEventArgs e)
        {
            IsSelected = (bool)e.Value;
            await OnEmployeeSelection.InvokeAsync(IsSelected);
        }

        protected void Delete_Click()
        {
            DeleteConfirmation.Show();
        }

        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await EmployeeService.Delete(Employee.EmployeeId);
                await OnEmployeeDeleted.InvokeAsync(Employee.EmployeeId);
            }
        }

        /*protected async Task Delete_Click()
        {
            await EmployeeService.Delete(Employee.EmployeeId);
            await OnEmployeeDeleted.InvokeAsync();
        }*/
    }
}
