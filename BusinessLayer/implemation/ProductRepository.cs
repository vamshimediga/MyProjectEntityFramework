using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.implemation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all products
        public async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products = await _context.Products.FromSqlRaw("EXEC GetAllProducts").ToListAsync(); // First, retrieve the products

            // Load the related Category for each product
            foreach (var product in products)
            {
                // Explicitly load the related Category
                await _context.Entry(product).Reference(p => p.Category).LoadAsync();
            }

            return products ?? throw new ArgumentException("Product not found");
        }

        // Get a product by ID
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var parameters = new[]
            {
            new SqlParameter("@ProductId", productId)
        };

            var products = await _context.Products
                .FromSqlRaw("EXEC GetProductById @ProductId", parameters)
                .ToListAsync();

            return products.FirstOrDefault(); // Returns null if not found
        }

        // Add a new product
        public async Task AddProductAsync(Product product)
        {
            var parameters = new[]
            {
            new SqlParameter("@ProductName", product.ProductName),
            new SqlParameter("@Price", product.Price),
            new SqlParameter("@CategoryId", product.CategoryId)
        };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddProduct @ProductName, @Price, @CategoryId", parameters);
        }

        // Update an existing product
        public async Task UpdateProductAsync(Product product)
        {
            var parameters = new[]
            {
            new SqlParameter("@ProductId", product.ProductId),
            new SqlParameter("@ProductName", product.ProductName),
            new SqlParameter("@Price", product.Price),
            new SqlParameter("@CategoryId", product.CategoryId)
        };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateProduct @ProductId, @ProductName, @Price, @CategoryId", parameters);
        }

        // Delete a product
        public async Task DeleteProductAsync(int productId)
        {
            var parameters = new[]
            {
            new SqlParameter("@ProductId", productId)
        };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteProduct @ProductId", parameters);
        }
    }

}
