using BusinessLayer;
using BusinessLayer.implemation;
using BusinessLayer.Implementation;
using Data;
using Interfaces;
using Interfaces.@interface;
using Microsoft.EntityFrameworkCore;
using MyProjectEntity;

using Repository;


var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<ApplicationDbContext>(options =>  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
//builder.Services.AddScoped<ICourseRepository, CourseRepository>();
//builder.Services.AddScoped<ICustomer, CustomerRepository>();
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<IInstitute,InstituteRepository>();
//builder.Services.AddScoped<IStudent, StudentRepository>();
//builder.Services.AddScoped<IAuthors, AuthorRepository>();
//builder.Services.AddScoped<IBook, BookRepositoryADODOTNET>();
//builder.Services.AddScoped<IDepartment, DepartmentRepository>();
//builder.Services.AddScoped<IEmployees, EmployeeRepository>();
//builder.Services.AddScoped<ILead,LeadRepository>();
//builder.Services.AddScoped<IContact,ContactRepository>();
//builder.Services.AddScoped<IUsers, UsersRepository>();
builder.Services.DataServicesLayer(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Route for area controllers
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Route for non-area controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
