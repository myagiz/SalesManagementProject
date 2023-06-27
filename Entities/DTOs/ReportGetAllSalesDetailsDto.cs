using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ReportGetAllSalesDetailsDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public double Quantity { get; set; }
        public double TotalQuantity { get; set; }
        public double SalesPrice { get; set; }
        public double DiscountRate { get; set; }
        public DateTime Date { get; set; }
    }
}
