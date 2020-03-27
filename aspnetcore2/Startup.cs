using System.Collections.Generic;
using HYJ.Formation.AspNetCore.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.UseCamelCasing(false);
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.EnableAnnotations();
                
                c.DocumentFilter<PathPrefixInsertDocumentFilter>("api");
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "ApiKey using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();
            
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
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            
            app.Map("/api", apiApp =>
            {
                apiApp.UseMiddleware<ApiSecureMiddleware>();
                
                apiApp.UseRouting();
                
                apiApp.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            });
            
            app.UseMiddleware<MiddlewareConstant>();
        }
    }
}