using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace UrlsAndRoutes.Models
{
        public class AppIdentityDbContext : IdentityDbContext<AppUser>
        {
            public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options) { }
        public static Task CreateAdminAccount(IServiceProvider serviceProvider,IConfiguration configuration)
        {
            UserManager<AppUser> userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = configuration["Admin"];
            string email = configuration["admin@example.com"];
            string password = configuration["secret"];
            string role = configuration["Admins"];
            
                //if (userManager.FindByNameAsync(username) == null){
                //if (roleManager.FindByNameAsync(role) == null)
                //{
                    roleManager.CreateAsync(new IdentityRole(role));
               // }
                AppUser user = new AppUser
                {
                    UserName = username,
                    Email = email
                };
                //IdentityResult result = 
                    userManager.CreateAsync(user, password);
               // if (result.Succeeded)
                //{
                   return userManager.AddToRoleAsync(user, role);
                //}
            }
        }
    }
  

