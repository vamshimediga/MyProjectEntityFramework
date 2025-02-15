using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ICustomer
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();  // Get all customers
        Task<Customer> GetCustomerByIdAsync(int customerId); // Get a customer by ID
        Task AddCustomerAsync(Customer customer);            // Add a new customer
        Task UpdateCustomerAsync(Customer customer);         // Update customer details
        Task DeleteCustomerAsync(int customerId);           // Delete a customer by ID
    }

}
