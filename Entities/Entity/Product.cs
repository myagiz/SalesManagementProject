using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Entity
{
    public partial class Product
    {
        public Product()
        {
            Purchases = new HashSet<Purchase>();
            Sales = new HashSet<Sale>();
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public double? Salesprice { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
