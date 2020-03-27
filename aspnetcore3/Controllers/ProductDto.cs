using System.ComponentModel.DataAnnotations;
using HYJ.Formation.AspNetCore.DataAccess;

namespace HYJ.Formation.AspNetCore.Controllers
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; } = "";

        [Required]
        public Quality Quality { get; set; }

        public Product ToProduct()
        {
            return new Product(Id, Brand, Quality);
        }
    }
}