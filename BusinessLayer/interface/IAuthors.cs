using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IAuthors
    {
        Task<List<Author>> GetAuthors();
        Task<Author> GetById(int id);   
        Task Insert(Author author);
        Task Delete(int id);
        Task Update(Author author);

    }
}
