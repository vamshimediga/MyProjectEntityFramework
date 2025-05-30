﻿using BusinessLayer.implemation;
using BusinessLayer.Implementation;
using Data;
using Data.Repositories;
using Entities;
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
            services.AddScoped<IPosts, PostRepository>();
            services.AddScoped<IPersons,PersonsRepository>();
            services.AddScoped<IPassport,PassportRepository>();
            services.AddScoped<ILawyer, LawyerRepository>();
            services.AddScoped<IClient, ClientRepository>();
            services.AddScoped<ICoalProduction, CoalProductionRepository>();
            services.AddScoped<IApartment, ApartmentRepository>();
            services.AddScoped<IResident, ResidentRepository>();
            services.AddScoped<IActivity, ActivityRepository>();
            services.AddScoped<IOpportunity, OpportunityRepository>();
            services.AddScoped<IAppointmentRepository,AppointmentRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<ILoginLogRepository, LoginLogRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IAgentRepository, AgentRepository>();
            services.AddScoped<ILeadAgentRepository, LeadAgentRepository>();
            services.AddScoped<IDeveloperRepository, DeveloperRepository>();
            services.AddScoped<ISystemAdmin, SystemAdminRepository>();
            services.AddScoped<ITeamLeadRepository,TeamLeadAdoRepository>();
            services.AddScoped<ICompanyTypeRepository, CompanyTypeRepository>();
            services.AddScoped<IOpptypeRepository,OpptypeRepository>();
            return services;
        }
    }
}
