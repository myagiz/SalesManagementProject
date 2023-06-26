using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using Core.Utilities.Rules;
using DataAccess.Abstract;
using Entities.DTOs;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task<IResult> AddProduct(CreateProductDto model)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(model.Name));
            if (result == null)
            {
                await _productDal.AddProduct(model);
                return new SuccessResult(Messages.AddMethodSuccessfully);
            }

            return result;
        }

        public IResult DeleteProduct(int id)
        {
            var controlCategory = _productDal.Get(x => x.Id == id);
            if (controlCategory != null)
            {
                _productDal.DeleteProduct(id);
                return new SuccessResult(Messages.DeleteMethodSuccessfully);
            }
            return new ErrorResult(Messages.NotFoundId);
        }

        public IDataResult<List<GetAllProductDto>> GetAllProducts()
        {
            var result = _productDal.GetAllProducts();
            return new SuccessDataResult<List<GetAllProductDto>>(result, Messages.SuccessListed);
        }

        public IDataResult<GetAllProductDto> GetProductById(int id)
        {
            var controlCategory = _productDal.Get(x => x.Id == id);
            if (controlCategory != null)
            {
                var result = _productDal.GetProductById(id);
                return new SuccessDataResult<GetAllProductDto>(result, Messages.SuccessGetById);
            }
            return new ErrorDataResult<GetAllProductDto>(Messages.NotFoundId);
        }

        public async Task<IResult> UpdateProduct(UpdateProductDto model)
        {
            var controlCategory = _productDal.Get(x => x.Id == model.Id);
            if (controlCategory != null)
            {
                await _productDal.UpdateProduct(model);
                return new SuccessResult(Messages.UpdateMethodSuccessfully);
            }
            return new ErrorResult(Messages.NotFoundId);
        }

        private IResult CheckIfProductNameExists(string name)
        {
            var result = _productDal.GetAll(x => x.Name == name).Any();
            if (result)
            {
                return new ErrorResult(Messages.DataAlreadyExist);
            }
            return new SuccessResult();
        }
    }
}
