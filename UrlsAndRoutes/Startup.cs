using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UrlsAndRoutes.Infrastructure;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //TypeBroker.SetRepositoryType<AlternateRepository>();
            services.AddTransient<IRepository, MemoryRepository>();
            services.AddMvc();
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
