using BusinessLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using MyProject.APIEndpoints;
using MyProjectEntity;
using MyProjectEntity.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IHttpWrapper, HttpWrapper>();


// Register the specific ActivityService
builder.Services.AddScoped(typeof(ServiceLayer<>));
builder.Services.AddSingleton<ApiService>();

// Configure Services
builder.Services.AddHttpClient();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.DataServicesLayer(builder.Configuration);

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Redirect to login page
        options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect unauthorized users
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Middleware Configuration

// Global Exception Handling (Should be before other middleware)
app.UseExceptionHandlingMiddleware();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Serve static files (Before Routing)


// Enable CORS
app.UseCors("AllowAll");

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Enable Routing
app.UseRouting();

// Enable Authentication & Authorization (Order matters)
app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePagesWithRedirects("/Account/AccessDenied");
app.MapControllers(); // ? Enable attribute-based routing
// Map Minimal API Endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapUserEndpoints();
});

// Route for area controllers
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Route for non-area controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


app.Run();
