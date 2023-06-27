using Dapper;
using DataAccess.Abstract;
using Entities.DTOs;
using Entities.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreReportDal : IReportDal
    {
        private readonly IConfiguration _configuration;

        public EfCoreReportDal(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<ReportGetAllSalesDetailsDto>> GetAllSalesDetailsAsync()
        {
            using IDbConnection context = new SqlConnection(_configuration.GetConnectionString("SalesManagementConnection"));
            //var result = await Task.FromResult(context.Query<ReportGetAllSalesDetailsDto>("dbo.SPReportGetAllSalesDetail", new { startDate = startDate, endDate = endDate }, commandType: CommandType.StoredProcedure).ToList());
            var result = await Task.FromResult(context.Query<ReportGetAllSalesDetailsDto>("dbo.SPReportGetAllSalesDetail", commandType: CommandType.StoredProcedure).ToList());
            return result;
        }

        public async Task<List<ReportGetAllStockDetailsDto>> GetAllStockDetailsAsync()
        {
            using (var context = new SalesManagementDBContext())
            {
                var data = (from a in context.Stocks
                            join b in context.Products on a.ProductId equals b.Id
                            join c in context.Categories on b.CategoryId equals c.Id
                            select new ReportGetAllStockDetailsDto
                            {
                                Id = a.Id,
                                ProductId = a.ProductId,
                                ProductName = b.Name,
                                CategoryId = b.CategoryId,
                                CategoryName = c.Name,
                                Date = a.Date,
                                Quantity = a.Quantity
                            }).ToListAsync();
                return await data;
            }
        }
    }
}
