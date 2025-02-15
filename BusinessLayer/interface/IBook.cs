using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IBook
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int bookId);
        Task<int> InsertBookAsync(Book book);
        Task<int> UpdateBookAsync(Book book);
        Task<int> DeleteBookAsync(int bookId);
    }
}
