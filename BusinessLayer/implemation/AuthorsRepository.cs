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
    public class AuthorRepository : IAuthors
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<List<Author>> GetAuthors()
        {
            List<Author> authors = await _context.Authors
                    .FromSqlRaw("EXEC GetAllAuthors")  // Execute the stored procedure to get authors
                    .ToListAsync();  // This will return a list of authors

            // Now, load the related books for each author in a separate query
            foreach (var author in authors)
            {
                // Load related books for each author
                await _context.Entry(author)
                    .Collection(a => a.Books)  // Include the related Books collection
                    .LoadAsync();
            }

            return authors;
        }

        public async Task<Author> GetById(int id)
        {
            var sql = "EXEC [dbo].[GetAuthorById] @AuthorId = {0}";

            // Execute the stored procedure and get the result
            Author author = _context.Authors
                .FromSqlRaw(sql, id)  // Pass the parameter to the SQL query
                .AsEnumerable()  // Move the query to the client side (client-side composition)
                .FirstOrDefault(); // Get the first author or null if not found

       

            return author;
        }

        public async Task Insert(Author author)
        {
            var parameters = new SqlParameter("@Name", author.Name);
            // Execute the stored procedure with parameters
            await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[InsertAuthor] @Name",parameters);
        }

        public async Task Delete(int id)
        {
            SqlParameter authorIdParam = new SqlParameter("@AuthorId", id);
          

            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[DeleteAuthor] @AuthorId",authorIdParam);

        }

        public async Task Update(Author author)
        {
            var authorIdParameter = new SqlParameter("@AuthorId", author.AuthorId);
            var nameParameter = new SqlParameter("@Name", author.Name);
          

            // Execute the stored procedure with the parameters
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[UpdateAuthor] @AuthorId, @Name",
                authorIdParameter, nameParameter);
        }
    }
}
