using Core.Repositories.Entity;
using Dapper;
using DataAccess.Abstract;
using DataAccess.Helpers;
using Entities.DTOs;
using Entities.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreSalesDal : EfCoreEntityRepository<Sale, SalesManagementDBContext>, ISalesDal
    {
        private readonly IConfiguration _configuration;

        public EfCoreSalesDal(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void AddSale(CreateSaleDto model)
        {
            using (var context = new SalesManagementDBContext())
            {
                double? listPrice = 0;
                var getListPrice = context.Products.FirstOrDefault(x => x.Id == model.ProductId);
                if (getListPrice != null)
                {
                    listPrice = getListPrice.Salesprice;
                }

                var discountRate = CalculatorDiscountRateHelper.Calculate(listPrice, model.Salesprice);

                Sale sale = new Sale();
                sale.ProductId = model.ProductId;
                sale.CustomerId = model.CustomerId;
                sale.Quantity = model.Quantity;
                sale.Salesprice = model.Salesprice;
                sale.Listprice = listPrice == 0 ? 0 : listPrice;
                sale.Amount = model.Quantity * model.Salesprice;
                sale.Discountrate = discountRate == 0 ? 0 : Math.Round(discountRate);
                sale.Date = DateTime.Now;
                context.Sales.Add(sale);
                context.SaveChanges();
            }
        }

        public void DeleteSale(int id)
        {
            using (var context = new SalesManagementDBContext())
            {
                var control = context.Sales.FirstOrDefault(x => x.Id == id);
                if (control != null)
                {
                    context.Sales.Remove(control);
                    context.SaveChanges();
                }
            }
        }

        public async Task<List<GetAllSaleDto>> GetAllSalesAsync()
        {
            using IDbConnection context = new SqlConnection(_configuration.GetConnectionString("SalesManagementConnection"));
            var result = await Task.FromResult(context.Query<GetAllSaleDto>("dbo.SPGetAllSales", commandType: CommandType.StoredProcedure).ToList());
            return result;
        }

        public async Task<GetAllSaleDto> GetSaleByIdAsync(int id)
        {
            using IDbConnection context = new SqlConnection(_configuration.GetConnectionString("SalesManagementConnection"));
            var result = await Task.FromResult(context.Query<GetAllSaleDto>("dbo.SPGetAllSales", commandType: CommandType.StoredProcedure).ToList());

            if (result.Count > 0)
            {
                var firstData = result.FirstOrDefault(x => x.Id == id);
                if (firstData != null)
                {
                    return firstData;
                }
                return null;

            }
            return null;
        }

        public void UpdateSale(UpdateSaleDto model)
        {
            using (var context = new SalesManagementDBContext())
            {
                var controlData = context.Sales.FirstOrDefault(x => x.Id == model.Id);
                if (controlData != null)
                {
                    double? listPrice = 0;
                    var getListPrice = context.Products.FirstOrDefault(x => x.Id == model.ProductId);
                    if (getListPrice != null)
                    {
                        listPrice = getListPrice.Salesprice;
                    }

                    var discountRate = CalculatorDiscountRateHelper.Calculate(listPrice, model.Salesprice);

                    controlData.ProductId = model.ProductId;
                    controlData.CustomerId = model.CustomerId;
                    controlData.Quantity = model.Quantity;
                    controlData.Salesprice = model.Salesprice;
                    controlData.Listprice = listPrice == 0 ? 0 : listPrice;
                    controlData.Amount = model.Quantity * model.Salesprice;
                    controlData.Discountrate = discountRate == 0 ? 0 : Math.Round(discountRate);
                    controlData.Date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
    }
}
