using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; } // Primary Key
        public DateTime OrderDate { get; set; } // Order Date
        public decimal TotalAmount { get; set; } // Total Amount for the order
        public string OrderStatus { get; set; } // Status of the order

        // Foreign Key to Customer
        public int CustomerId { get; set; }
        public CustomerViewModel Customer { get; set; } // Navigation Property

    }
}
