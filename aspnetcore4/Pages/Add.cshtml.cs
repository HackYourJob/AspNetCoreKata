using System.ComponentModel.DataAnnotations;
using HYJ.Formation.AspNetCore.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HYJ.Formation.AspNetCore.Pages
{
    public class AddModel : PageModel
    {
        private readonly IProductsStore _productsStore;

        public AddModel(IProductsStore productsStore)
        {
            _productsStore = productsStore;
        }

        [Required]
        [BindProperty]
        public int Id { get; set; }

        [Required]
        [BindProperty]
        public string Brand { get; set; } = "";

        [Required]
        [BindProperty]
        public Quality Quality { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
        
            _productsStore.Add(new Product(Id, Brand, Quality));

            return RedirectToPage(nameof(ProductsModel), new {id = Id});
        }
    }
}