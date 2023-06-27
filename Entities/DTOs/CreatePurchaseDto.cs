using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CreatePurchaseDto
    {
        public int? ProductId { get; set; }
        public double? Quantity { get; set; }
        public double? Price { get; set; }
        public int? CustomerId { get; set; }
    }
}
