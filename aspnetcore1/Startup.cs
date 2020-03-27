using System.Linq;
using System.Text.Json;
using HYJ.Formation.AspNetCore.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HYJ.Formation.AspNetCore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MiddlewareConstantOptions>(_configuration.GetSection("middlewareConstant"));
            services.AddSingleton<IProductsStore, MemoryProductsStore>();
            services.AddSingleton<IApiKeyStore, MemoryApiKeyStore>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IProductsStore productsStore)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();
            
            app.Map("/api", apiApp =>
            {
                apiApp.UseMiddleware<ApiSecureMiddleware>();
                
                apiApp.UseRouting();

                apiApp.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/products", async context =>
                    {
                        context.Response.StatusCode = 200;
                        await context.Response.WriteAsync(JsonSerializer.Serialize(productsStore.GetAll().ToArray()));
                    });
                    endpoints.MapPost("/products/{id:int}", async context =>
                    {
                        productsStore.Add(int.Parse((string) context.Request.RouteValues["id"]));

                        context.Response.StatusCode = 201;
                        await context.Response.WriteAsync("OK");
                    });
                });
            });
            
            app.UseMiddleware<MiddlewareConstant>();
        }
    }
}