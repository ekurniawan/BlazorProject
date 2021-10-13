using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Shared
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        
        [Required(ErrorMessage ="First Name harus diisi data")]
        [StringLength(100,MinimumLength =2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name harus diisi data")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string PhotoUrl { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
