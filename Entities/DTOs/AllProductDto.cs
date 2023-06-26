using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class AllProductDto
    {
        public CreateProductDto CreateProduct { get; set; }
        public UpdateProductDto UpdateProduct { get; set; }
        public List<GetAllProductDto> ListProducts { get; set; }
        public GetAllProductDto Product { get; set; }
    }
}
