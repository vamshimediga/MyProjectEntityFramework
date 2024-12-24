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
    public class CustomerRepository : ICustomer
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers
                .FromSqlRaw("EXEC GetAllCustomers")
                .ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            var sql = "EXEC GetCustomerById @CustomerId = {0}";
            Customer customers = _context.Customers
                                    .FromSqlRaw(sql, customerId)
                                    .AsEnumerable() // Move the query to the client side
                                    .FirstOrDefault(); // Use synchronous method
            return await Task.FromResult(customers);
        }




        public async Task AddCustomerAsync(Customer customer)
        {
            var parameters = new[]
            {
                         new SqlParameter("@Name", customer.CustomerName),
                        new SqlParameter("@Email", customer.Email),
                         new SqlParameter("@PhoneNumber", customer.PhoneNumber)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddCustomer @Name, @Email, @PhoneNumber", parameters);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var parameters = new[]{
                                  new SqlParameter("@CustomerId", customer.CustomerId),
                                  new SqlParameter("@Name", customer.CustomerName),
                                  new SqlParameter("@Email", customer.Email),
                                  new SqlParameter("@PhoneNumber", customer.PhoneNumber)
             };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateCustomer @CustomerId, @Name, @Email, @PhoneNumber", parameters);
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var parameters = new[]
            {
        new SqlParameter("@CustomerId", customerId)
    };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteCustomer @CustomerId", parameters);
        }
    }


}
