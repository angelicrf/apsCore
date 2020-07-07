using EO.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin.Security.Google;
using System.Security.Claims;
using UrlsAndRoutes.Infrastructure;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes
{
    public class Startup
    {
        public Microsoft.AspNetCore.Http.PathString CallbackPath { get; set; }

        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreIdentity:ConnectionString"]));
            services.AddTransient<IPasswordValidator<AppUser>, CustomPasswordValidator>();
            services.AddTransient<IUserValidator<AppUser>, CustomUserValidator>();
            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Users/Login");
            services.AddSingleton<IClaimsTransformation, LocationClaimsProvider>();
            services.AddTransient<IAuthorizationHandler, BlockUsersHandler>();
            services.AddTransient<IAuthorizationHandler, DocumentAuthorizationHandler>();


            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("DCUsers", policy =>
                {
                    policy.RequireRole("Users");
                    policy.RequireClaim(ClaimTypes.StateOrProvince, "DC");
                });
                opts.AddPolicy("NotBob", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new BlockUsersRequirement("Bob"));
                });
                opts.AddPolicy("AuthorsAndEditors", policy =>
                {
                    policy.AddRequirements(new DocumentAuthorizationRequirement
                    {
                        AllowAuthors = true,
                        AllowEditors = true
                    });
                });
            });
            services.AddAuthentication().AddGoogle("Google", opts =>
             {
                 IConfigurationSection googleAuthNSection =
                 Configuration.GetSection("Authentication:Google");


                //opts.CallbackPath = new PathString($"/{CallbackPath}");
                opts.ClientId = Configuration["Authentication:Google:ClientId"];
                 opts.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                 opts.SignInScheme = IdentityConstants.ExternalScheme;


                 opts.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v1/certs";
                 opts.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                 opts.ClaimActions.Clear();
                 opts.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                 opts.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                 opts.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                 opts.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                 opts.ClaimActions.MapJsonKey("urn:google:profile", "link");
                 opts.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");

             });
            var google = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "95719071149-i7k9i69nj6qv2ffqf335hoc8o9np0di5.apps.googleusercontent.com",
                ClientSecret = "kKDCqK9zv-Yq4g3YGG0q2u3T",
                Provider = new GoogleOAuth2AuthenticationProvider()
            };
            google.Scope.Add("email");
            // app.UseGoogleAuthentication(google);
            //services.AddIdentity<AppUser, IdentityRole>()
            //        .AddEntityFrameworkStores<AppIdentityDbContext>()
            //        .AddDefaultTokenProviders();
            services.AddIdentity<AppUser, IdentityRole>(opts =>
            {
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
            app.UseIdentity();

            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();


            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.Strict
            });


            //AppIdentityDbContext.CreateAdminAccount(app.ApplicationServices,Configuration).Wait();
        }
    }
}
