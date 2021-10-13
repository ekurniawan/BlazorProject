using AutoMapper;
using BlazorProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Client.Models
{
    public class EmployeeProfile  : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EditEmployeeModel>()
                .ForMember(d => d.ConfirmEmail, opt => opt.MapFrom(s => s.Email));
            CreateMap<EditEmployeeModel, Employee>();
        }
    }
}
