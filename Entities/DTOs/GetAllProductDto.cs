﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class GetAllProductDto
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public double? Salesprice { get; set; }
    }
}
