using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using m = Employee.Api.Models;

namespace Employee.Api.Abstraction
{
    public interface IEmployeeService
    {
        Task<IEnumerable<m.Employee>> GetEmployees();
        Task<IEnumerable<m.Employee>> Search(string SearchString);
        Task<m.Employee> GetEmployee(int Id);
        Task<bool> AddEmployee(m.Employee employee);
        Task<bool> EditEmployee(m.Employee employee);
        Task<bool> DeleteEmployee(int Id);
    }
}
