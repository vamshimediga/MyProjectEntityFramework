using Data;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class StudentRepository : IStudent
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> allStudents()
        {
            // Fetch students using the stored procedure
            List<Student> students = await _context.Students
                .FromSqlRaw("EXEC GetAllStudents")
                .ToListAsync(); // This will return a list of students

            // Eagerly load the Institute for each student using Include (client-side)
            foreach (var student in students)
            {
                await _context.Entry(student)
                    .Reference(s => s.Institute) // Load the Institute navigation property
                    .LoadAsync();
            }

            return students;
        }





        public async Task<int> delete(int id)
        {
            SqlParameter studentIdParam = new SqlParameter("@StudentId", id);
            SqlParameter deletedStudentIdParam = new SqlParameter("@DeletedStudentId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[DeleteStudent] @StudentId, @DeletedStudentId OUTPUT",
                studentIdParam, deletedStudentIdParam);

            // Return the deleted StudentId from the output parameter, or null if no rows were deleted
            return (int)(deletedStudentIdParam.Value as int?);
        }

        public async Task<int> insert(Student student)
        {
            var parameters = new[]{
        new SqlParameter("@Name", student.Name),
        new SqlParameter("@InstituteId", student.InstituteId),
        new SqlParameter("@InsertedStudentId", SqlDbType.Int){ Direction = ParameterDirection.Output}
                       };

            // Execute the stored procedure with parameters
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[InsertStudent] @Name, @InstituteId, @InsertedStudentId OUTPUT",
                parameters);

            // Return the inserted StudentId from the output parameter
            return (int)parameters[2].Value;
        }

        public async Task<Student> students(int id)
        {  // Create the SQL parameter for the stored procedure
           // Define the SQL query for the stored procedure
            var sql = "EXEC [dbo].[GetStudentById] @StudentId = {0}";

            // Execute the stored procedure and get the result as a list
            Student student = _context.Students
                .FromSqlRaw(sql, id)  // Pass the parameter to the SQL query
                .AsEnumerable()  // Move the query to the client side (client-side composition)
                .FirstOrDefault(); // Get the first student or null if not found

            // Optionally, load related entities (like Institute) if needed
            if (student != null)
            {
                // Explicitly load the related Institute navigation property
                await _context.Entry(student)
                    .Reference(s => s.Institute)
                    .LoadAsync();
            }

            return student;
        }

        public async Task<int> update(Student student)
        {
            var parameters = new[]
    {
        new SqlParameter("@StudentId", student.StudentId),
        new SqlParameter("@Name", student.Name),
        new SqlParameter("@InstituteId", student.InstituteId),
        new SqlParameter("@UpdatedStudentId", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        }
    };

            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[UpdateStudent] @StudentId, @Name, @InstituteId, @UpdatedStudentId OUTPUT",
                parameters);

            // Return the updated StudentId from the output parameter (or null if no update)
            return (int)(parameters[3].Value as int?);
        }
    }
}
