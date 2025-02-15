using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IEmployees
    {
        Task<List<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int employeeId);

        Task<bool> Insert(Employee employee);   

        Task<bool> Update(Employee employee);

        Task<bool> Delete(int id);

    }
}
