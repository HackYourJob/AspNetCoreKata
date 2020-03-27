using System.Collections.Generic;
using System.Linq;
using HYJ.Formation.AspNetCore.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HYJ.Formation.AspNetCore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductsStore _productsStore;

        public IndexModel(IProductsStore productsStore)
        {
            _productsStore = productsStore;
        }

        [BindProperty(SupportsGet = true)]
        public Quality? SelectedQuality { get; set; }

        public IList<Product> Products { get; private set; } = new List<Product>();

        public void OnGet()
        {
            Products =
                _productsStore
                    .GetAll()
                    .Where(p => p.Quality == SelectedQuality || !SelectedQuality.HasValue)
                    .ToArray();
        }
    }
}
