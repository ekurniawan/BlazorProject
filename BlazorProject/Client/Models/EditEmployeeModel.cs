using BlazorProject.Shared;
using BlazorProject.Shared.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Client.Models
{
    public class EditEmployeeModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First Name harus diisi data")]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name harus diisi data")]
        public string LastName { get; set; }

        [Required]
        [EmailDomainValidator(AllowedDomain = "gmail.com")]
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string PhotoUrl { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
