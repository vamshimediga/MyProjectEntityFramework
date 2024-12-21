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
    public class CustomerRepository:ICustomer
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
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC AddCustomer @Name = {customer.CustomerName}, @Email = {customer.Email}, @PhoneNumber = {customer.PhoneNumber}");
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC UpdateCustomer @CustomerId = {customer.CustomerId}, @Name = {customer.CustomerName}, @Email = {customer.Email}, @PhoneNumber = {customer.PhoneNumber}");
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC DeleteCustomer @CustomerId = {customerId}");
        }
    }


}
