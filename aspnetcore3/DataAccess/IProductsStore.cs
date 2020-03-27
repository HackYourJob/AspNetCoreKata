using System.Collections.Generic;

namespace HYJ.Formation.AspNetCore.DataAccess
{
    public interface IProductsStore
    {
        void Add(Product id);
        Product? TryToGet(int id);
        IEnumerable<Product> GetAll();
    }
}