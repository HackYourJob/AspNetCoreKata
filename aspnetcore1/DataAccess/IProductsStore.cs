using System.Collections.Generic;

namespace HYJ.Formation.AspNetCore.DataAccess
{
    public interface IProductsStore
    {
        void Add(int id);
        IEnumerable<int> GetAll();
    }
}