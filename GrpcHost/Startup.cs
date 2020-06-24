using System;
using System.IO;
using System.Threading.Tasks;
using GrpcHost.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GrpcHost
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddAuthentication();
            services.AddGrpc();
            services.AddCors(o => o.AddPolicy("gRpc", builder =>
            {
                builder
                    .WithOrigins("https://localhost:5001")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
            }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
            // fix to waiting .net 5 with IIS
            app.Use((c, next) =>
            {
                if (c.Request.ContentType == "application/grpc")
                {
                    var current = c.Features.Get<IHttpResponseFeature>();
                    c.Features.Set<IHttpResponseFeature>(new HttpSysWorkaroundHttpResponseFeature(current));
                }

                return next();
            });

            app.UseCors();

            app.UseMiddleware<ApiSecureMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ProductsService>().RequireCors("gRpc");
            });
        }
    }

    public class HttpSysWorkaroundHttpResponseFeature : IHttpResponseFeature
    {
        private readonly IHttpResponseFeature _inner;

        public HttpSysWorkaroundHttpResponseFeature(IHttpResponseFeature inner)
        {
            _inner = inner;
            _inner.OnStarting(o =>
            {
                HasStarted = true;
                return Task.CompletedTask;
            }, null);
        }

        public void OnCompleted(Func<object, Task> callback, object state)
        {
            _inner.OnCompleted(callback, state);
        }

        public void OnStarting(Func<object, Task> callback, object state)
        {
            _inner.OnStarting(callback, state);
        }

        [Obsolete]
        public Stream Body
        {
            get => _inner.Body;
            set => _inner.Body = value;
        }

        public bool HasStarted { get; private set; }

        public IHeaderDictionary Headers
        {
            get => _inner.Headers;
            set => _inner.Headers = value;
        }
        
        public string ReasonPhrase
        {
            get => _inner.ReasonPhrase;
            set => _inner.ReasonPhrase = value;
        }

        public int StatusCode
        {
            get => _inner.StatusCode;
            set => _inner.StatusCode = value;
        }
    }
}
