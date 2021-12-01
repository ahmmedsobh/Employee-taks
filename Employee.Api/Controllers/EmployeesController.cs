using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employee.Api.Data;
using Employee.Api.Models;
using Employee.Api.Abstraction;

namespace Employee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService EmployeeServices;

        public EmployeesController(IEmployeeService EmployeeServices)
        {
            this.EmployeeServices = EmployeeServices;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Employee>>> GetEmployees()
        {
            return Ok(await EmployeeServices.GetEmployees());
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Employee>> GetEmployee(int id)
        {
            return Ok(await EmployeeServices.GetEmployee(id));
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id,Models.Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            var r = await EmployeeServices.EditEmployee(employee);

            if(r)
            {
                return Ok();
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.Employee>> PostEmployee(Models.Employee employee)
        {
            var r = await EmployeeServices.AddEmployee(employee);

            if(r)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var r = await EmployeeServices.DeleteEmployee(id);
            if(r)
            {
                return Ok();
            }

            return NoContent();
        }

        
    }
}
