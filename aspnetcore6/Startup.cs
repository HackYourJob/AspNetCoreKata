using aspnetcore6.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace aspnetcore6
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthentication(User.DefaultAuthenticationType)
                .AddCookie(User.DefaultAuthenticationType, options =>
                {
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.LoginPath = "/Account/Login";
                });
            services.AddAuthorization(options =>
                options.AddPolicy("PrivatePages", policy => policy.RequireClaim(nameof(Permissions), nameof(Permissions.CanViewPrivatePage))));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
