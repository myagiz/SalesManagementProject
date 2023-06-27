using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
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
    public class ReportManager : IReportService
    {
        private readonly IReportDal _reportDal;

        public ReportManager(IReportDal reportDal)
        {
            _reportDal = reportDal;
        }

        public async Task<IDataResult<List<ReportGetAllSalesDetailsDto>>> GetAllSalesDetailsAsync()
        {
            var result = await _reportDal.GetAllSalesDetailsAsync();
            return new SuccessDataResult<List<ReportGetAllSalesDetailsDto>>(result, Messages.SuccessListed);
        }

        public async Task<IDataResult<List<ReportGetAllStockDetailsDto>>> GetAllStockDetailsAsync()
        {
            var result = await _reportDal.GetAllStockDetailsAsync();
            return new SuccessDataResult<List<ReportGetAllStockDetailsDto>>(result, Messages.SuccessListed);
        }
    }
}
