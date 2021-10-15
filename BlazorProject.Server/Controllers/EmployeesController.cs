using BlazorProject.Server.Helpers;
using BlazorProject.Server.Models;
using BlazorProject.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorProject.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeRepository _employeeRepo;
        public EmployeesController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var results = await _employeeRepo.GetAll();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Kesalahan gagal mengambil data dari table Employee");
            }
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            try
            {
                var result = await _employeeRepo.GetById(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Gagal ambil data dati database");
            }
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest();
                var newEmployee = await _employeeRepo.Add(employee);
                return CreatedAtAction(nameof(Get), new { id = newEmployee.EmployeeId }, newEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Gagal tambah data Employee");
            }
        }

        // PUT api/<EmployeesController>/5
        //[ApiExplorerSettings(IgnoreApi =true)]
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> Put(int id, Employee employee)
        {
            try
            {
                var updateEmployee = await _employeeRepo.Update(id, employee);
                return Ok(updateEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _employeeRepo.Delete(id);
                return Ok("Data employee berhasil di delete");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
