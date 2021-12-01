using Employee.Api.Abstraction;
using Employee.Api.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Api
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext context;

        public EmployeeService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddEmployee(Models.Employee employee)
        {
            await context.Employees.AddAsync(employee);
            var r = await context.SaveChangesAsync();
            if(r > 0)
            {
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);

        }

        public async Task<bool> DeleteEmployee(int Id)
        {
            if(Id == 0)
            {
                return await Task.FromResult(false);
            }

            var employee = await GetEmployee(Id);
            if(employee == null)
            {
                return await Task.FromResult(false);
            }

            context.Employees.Remove(employee);
            var r = await context.SaveChangesAsync();
            if (r > 0)
            {
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);

        }

        public async Task<bool> EditEmployee(Models.Employee employee)
        {
            if(employee == null)
            {
                return await Task.FromResult(false);
            }

            var EmployeeToUpdate = await GetEmployee(employee.Id);
            if(EmployeeToUpdate == null)
            {
                return await Task.FromResult(false);
            }

            EmployeeToUpdate.Name = employee.Name;
            EmployeeToUpdate.Age = employee.Age;
            EmployeeToUpdate.Salary = employee.Salary;

            context.Employees.Update(EmployeeToUpdate);
            var r = await context.SaveChangesAsync();
            if (r > 0)
            {
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);


        }

        public async Task<Models.Employee> GetEmployee(int Id)
        {
            var employee = new Models.Employee();
            if (Id == 0)
            {
                return await Task.FromResult(employee);
            }

            employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == Id);
            return await Task.FromResult(employee);
        }

        public async Task<IEnumerable<Models.Employee>> GetEmployees()
        {
            return await context.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Models.Employee>> Search(string SearchString)
        {
            var employees = new List<Models.Employee>();
            if(SearchString == null || SearchString == "")
            {
                employees = await context.Employees.ToListAsync();
            }

            employees =await(from e in context.Employees
                         where e.Name.ToLower().Contains(SearchString)
                         || e.Salary.ToString().Contains(SearchString)
                         || e.Age.ToString().Contains(SearchString)
                         select e).ToListAsync();

            return employees;
        }
    }
}
