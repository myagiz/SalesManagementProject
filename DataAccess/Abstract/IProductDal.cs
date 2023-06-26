using Core.Repositories.Entity;
using Entities.DTOs;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDal: IEntityRepository<Product>
    {
        public Task AddProduct(CreateProductDto model);
        public Task UpdateProduct(UpdateProductDto model);
        public void DeleteProduct(int id);
        public List<GetAllProductDto> GetAllProducts();
        public GetAllProductDto GetProductById(int id);
    }
}
