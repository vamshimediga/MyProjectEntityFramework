using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Ensure we get the correct project root directory
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../MyProjectEntity"));
            Console.WriteLine($"Base Path: {basePath}"); // Debugging

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath) // Set to project root
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
