using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class AllPurchaseDto
    {
        public CreatePurchaseDto CreatePurchase { get; set; }
        public UpdatePurchaseDto UpdatePurchase { get; set; }
        public List<GetAllPurchaseDto> ListPurchases { get; set; }
        public GetAllPurchaseDto Purchase { get; set; }
    }
}
