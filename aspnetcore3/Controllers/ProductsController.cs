using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using HYJ.Formation.AspNetCore.DataAccess;
using HYJ.Formation.AspNetCore.Views.Products;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HYJ.Formation.AspNetCore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsStore _productsStore;

        public ProductsController(IProductsStore productsStore)
        {
            _productsStore = productsStore;
        }

        [HttpGet]
        public IActionResult Index(Quality? selectedQuality = null)
        {
            return View(new HomeViewModel(_productsStore.GetAll().Where(p => p.Quality == selectedQuality || !selectedQuality.HasValue).ToArray(), selectedQuality));
        }
        
        [HttpGet]
        public IActionResult Product(int id)
        {
            var maybeProduct = _productsStore.TryToGet(id);
            return maybeProduct == null ? (IActionResult) NotFound() : View(maybeProduct);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("New");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return View("New");
            }
        
            _productsStore.Add(product.ToProduct());

            return RedirectToAction(nameof(Product), new {id = product.Id});
        }
    }
}