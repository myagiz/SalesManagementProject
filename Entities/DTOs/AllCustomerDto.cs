using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class AllCustomerDto
    {
        public Customer Customer { get; set; }
        public List<Customer> ListCustomers { get; set; }
    }
}
