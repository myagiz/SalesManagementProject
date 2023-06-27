using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ReportGetAllStockDetailsDto
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public double? Quantity { get; set; }
        public DateTime? Date { get; set; }
    }
}
