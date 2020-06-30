using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UrlsAndRoutes.Models;
using static UrlsAndRoutes.Models.Repository;

namespace UrlsAndRoutes
{
    public class Startup
    {
        private IHostingEnvironment env;
        public Startup(IHostingEnvironment hostEnv) => env = hostEnv;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository, MemoryRepository>();
            services.AddMvc();

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.Map("/mvcapp", appBuilder =>
            //{
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles();
                app.UseMvc(routes => {
                    routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                });
            //});
        }
    }
}
