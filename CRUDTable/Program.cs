
using BusinessLogicLayer.Danhmuc;
using BusinessLogicLayer.Danhmuctintuc;
using BusinessLogicLayer.Quyen;
using BusinessLogicLayer.Taikhoan;
using BusinessLogicLayer.Tintuc;
using DataAccessLayer.Data;
using DataAccessLayer.Interfaces.Danhmuc;
using DataAccessLayer.Interfaces.Danhmuctintuc;
using DataAccessLayer.Interfaces.Quyen;
using DataAccessLayer.Interfaces.Taikhoan;
using DataAccessLayer.Interfaces.Tintuc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
namespace CRUDTable
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
            //Regis Service
            builder.Services.AddScoped<ITintucRepo, TintucRepo>();
            builder.Services.AddScoped<IDanhmucRepo, DanhmucRepo>();
            builder.Services.AddScoped<IDanhmucTinTucRepo, DanhmucTintucRepo>();
            builder.Services.AddScoped<ITaikhoanRepo, TaikhoanRepo>();
            builder.Services.AddScoped<IQuyenRepo, QuyenRepo>();
            builder.Services.AddScoped<ITintucFilterRepo, TintucFilterRepo>();
 
            builder.Services.AddScoped<ITintucServices, TintucServices>();
            builder.Services.AddScoped<IDanhmucService, DanhmucService>();
            builder.Services.AddScoped<IDanhmuctintucService, DanhmucTintucService>();
            builder.Services.AddScoped<IQuyenService, QuyenService>();
            builder.Services.AddScoped<ITaikhoanService, TaikhoanService>();
            builder.Services.AddScoped<ITintucFilterService, TintucFilterService>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            // Điều này giả sử Itintuc là một interface và TintucServices là lớp triển khai interface đó.
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/TinTucs/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Tintucs}/{action=Index}/{id?}");
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var roleManagerment = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Member", "User" };
                foreach (var itemrole in roles)
                {
                    if (!await roleManagerment.RoleExistsAsync(itemrole))
                    {
                        await roleManagerment.CreateAsync(new IdentityRole(itemrole));
                    }
                }
            }
            using (var scope = app.Services.CreateScope())
            {
                var userManagerment = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string email = "admin@news.com";
                string pass = "Admin123456test@";
                if (await userManagerment.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = email,
                        Email = email,
                    };
                    await userManagerment.CreateAsync(user, pass);
                    await userManagerment.AddToRoleAsync(user, "Admin");
                }
            }
            app.Run();

        }
    }
}

