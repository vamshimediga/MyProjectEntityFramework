using Entities;
using Microsoft.EntityFrameworkCore;
using MyProjectEntity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        // Configuring relationships in OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure decimal precision for TotalAmount
            modelBuilder.Entity<Course>().ToTable(nameof(Course));
            modelBuilder.Entity<Customer>().ToTable(nameof(Customer));
            modelBuilder.Entity<Order>().ToTable(nameof(Order));
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18, 2)"); // Precision of 18 digits, scale of 2 digits after the decimal point

            // Configure one-to-many relationship between Order and Customer
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer) // Order has one Customer
                .WithMany(c => c.Orders) // Customer has many Orders
                .HasForeignKey(o => o.CustomerId) // Foreign Key in Order
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete and update
        }
    }
}
