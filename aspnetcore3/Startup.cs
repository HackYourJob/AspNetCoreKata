using System;
using System.Threading;
using System.Threading.Tasks;
using HYJ.Formation.AspNetCore.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HYJ.Formation.AspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add(new RobotFilter()));

            services.AddSingleton<IProductsStore, MemoryProductsStore>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "home",
                    pattern: "/",
                    new { controller = "Products", action = "Index"});
                endpoints.MapControllerRoute(
                    name: "product",
                    pattern: "/products/new",
                    new { controller = "Products", action = "Add"});
                endpoints.MapControllerRoute(
                    name: "product",
                    pattern: "/products/{id}",
                    new { controller = "Products", action = "Product"});
            });
        }
    }
}