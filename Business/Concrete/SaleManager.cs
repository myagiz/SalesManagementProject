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
    public class SaleManager : ISaleService
    {
        private readonly ISalesDal _salesDal;

        private readonly IStockDal _stockDal;

        public SaleManager(ISalesDal salesDal, IStockDal stockDal)
        {
            _salesDal = salesDal;
            _stockDal = stockDal;
        }

        public IResult AddSale(CreateSaleDto model)
        {
            if (model.ProductId != null)
            {
                var getStockControl = _stockDal.UpdateStockForAddMethod(model.ProductId, model.Quantity);
                if (getStockControl.Equals("success"))
                {
                    _salesDal.AddSale(model);
                    return new SuccessResult(Messages.AddMethodSuccessfully);
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

        public IResult DeleteSale(int id)
        {
            var controlData = _salesDal.GetSaleByIdAsync(id).Result;
            if (controlData != null)
            {
                if (controlData.ProductId != null)
                {
                    var getStockControl = _stockDal.UpdateStockForDeleteMethod(controlData.ProductId, controlData.Quantity);
                    if (getStockControl.Equals("success"))
                    {
                        _salesDal.DeleteSale(id);
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

        public async Task<IDataResult<List<GetAllSaleDto>>> GetAllSalesAsync()
        {
            var result = await _salesDal.GetAllSalesAsync();
            return new SuccessDataResult<List<GetAllSaleDto>>(result, Messages.SuccessListed);
        }

        public async Task<IDataResult<GetAllSaleDto>> GetSalesByIdAsync(int id)
        {
            var result = await _salesDal.GetSaleByIdAsync(id);
            if (result != null)
            {
                return new SuccessDataResult<GetAllSaleDto>(result, Messages.SuccessListed);
            }
            return new ErrorDataResult<GetAllSaleDto>(Messages.NotFoundId);
        }

        public IResult UpdateSale(UpdateSaleDto model)
        {
            var controlData = _salesDal.GetSaleByIdAsync(model.Id).Result;
            if (controlData != null)
            {
                if (model.ProductId != null)
                {
                    var getStockControl = _stockDal.UpdateStockForUpdateMethod(model.ProductId, model.Quantity, controlData.Quantity);
                    if (getStockControl.Equals("success"))
                    {
                        _salesDal.UpdateSale(model);
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
