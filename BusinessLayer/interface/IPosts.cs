using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IPosts
    {
        // Insert a new post
        Task<int> InsertPostAsync(PostDomainModel model);

        // Update an existing post
        Task<int> UpdatePostAsync(PostDomainModel model);

        // Delete a post
        Task<int> DeletePostAsync(int postId);

        // Get all posts
        Task<List<PostDomainModel>> Getpostlist();

        // Get a post by its ID
        Task<PostDomainModel> GetPostByIdAsync(int postId);
    }
}
