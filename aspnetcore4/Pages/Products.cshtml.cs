using HYJ.Formation.AspNetCore.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HYJ.Formation.AspNetCore.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IProductsStore _productsStore;

        public ProductsModel(IProductsStore productsStore)
        {
            _productsStore = productsStore;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Product Product { get; private set; }

        public IActionResult OnGet()
        {
            Product = _productsStore.TryToGet(Id);
            return Product == null ? (IActionResult) NotFound() : Page();
        }
    }
}
