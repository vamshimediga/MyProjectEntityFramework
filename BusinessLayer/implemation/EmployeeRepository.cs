using Data;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class EmployeeRepository : IEmployees
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All Employees
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            // Retrieve employees using the stored procedure
            var employees = await _context.Employees
                .FromSqlRaw("EXEC GetAllEmployees")
                .AsNoTracking() // Optional: Avoid tracking if not needed
                .ToListAsync();

            // Load navigation properties if necessary
            foreach (var employee in employees)
            {
                _context.Entry(employee).Reference(e => e.Department).Load();
            }

            return employees;
        }


        // Get Employee By ID
        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            var employeeIdParam = new SqlParameter("@EmployeeID", employeeId);

            return await _context.Employees
                .FromSqlRaw("EXEC GetEmployeeById @EmployeeID", employeeIdParam)
                .Include(e => e.Department) // Include navigation property if needed
                .FirstOrDefaultAsync();
        }

        // Insert Employee
        public async Task<bool> Insert(Employee employee)
        {
            var employeeNameParam = new SqlParameter("@EmployeeName", employee.EmployeeName);
            var departmentIdParam = new SqlParameter("@DepartmentID", employee.DepartmentID);
            var newEmployeeIdParam = new SqlParameter("@NewEmployeeID", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC InsertEmployee @EmployeeName, @DepartmentID, @NewEmployeeID OUTPUT",
                employeeNameParam,
                departmentIdParam,
                newEmployeeIdParam
            );

            employee.EmployeeID = (int)newEmployeeIdParam.Value; // Set the generated EmployeeID
            return employee.EmployeeID > 0;
        }

        // Update Employee
        public async Task<bool> Update(Employee employee)
        {
            var employeeIdParam = new SqlParameter("@EmployeeID", employee.EmployeeID);
            var employeeNameParam = new SqlParameter("@EmployeeName", employee.EmployeeName);
            var departmentIdParam = new SqlParameter("@DepartmentID", employee.DepartmentID);
            var isUpdatedParam = new SqlParameter("@IsUpdated", System.Data.SqlDbType.Bit)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateEmployee @EmployeeID, @EmployeeName, @DepartmentID, @IsUpdated OUTPUT",
                employeeIdParam,
                employeeNameParam,
                departmentIdParam,
                isUpdatedParam
            );

            return (bool)isUpdatedParam.Value;
        }

        // Delete Employee
        public async Task<bool> Delete(Employee employee)
        {
            var employeeIdParam = new SqlParameter("@EmployeeID", employee.EmployeeID);
            var isDeletedParam = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteEmployee @EmployeeID, @IsDeleted OUTPUT",
                employeeIdParam,
                isDeletedParam
            );

            return (bool)isDeletedParam.Value;
        }
    }
}
