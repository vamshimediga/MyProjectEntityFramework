using Data;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class PostRepository : IPosts
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> DeletePostAsync(int postId)
        {
            var resultParam = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var postIdParam = new SqlParameter("@PostId", postId);

            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeletePost @PostId, @Result OUT",
                postIdParam, resultParam
            );

            // Return the result value from the OUTPUT parameter
            return (int)resultParam.Value;
        }

        
        public async Task<PostDomainModel> GetPostByIdAsync(int postId)
        {
            // Define the SQL query for the stored procedure
            var sql = "EXEC [dbo].[GetPostById] @PostId = {0}";

            // Execute the stored procedure and get the result as a list
            PostDomainModel post = _context.posts
                .FromSqlRaw(sql, postId)  // Pass the parameter to the SQL query
                .AsEnumerable()  // Move the query to the client side (client-side composition)
                .FirstOrDefault(); // Get the first student or null if not found

            // Optionally, load related entities (like Institute) if needed
            if (post != null)
            {
                // Explicitly load the related Institute navigation property
                await _context.Entry(post)
                    .Reference(p => p.User)
                    .LoadAsync();
            }
            
                    

            return post;
        }

        public async Task<int> InsertPostAsync(PostDomainModel model)
        {
           
            var postIdParam = new SqlParameter("@PostId", SqlDbType.Int) { Direction = ParameterDirection.Output };

            // Execute the stored procedure
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC InsertPost @UserId, @Title, @Content, @PublishedAt,@PostId OUT",
                new SqlParameter("@UserId", model.UserId),
                new SqlParameter("@Title", model.Title),
                 new SqlParameter("@Content", model.Content),
                 new SqlParameter("@PublishedAt", model.PublishedAt),
                postIdParam
            );

            // Return the UserId from the output parameter
            return (int)postIdParam.Value;
        }
    

        public async Task<int> UpdatePostAsync(PostDomainModel model)
        {
            var resultParam = new SqlParameter("@Result", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            var postIdParam = new SqlParameter("@PostId", model.PostId);
            var userIdParam = new SqlParameter("@UserId", model.UserId);
            var titleParam = new SqlParameter("@Title", model.Title);
            var contentParam = new SqlParameter("@Content", model.Content);
            var publishedAtParam = new SqlParameter("@PublishedAt", model.PublishedAt);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdatePost @PostId, @UserId, @Title, @Content, @PublishedAt, @Result OUTPUT",
                postIdParam, userIdParam, titleParam, contentParam, publishedAtParam, resultParam);

            int result = (int)resultParam.Value;
            return result;
        }

       public async Task<List<PostDomainModel>> Getpostlist()
        {

            List<PostDomainModel> posts = await _context.posts
               .FromSqlRaw("EXEC GetPosts")
               .ToListAsync(); // This will return a list of users

            // Eagerly load the Posts for each user using Collection (client-side)
            foreach (PostDomainModel post in posts)
            {
                // Use Collection() for loading collection navigation property (Posts)
                await _context.Entry(post)
                    .Reference(p => p.User)
                    .LoadAsync();
            }

            return posts;
        }
    }
}
