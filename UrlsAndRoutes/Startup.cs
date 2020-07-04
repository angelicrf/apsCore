using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using UrlsAndRoutes.Infrastructure;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreIdentity:ConnectionString"]));
            services.AddTransient<IPasswordValidator<AppUser>,CustomPasswordValidator>();
            services.AddTransient<IUserValidator<AppUser>,CustomUserValidator>();
            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Users/Login");
            services.AddSingleton<IClaimsTransformation,LocationClaimsProvider>();
            services.AddTransient<IAuthorizationHandler, BlockUsersHandler>();
            services.AddTransient<IAuthorizationHandler, DocumentAuthorizationHandler>();

            services.AddAuthorization(opts => {
                opts.AddPolicy("DCUsers", policy => {
                    policy.RequireRole("Users");
                    policy.RequireClaim(ClaimTypes.StateOrProvince, "DC");
                });
                opts.AddPolicy("NotBob", policy => {
                    policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new BlockUsersRequirement("Bob"));
                });
                opts.AddPolicy("AuthorsAndEditors", policy => {
                    policy.AddRequirements(new DocumentAuthorizationRequirement
                    {
                        AllowAuthors = true,
                        AllowEditors = true
                    });
                });
            });
            services.AddAuthentication().AddGoogle(opts => {
                opts.ClientId = Configuration["Authentication:Google:ClientId"];
                opts.ClientSecret = Configuration["Authentication:Google:ClientSecret"];

                //opts.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v1/certs";

            });
            //services.AddIdentity<AppUser, IdentityRole>()
            //        .AddEntityFrameworkStores<AppIdentityDbContext>()
            //        .AddDefaultTokenProviders();
            services.AddIdentity<AppUser, IdentityRole>(opts => {
                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppIdentityDbContext>()
              .AddDefaultTokenProviders();

            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            //AppIdentityDbContext.CreateAdminAccount(app.ApplicationServices,Configuration).Wait();
        }
    }
}
