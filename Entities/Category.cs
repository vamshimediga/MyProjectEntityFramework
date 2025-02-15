using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Category
    {
      //  [Key]  // Primary Key
        public int CategoryId { get; set; }

        //[Required]
        //[MaxLength(100)]
        public string CategoryName { get; set; }

        //[MaxLength(500)]
        public string Description { get; set; }

        // Navigation property for related Products
        public ICollection<Product> Products { get; set; }
    }
}
