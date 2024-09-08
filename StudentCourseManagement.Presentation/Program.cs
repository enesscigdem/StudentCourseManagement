using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Application.Interfaces;
using StudentCourseManagement.Application.Services;
using StudentCourseManagement.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// MVC View'ları eklemek için
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Identity veya diğer servisler
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//addsocpe ları ekle.
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IRoleService, RoleService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// MVC route yapılandırması
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();