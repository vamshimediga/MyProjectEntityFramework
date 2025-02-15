using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class DepartmentRepository : IDepartment
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All Departments with Navigation Properties (e.g., Employees)
        public async Task<List<Department>> GetDepartments()
        {
            // Fetch departments with employees included, then use AsEnumerable to allow further LINQ operations
            return await _context.Departments
                .FromSqlRaw("EXEC GetAllDepartments")
               // .AsEnumerable() // Forces the results to be fetched into memory
                .ToListAsync();
              //  .Include(d => d.Employees) // Now you can include the navigation property
        }

        // Get Department by ID with Navigation Properties (e.g., Employees)
        public async Task<Department> GetDepartmentById(int id)
        {
            // Use SqlParameter to pass the ID to the stored procedure
            var departmentIdParam = new SqlParameter("@DepartmentID", id);

            // Use AsEnumerable() to bring the result into memory before applying any operations
            return _context.Departments
                .FromSqlRaw("EXEC GetDepartmentById @DepartmentID", departmentIdParam)
                .AsEnumerable() // This forces the query to execute and return the result in memory
                .FirstOrDefault();
        }


        // Insert Department with Output Parameter
        public async Task<int> Insert(Department department)
        {
            // Define output parameter for the new department ID
            var newDepartmentIdParam = new SqlParameter() {
                ParameterName = "@NewDepartmentID",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            // Execute stored procedure and pass the department name along with the output parameter
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC InsertDepartment @DepartmentName = {0}, @NewDepartmentID = {1} OUTPUT",
                department.DepartmentName,
                newDepartmentIdParam
            );

            // Return the newly inserted department ID from the output parameter
            return (int)newDepartmentIdParam.Value;
        }

        // Update Department with Output Parameter
        public async Task<bool> Update(Department department)
        {
            // Define output parameter for checking if the department was updated
            var isUpdatedParam = new SqlParameter
            {
                ParameterName = "@IsUpdated",
                SqlDbType = System.Data.SqlDbType.Bit,
                Direction = System.Data.ParameterDirection.Output
            };

            // Execute stored procedure for updating the department and pass input and output parameters
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateDepartment @DepartmentID = {0}, @DepartmentName = {1}, @IsUpdated = {2} OUTPUT",
                department.DepartmentID,
                department.DepartmentName,
                isUpdatedParam
            );
            // Return the output value (1 if updated, 0 if not found)
            return (bool)isUpdatedParam.Value;
        }

        // Delete Department with Output Parameter
        public async Task<bool> Delete(int id)
        {
            // Define output parameter for checking if the department was deleted
            var isDeletedParam = new SqlParameter
            {
                ParameterName = "@IsDeleted",
                SqlDbType = System.Data.SqlDbType.Bit,
                Direction = System.Data.ParameterDirection.Output
            };

            // Execute stored procedure for deleting the department and pass input and output parameters
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteDepartment @DepartmentID = {0}, @IsDeleted = {1} OUTPUT",
                id,
                isDeletedParam
            );

            // Return the output value (1 if deleted, 0 if not found)
            return (bool)isDeletedParam.Value;
        }
    }
}
