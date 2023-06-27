using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IReportDal
    {
        public Task<List<ReportGetAllStockDetailsDto>> GetAllStockDetailsAsync();
        public Task<List<ReportGetAllSalesDetailsDto>> GetAllSalesDetailsAsync();
    }
}
