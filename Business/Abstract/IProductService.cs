using Core.Utilities.Results.Abstract;
using Entities.DTOs;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<IResult> AddProduct(CreateProductDto model);
        Task<IResult> UpdateProduct(UpdateProductDto model);
        IResult DeleteProduct(int id);
        IDataResult<List<GetAllProductDto>> GetAllProducts();
        IDataResult<GetAllProductDto> GetProductById(int id);
    }
}
