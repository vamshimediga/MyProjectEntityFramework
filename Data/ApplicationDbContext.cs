﻿using Entities;
using Microsoft.EntityFrameworkCore;
using MyProjectEntity.Entities;
using System;
using System.Collections.Generic;
using System.Data;
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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Lead> Leads { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<UserDomainModel> users { get; set; }

        public DbSet<PostDomainModel> posts { get; set; }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Passport> Passports { get; set; }

        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<Client> Clients { get; set; }


        public DbSet<CoalMine> CoalMines { get; set; }
        public DbSet<CoalProduction> CoalProductions { get; set; }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Resident> Residents { get; set; }

        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Activity> Activities { get; set; }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<LoginLog> LoginLogs { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<AgentDomainModel> Agents { get; set; }
        public DbSet<LeadAgentDomainModel> LeadAgents { get; set; }
        public DbSet<DeveloperDomainModel> Developers { get; set; }
        public DbSet<SystemAdminDomainModel> SystemAdmins { get; set; }

        public DbSet<TeamLead> TeamLeads { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }

        public DbSet<CompanyTypeDomainModel> CompanyTypes { get; set; }
        public DbSet<OpptypeDomainModel> Opptypes { get; set; }
        // Configuring relationships in OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure decimal precision for TotalAmount
            modelBuilder.Entity<Course>().ToTable(nameof(Course));
            modelBuilder.Entity<Customer>().ToTable(nameof(Customer));
            modelBuilder.Entity<Order>().ToTable(nameof(Order));
            modelBuilder.Entity<Category>().ToTable(nameof(Categories));
            modelBuilder.Entity<Product>().ToTable(nameof(Products));
            modelBuilder.Entity<Institute>().ToTable(nameof(Institutes));
            modelBuilder.Entity<Student>().ToTable(nameof(Students));
            modelBuilder.Entity<Book>().ToTable(nameof(Books));
            modelBuilder.Entity<Author>().ToTable(nameof(Authors));
            modelBuilder.Entity<UserDomainModel>().ToTable(nameof(users));
            modelBuilder.Entity<PostDomainModel>().ToTable(nameof(posts));
            modelBuilder.Entity<Person>().ToTable(nameof(Persons));
            modelBuilder.Entity<Passport>().ToTable(nameof(Passports));

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18, 2)"); // Precision of 18 digits, scale of 2 digits after the decimal point
                                                  // Add check constraint for TotalAmount (length before decimal point <= 3)
            modelBuilder.Entity<Order>().HasCheckConstraint("CK_Order_TotalAmount_Length", "TotalAmount < 1000");

            // Configure unique constraint for Email in Customer
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique()
                .HasDatabaseName("IX_Customer_Email_Unique");
            // Configure one-to-many relationship between Order and Customer
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer) // Order has one Customer
                .WithMany(c => c.Orders) // Customer has many Orders
                .HasForeignKey(o => o.CustomerId) // Foreign Key in Order
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete and update

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Product>()
          .HasOne(p => p.Category)
          .WithMany(c => c.Products)
          .HasForeignKey(p => p.CategoryId)
          .OnDelete(DeleteBehavior.Cascade);  // Enable Cascade Delete

            modelBuilder.Entity<Institute>()
           .HasMany(i => i.Students)
           .WithOne(s => s.Institute)
           .HasForeignKey(s => s.InstituteId)
           .OnDelete(DeleteBehavior.Cascade);  // Enable cascade delete

            modelBuilder.Entity<Book>()
           .HasOne(b => b.Author)
           .WithMany(a => a.Books)
           .HasForeignKey(b => b.AuthorId)
           .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            modelBuilder.Entity<Employee>()
           .HasOne(e => e.Department)
           .WithMany(d => d.Employees)
           .HasForeignKey(e => e.DepartmentID)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contact>()
               .HasOne(c => c.Lead)              // A Contact has one Lead
               .WithMany(l => l.Contacts)        // A Lead has many Contacts
               .HasForeignKey(c => c.LeadID)     // Foreign Key in Contact
               .OnDelete(DeleteBehavior.Cascade); // Cascade Delete


            modelBuilder.Entity<PostDomainModel>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // When a User is deleted, delete all their posts.

            modelBuilder.Entity<UserDomainModel>()
                .ToTable("Users")  // Map to SQL table
                .HasKey(u => u.UserId); // Primary Key

            modelBuilder.Entity<PostDomainModel>()
                .ToTable("Posts")
                .HasKey(p => p.PostId);

            modelBuilder.Entity<Person>()
                       .HasMany(p => p.Passports)         // A Person has many Passports
                       .WithOne(p => p.Person)            // A Passport has one Person
                       .HasForeignKey(p => p.PersonId)    // The foreign key is on Passport
                       .OnDelete(DeleteBehavior.Cascade); // Set Cascade Delete

            modelBuilder.Entity<Client>()
           .HasOne(c => c.Lawyer)
           .WithMany(l => l.Clients)
           .HasForeignKey(c => c.LawyerID)
           .OnDelete(DeleteBehavior.Cascade);  // ON DELETE CASCADE


            modelBuilder.Entity<CoalProduction>()
           .HasOne(cp => cp.CoalMine)         // CoalProduction has one related CoalMine
           .WithMany(cm => cm.CoalProductions) // CoalMine can have multiple CoalProductions
           .HasForeignKey(cp => cp.MineID)    // Foreign key in CoalProduction refers to MineID in CoalMine
           .OnDelete(DeleteBehavior.Cascade); // When a CoalMine is deleted, its related CoalProductions are also deleted


            modelBuilder.Entity<Resident>()
            .HasOne(r => r.Apartment)
            .WithMany(a => a.Residents)
            .HasForeignKey(r => r.ApartmentID);


            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Opportunity)
                .WithMany(o => o.Activities)
                .HasForeignKey(a => a.OpportunityID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientID)
            .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<LoginLog>().HasNoKey();
            modelBuilder.Entity<LoginLog>().HasNoKey();

            modelBuilder.Entity<UserProfile>()
           .HasMany(u => u.Photos)
           .WithOne(p => p.UserProfile)
           .HasForeignKey(p => p.UserProfileId);


            // Define Primary Key for AgentDomainModel (AgentID)
            modelBuilder.Entity<AgentDomainModel>()
                .HasKey(a => a.AgentID);

            // Define Primary Key for LeadAgentDomainModel (LeadAgentID)
            modelBuilder.Entity<LeadAgentDomainModel>()
                .HasKey(l => l.LeadAgentID);

            // Setup Foreign Key relationship between LeadAgent and Agent
            modelBuilder.Entity<LeadAgentDomainModel>()
                .HasOne(l => l.Agent)                   // Each LeadAgent has one Agent
                .WithMany(a => a.LeadAgents)            // Each Agent can have many LeadAgents
                .HasForeignKey(l => l.AgentID)          // Foreign key in LeadAgent table
                .OnDelete(DeleteBehavior.Cascade);      // If Agent is deleted, delete related LeadAgents

            modelBuilder.Entity<SystemAdminDomainModel>()
         .HasOne(sa => sa.Developer)
         .WithMany(d => d.SystemAdmins)
         .HasForeignKey(sa => sa.DeveloperId)
         .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeamLead>()
            .HasMany(tl => tl.TeamMembers)
            .WithOne(tm => tm.TeamLead)
            .HasForeignKey(tm => tm.TeamLeadId)
            .OnDelete(DeleteBehavior.Cascade); // Optional: cascade delete


            modelBuilder.Entity<CompanyTypeDomainModel>()
    .HasMany(ct => ct.Opptypes)
    .WithOne(op => op.CompanyType)
    .HasForeignKey(op => op.CompanyTypeID)
    .OnDelete(DeleteBehavior.Cascade); // Optional: cascade delete

            base.OnModelCreating(modelBuilder);
        }

    }
}
