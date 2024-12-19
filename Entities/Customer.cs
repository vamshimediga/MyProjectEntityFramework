using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Customer
    {
        public int CustomerId { get; set; } // Primary Key
        public string CustomerName { get; set; } // Customer's name
        public string Email { get; set; } // Customer's email
        public string PhoneNumber { get; set; } // Customer's phone number

        // Navigation property for related orders
        public ICollection<Order> Orders { get; set; } // One-to-many relationship
    }
}
