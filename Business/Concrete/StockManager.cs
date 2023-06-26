using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StockManager : IStockService
    {
        private readonly IStockDal _stockDal;

        public StockManager(IStockDal stockDal)
        {
            _stockDal = stockDal;
        }

        public async Task<IDataResult<double>> GetStockQuantityByProductIdAsync(int productId)
        {
            var result = await _stockDal.GetStockQuantityByProductIdAsync(productId);
            if (result != 0)
            {
                return new SuccessDataResult<double>(result, Messages.SuccessListed);
            }

            return new ErrorDataResult<double>(Messages.NotFoundId);

        }
    }
}
