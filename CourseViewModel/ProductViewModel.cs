using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class ProductViewModel
    {
        [Key]  // Primary Key
        public int ProductId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        // Foreign Key to Category
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        // Navigation property to related Category
        public CategoryViewModel Category { get; set; }
    }
}
