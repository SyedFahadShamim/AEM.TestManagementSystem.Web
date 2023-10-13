using AEM.TestManagementSystem.Repository.Interfaces;
using AEM.TestManagementSystem.Repository.Models.Domain;
using AEM.TestManagementSystem.Services.Implementation;
using AEM.TestManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AEM.TestManagementSystem.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"), sqlServerOptions => sqlServerOptions.CommandTimeout(120));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

});


// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
//builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/UserAuthentication/Login");

//add services to container
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
