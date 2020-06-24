using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace GrpcHost.Services
{
    public class ProductsService : Products.ProductsBase
    {
        private static readonly Product[] Products = {
            new Product { Id = 1, Name = "Simple" }, 
            new Product { Id = 2, Name = "Double" }, 
            new Product { Id = 3, Name = "Triple" }, 
        };

        private readonly ILogger<ProductsService> _logger;
        public ProductsService(ILogger<ProductsService> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public override Task<SearchResult> GetAll(GetAllRequest request, ServerCallContext context)
        {
            var response = new SearchResult();
            response.Products.AddRange(Products.Where(p => request.Quality == GetAllRequest.Types.Quality.All || request.Quality.ToString().Equals(p.Name)));
            return Task.FromResult(response);
        }
    }
}
