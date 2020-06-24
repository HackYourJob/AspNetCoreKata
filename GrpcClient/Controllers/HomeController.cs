using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GrpcHost.Services;

namespace GrpcClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Products.ProductsClient _productsClient;

        public HomeController(ILogger<HomeController> logger, Products.ProductsClient productsClient)
        {
            _logger = logger;
            _productsClient = productsClient;
        }

        public async Task<IActionResult> Index()
        {
            var result = 
                await _productsClient.GetAllAsync(
                    new GetAllRequest {Quality = GetAllRequest.Types.Quality.Double});

            return View(result.Products);
        }

        public IActionResult Online()
        {
            return View();
        }
    }
}
