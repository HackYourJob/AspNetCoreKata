using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using GrpcHost.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GrpcClient
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
            services.AddControllersWithViews();
            services.AddGrpcClient<Products.ProductsClient>(clientOptions =>
            {
                clientOptions.Address = new Uri("https://localhost:5101");
                var credential = CallCredentials.FromInterceptor((context, metadata) =>
                {
                    metadata.Add("Authorization", "Bearer Toto");
                    return Task.CompletedTask;
                });
                clientOptions.ChannelOptionsActions.Add(channelOptions =>
                {
                    channelOptions.Credentials = ChannelCredentials.Create(new SslCredentials(), credential);
                });
            });
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
