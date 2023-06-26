using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CreateSaleDto
    {
        public int? ProductId { get; set; }
        public double? Quantity { get; set; }
        public double? Salesprice { get; set; }
        public int? CustomerId { get; set; }
    }
}
