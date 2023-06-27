using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class GetAllPurchaseDto
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public double? Quantity { get; set; }
        public double? Price { get; set; }
        public double? Amount { get; set; }
        public DateTime? Date { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
