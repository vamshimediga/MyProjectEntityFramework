using Data;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.FromSqlRaw("EXEC [dbo].[GetAllCategories]").ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            var sql = "EXEC [dbo].[GetCategoryById] @CategoryId = {0}";

            // Execute the SQL and retrieve the result
            var category = _context.Categories
                                   .FromSqlRaw(sql, categoryId)
                                   .AsEnumerable()  // Forces client-side processing
                                   .FirstOrDefault();  // Use synchronous method to get the first result

            return (category);  // Wrap the result in a Task and return
        }



        public async Task InsertCategoryAsync(Category category)
        {
            // Create SqlParameters for the stored procedure parameters
            var categoryNameParam = new SqlParameter("@CategoryName", category.CategoryName ?? (object)DBNull.Value);
            var descriptionParam = new SqlParameter("@Description", category.Description ?? (object)DBNull.Value);

            // Execute the stored procedure with parameters
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[InsertCategory] @CategoryName, @Description",
                categoryNameParam, descriptionParam);
        }


        public async Task UpdateCategoryAsync(Category category)
        {
            // Create SqlParameters for the stored procedure parameters
            SqlParameter categoryIdParam = new SqlParameter("@CategoryId", category.CategoryId);
            SqlParameter categoryNameParam = new SqlParameter("@CategoryName", category.CategoryName ?? (object)DBNull.Value);
            var descriptionParam = new SqlParameter("@Description", category.Description ?? (object)DBNull.Value);

            // Execute the stored procedure with parameters
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[UpdateCategory] @CategoryId, @CategoryName, @Description",
                categoryIdParam, categoryNameParam, descriptionParam);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            // Define the SQL parameter
            var categoryIdParam = new SqlParameter("@CategoryId", categoryId);

            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[DeleteCategory] @CategoryId", categoryIdParam);
        }

    }
}
