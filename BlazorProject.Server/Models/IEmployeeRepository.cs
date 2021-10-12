using BlazorProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Models
{
    public interface IEmployeeRepository : ICrud<Employee>
    {
        
    }
}
