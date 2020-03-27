using System.Collections.Generic;
using System.Linq;

namespace HYJ.Formation.AspNetCore.DataAccess
{
    public class MemoryProductsStore : IProductsStore
    {
        private readonly List<Product> _products = new List<Product>(); 
        
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