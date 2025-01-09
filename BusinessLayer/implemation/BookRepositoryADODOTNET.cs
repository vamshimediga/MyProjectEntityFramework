using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Entities;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Implementation
{
    public class BookRepositoryADODOTNET : IBook
    {
        private readonly string _connectionString;

        // Inject IConfiguration into the constructor
        public BookRepositoryADODOTNET(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            var books = new List<Book>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("[dbo].[GetAllBooks]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var book = new Book
                            {
                                BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                AuthorId = reader.GetInt32(reader.GetOrdinal("AuthorId"))
                            };

                            books.Add(book);
                        }
                    }
                }

                foreach (var book in books)
                {
                    book.Author = await GetAuthorByIdAsync(book.AuthorId);
                }
            }

            return books;
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            Book book = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("[dbo].[GetBookById]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookId", bookId);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            book = new Book
                            {
                                BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                AuthorId = reader.GetInt32(reader.GetOrdinal("AuthorId"))
                            };
                        }
                    }
                }
            }

            if (book != null)
            {
                book.Author = await GetAuthorByIdAsync(book.AuthorId);
            }

            return book;
        }

        public async Task<int> InsertBookAsync(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("[dbo].[InsertBook]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@AuthorId", book.AuthorId);

                    await connection.OpenAsync();
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> UpdateBookAsync(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("[dbo].[UpdateBook]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookId", book.BookId);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@AuthorId", book.AuthorId);

                    await connection.OpenAsync();
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DeleteBookAsync(int bookId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("[dbo].[DeleteBook]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookId", bookId);

                    await connection.OpenAsync();
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task<Author> GetAuthorByIdAsync(int authorId)
        {
            Author author = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("[dbo].[GetAuthorById]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AuthorId", authorId);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            author = new Author
                            {
                                AuthorId = reader.GetInt32(reader.GetOrdinal("AuthorId")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            };
                        }
                    }
                }
            }

            return author;
        }
    }
}
