using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CreateProductDto
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ImageSrc { get; set; }
        public double? Salesprice { get; set; }
    }
}
