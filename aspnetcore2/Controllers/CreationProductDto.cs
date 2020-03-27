using HYJ.Formation.AspNetCore.DataAccess;

namespace HYJ.Formation.AspNetCore.Controllers
{
    public class CreationProductDto
    {
        public string Brand { get; set; } = "";

        public Quality Quality { get; set; }

        public Product ToProduct(int id)
        {
            return new Product(id, Brand, Quality);
        }
    }
}