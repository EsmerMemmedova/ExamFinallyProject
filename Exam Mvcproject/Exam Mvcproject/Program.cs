using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.Models;
using Core.RepostoryAbstract;
using Data.DAL;
using Data.RepostoryConcretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Exam_Mvcproject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddIdentity<AppUser,IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric=true;
                opt.Password.RequiredLength = 8;
                opt.User.RequireUniqueEmail=false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); 
            builder.Services.AddScoped<IExploreService,ExploreService>();
            builder.Services.AddScoped<IExploreRepostory,ExploreRepostory>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
           

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}