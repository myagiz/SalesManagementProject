using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class AllSaleDto
    {
        public CreateSaleDto CreateSale { get; set; }
        public UpdateSaleDto UpdateSale { get; set; }
        public List<GetAllSaleDto> ListSales { get; set; }
        public GetAllSaleDto Sale { get; set; }

    }
}
