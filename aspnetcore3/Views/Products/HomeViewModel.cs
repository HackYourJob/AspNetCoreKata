using System.Collections.Generic;
using HYJ.Formation.AspNetCore.DataAccess;

namespace HYJ.Formation.AspNetCore.Views.Products
{
    public class HomeViewModel
    {
        public HomeViewModel(IList<Product> products)
        {
            Products = products;
        }
        
        public HomeViewModel(IList<Product> products, Quality? selectedQuality)
        {
            SelectedQuality = selectedQuality;
            Products = products;
        }

        public Quality? SelectedQuality { get; }

        public IList<Product> Products { get; }
    }
}