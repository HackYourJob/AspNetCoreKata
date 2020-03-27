using System.Collections.Generic;

namespace HYJ.Formation.AspNetCore.DataAccess
{
    public class MemoryProductsStore : IProductsStore
    {
        private readonly List<int> _ids = new List<int>(); 
        
        public void Add(int id)
        {
            _ids.Add(id);
        }

        public IEnumerable<int> GetAll()
        {
            return _ids;
        }
    }
}