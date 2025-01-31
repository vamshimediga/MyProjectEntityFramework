using BusinessLayer.implemation;
using BusinessLayer.Implementation;
using Data;
using Interfaces.@interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection DataServicesLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

           

            // Register Repositories
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICustomer, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IInstitute, InstituteRepository>();
            services.AddScoped<IStudent, StudentRepository>();
            services.AddScoped<IAuthors, AuthorRepository>();
            services.AddScoped<IBook, BookRepositoryADODOTNET>();
            services.AddScoped<IDepartment, DepartmentRepository>();
            services.AddScoped<IEmployees, EmployeeRepository>();
            services.AddScoped<ILead, LeadRepository>();
            services.AddScoped<IContact, ContactRepository>();
            services.AddScoped<IUsers, UsersRepository>();
            return services;
        }
    }
}
