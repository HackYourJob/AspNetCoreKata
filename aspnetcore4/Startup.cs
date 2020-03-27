using HYJ.Formation.AspNetCore.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HYJ.Formation.AspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Add", "products/add");
                options.Conventions.AddFolderRouteModelConvention("/", ToLowerPath);
                options.Conventions.ConfigureFilter(new RobotFilter());
            });

            services.AddSingleton<IProductsStore, MemoryProductsStore>();
        }

        private static void ToLowerPath(PageRouteModel model)
        {
            foreach (var selector in model.Selectors)
            {
                selector.AttributeRouteModel.Template = selector.AttributeRouteModel.Template.ToLower();
            }
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
                endpoints.MapRazorPages();
            });
        }
    }
}