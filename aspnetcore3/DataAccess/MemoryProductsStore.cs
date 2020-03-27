using System.Collections.Generic;
using System.Linq;

namespace HYJ.Formation.AspNetCore.DataAccess
{
    public class MemoryProductsStore : IProductsStore
    {
        private readonly List<Product> _products = new List<Product>();

        public MemoryProductsStore()
        {
            _products.Add(new Product(1, "Lotus", Quality.Double));
            _products.Add(new Product(2, "Trump", Quality.Simple));
            _products.Add(new Product(3, "Lotus", Quality.Triple));
        }

        public void Add(Product id)
        {
            _products.Add(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product? TryToGet(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }
    }
}