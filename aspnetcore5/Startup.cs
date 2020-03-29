using System.Globalization;
using System.Threading.Tasks;
using aspnetcore5.Resources;
using HYJ.Formation.AspNetCore.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
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
                options.Conventions.AddFolderRouteModelConvention("/", LocalRoute);
                options.Conventions.ConfigureFilter(new RobotFilter());
            });

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
            services.AddSingleton<LocalizationService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddSingleton<IProductsStore, MemoryProductsStore>();
        }

        private void LocalRoute(PageRouteModel model)
        {
            foreach (var selector in model.Selectors)
            {
                selector.AttributeRouteModel.Template = AttributeRouteModel.CombineTemplates(
                    "{culture}",
                    selector.AttributeRouteModel.Template);
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

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                SupportedCultures = new[] {new CultureInfo("en-US"), new CultureInfo("fr-FR")},
                SupportedUICultures = new[] {new CultureInfo("en-US"), new CultureInfo("fr-FR")},
                DefaultRequestCulture = new RequestCulture("fr-FR"),
                RequestCultureProviders = new[] {new RouteDataRequestCultureProvider() }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("", context => { 
                    context.Response.Redirect("/fr-fr");
                    return Task.CompletedTask;
                });

                endpoints.MapRazorPages();
            });
        }
    }
}