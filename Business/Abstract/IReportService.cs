using Core.Utilities.Results.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IReportService
    {
        Task<IDataResult<List<ReportGetAllStockDetailsDto>>> GetAllStockDetailsAsync();
        Task<IDataResult<List<ReportGetAllSalesDetailsDto>>> GetAllSalesDetailsAsync();
    }
}
