using Employee.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Api.Helpers
{
    public static class DataInitializer
    {
        public static void SeedData(ApplicationDbContext context)
        {
            try
            {
                var employee = new Models.Employee()
                {
                    Name = "Ahmed Sobh",
                    Age = 28,
                    Salary = 6000
                };

                context.Employees.Add(employee);
                context.SaveChanges();
            }
            catch
            {

            }
        }
    }
}
