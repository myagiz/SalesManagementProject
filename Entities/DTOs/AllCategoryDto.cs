using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class AllCategoryDto
    {
        public Category Category { get; set; }
        public List<Category> ListCategories { get; set; }
    }
}
