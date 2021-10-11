using BlazorProject.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {}

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed department table
            modelBuilder.Entity<Department>()
                .HasData(new Department { DepartmentId = 1, DepartmentName = "IT" });
            modelBuilder.Entity<Department>()
                .HasData(new Department { DepartmentId = 2, DepartmentName = "HRD" });
            modelBuilder.Entity<Department>()
                .HasData(new Department { DepartmentId = 3, DepartmentName = "Payroll" });
            modelBuilder.Entity<Department>()
                .HasData(new Department { DepartmentId = 4, DepartmentName = "Infrastructure" });

            //seed employee
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 1,
                FirstName = "Erick",
                LastName = "Kurniawan",
                Email = "erick@gmail.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = Gender.Male,
                DepartmentId=1,
                PhotoUrl = "images/pic1.png"
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 2,
                FirstName = "Peter",
                LastName = "Parker",
                Email = "peter@gmail.com",
                DateOfBirth = new DateTime(1990, 2, 2),
                Gender = Gender.Male,
                DepartmentId = 2,
                PhotoUrl = "images/pic1.png"
            });
        }
    }
}
