using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class Startup
    {
        private IHostingEnvironment env;
        public Startup(IHostingEnvironment hostEnv) => env = hostEnv;

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<FilterDiagnostics, DefaultFilterDiagnostics>();
            //services.AddSingleton<FilterDiagnostics, DefaultFilterDiagnostics>();
            //services.AddSingleton<TimeFilter>();
            //services.AddMvc();
            services.AddScoped<FilterDiagnostics, DefaultFilterDiagnostics>();
            services.AddScoped<TimeFilter>();
            services.AddScoped<ViewResultDiagnostics>();
            services.AddScoped<DiagnosticsFilter>();
            services.AddMvc().AddMvcOptions(options => {
                //options.Filters.AddService(typeof(ViewResultDiagnostics));
                //options.Filters.AddService(typeof(DiagnosticsFilter));
                options.Filters.Add(new
                DisplayAttribute("This is the Globally-Scoped Filter"));
            });
        
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
