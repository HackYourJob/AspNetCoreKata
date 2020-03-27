using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using HYJ.Formation.AspNetCore.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HYJ.Formation.AspNetCore.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsStore _productsStore;

        public ProductsController(IProductsStore productsStore)
        {
            _productsStore = productsStore;
        }

        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _productsStore.GetAll();
        }
        
        [HttpGet("quality/{quality}")]
        [SwaggerOperation(Summary = "Filter by quality")]
        public IEnumerable<Product> GetAll(Quality quality)
        {
            return _productsStore.GetAll().Where(p => p.Quality == quality);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var maybeProduct = _productsStore.TryToGet(id);
            return maybeProduct == null ? (IActionResult) NotFound() : Ok(maybeProduct);
        }

        [HttpPost("{id}")]
        [SwaggerOperation(
            Summary = "Add product",
            Description = "Return productId"
        )]
        [ProducesResponseType(201)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Create(int id, [FromBody] CreationProductDto product)
        {
            _productsStore.Add(product.ToProduct(id));

            return CreatedAtAction(nameof(Get), new{ id }, id);
        }
    }
}