using ConfriguringApps.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ConfriguringApps
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services.AddMvc()
                .AddMvcOptions(options =>
                 {
                     options.RespectBrowserAcceptHeader = true;
                 });
        }
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services.AddMvc();             
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if ((Configuration.GetSection("ShortCircuitMiddleware")?
                  .GetValue<bool>("EnableBrowserShortCircuit")).Value)
            {
                app.UseMiddleware<ErrorMiddleware>();
                app.UseMiddleware<BrowserTypeMiddleware>();
                app.UseMiddleware<ShortCircuitMiddleware>();
                app.UseMiddleware<ContentMiddleware>();
            }
            app.UseExceptionHandler("/Home/Error");
            app.UseStaticFiles();

                app.UseMvc(routes => {
                    routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                });
        }
        public void ConfigureDevelopment(IApplicationBuilder app,IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseBrowserLink();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
