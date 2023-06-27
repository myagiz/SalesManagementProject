using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PurchaseManager : IPurchaseService
    {
        private readonly IPurchaseDal _purchaseDal;

        private readonly IStockDal _stockDal;

        public PurchaseManager(IPurchaseDal purchaseDal, IStockDal stockDal)
        {
            _purchaseDal = purchaseDal;
            _stockDal = stockDal;
        }

        public IResult AddPurchase(CreatePurchaseDto model)
        {
            if (model.ProductId != null)
            {
                var getStockControl = _stockDal.AddStockForAddMethod(model.ProductId, model.Quantity);
                if (getStockControl.Equals("success"))
                {
                    _purchaseDal.AddPurchase(model);
                    return new SuccessResult(Messages.AddMethodSuccessfully);
                }
                else
                {
                    return new ErrorResult(Messages.NotSavedProductStock);

                }
            }

            return new ErrorResult(Messages.GeneralError);
        }

        public IResult DeletePurchase(int id)
        {
            var controlData = _purchaseDal.GetPurchaseByIdAsync(id).Result;
            if (controlData != null)
            {
                if (controlData.ProductId != null)
                {
                    var getStockControl = _stockDal.AddStockForDeleteMethod(controlData.ProductId, controlData.Quantity);
                    if (getStockControl.Equals("success"))
                    {
                        _purchaseDal.DeletePurchase(id);
                        return new SuccessResult(Messages.DeleteMethodSuccessfully);
                    }
                    else if (getStockControl.Equals("noStock"))
                    {
                        return new ErrorResult(Messages.NotEnoughStock);

                    }
                    else
                    {
                        return new ErrorResult(Messages.NotSavedProductStock);

                    }
                }

                return new ErrorResult(Messages.GeneralError);

            }
            return new ErrorResult(Messages.NotFoundId);
        }

        public async Task<IDataResult<List<GetAllPurchaseDto>>> GetAllPurchasesAsync()
        {
            var result = await _purchaseDal.GetAllPurchasesAsync();
            return new SuccessDataResult<List<GetAllPurchaseDto>>(result, Messages.SuccessListed);
        }

        public async Task<IDataResult<GetAllPurchaseDto>> GetPurchaseByIdAsync(int id)
        {
            var result = await _purchaseDal.GetPurchaseByIdAsync(id);
            if (result != null)
            {
                return new SuccessDataResult<GetAllPurchaseDto>(result, Messages.SuccessListed);
            }
            return new ErrorDataResult<GetAllPurchaseDto>(Messages.NotFoundId);
        }

        public IResult UpdatePurchase(UpdatePurchaseDto model)
        {
            var controlData = _purchaseDal.GetPurchaseByIdAsync(model.Id).Result;
            if (controlData != null)
            {
                if (model.ProductId != null)
                {
                    var getStockControl = _stockDal.AddStockForUpdateMethod(model.ProductId, model.Quantity, controlData.Quantity);
                    if (getStockControl.Equals("success"))
                    {
                        _purchaseDal.UpdatePurchase(model);
                        return new SuccessResult(Messages.UpdateMethodSuccessfully);
                    }
                    else if (getStockControl.Equals("noStock"))
                    {
                        return new ErrorResult(Messages.NotEnoughStock);

                    }
                    else
                    {
                        return new ErrorResult(Messages.NotSavedProductStock);

                    }
                }

                return new ErrorResult(Messages.GeneralError);

            }
            return new ErrorResult(Messages.NotFoundId);
        }
    }
}
