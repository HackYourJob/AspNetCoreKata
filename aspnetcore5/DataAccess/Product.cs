using System;

namespace HYJ.Formation.AspNetCore.DataAccess
{
    public class Product
    {
        public Product(int id, string brand, Quality quality)
        {
            Id = id;
            Brand = brand;
            Quality = quality;
            Created = DateTime.Now;
        }
        public int Id { get; }

        public string Brand { get; }

        public Quality Quality { get; }

        public DateTime Created { get; }
    }
}