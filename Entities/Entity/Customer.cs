using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Entity
{
    public partial class Customer
    {
        public Customer()
        {
            Purchases = new HashSet<Purchase>();
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public string Customertitle { get; set; }
        public string Customernumber { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
