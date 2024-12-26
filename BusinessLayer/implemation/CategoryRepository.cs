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
             await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[InsertCategory] @CategoryName = {0}, @Description = {1}",
               category.CategoryName, category.Description);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
             await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[UpdateCategory] @CategoryId = {0}, @CategoryName = {1}, @Description = {2}",
                category.CategoryId, category.CategoryName, category.Description);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
             await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[DeleteCategory] @CategoryId = {0}", categoryId);
        }
    }
}
