using Core.Repositories.Entity;
using Dapper;
using DataAccess.Abstract;
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
    public class EfCorePurchaseDal : EfCoreEntityRepository<Purchase, SalesManagementDBContext>, IPurchaseDal
    {
        private readonly IConfiguration _configuration;

        public EfCorePurchaseDal(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void AddPurchase(CreatePurchaseDto model)
        {
            using (var context = new SalesManagementDBContext())
            {
                Purchase purchase = new Purchase();
                purchase.CustomerId = model.CustomerId;
                purchase.ProductId = model.ProductId;
                purchase.Quantity = model.Quantity;
                purchase.Price = model.Price;
                purchase.Amount = model.Quantity * model.Price;
                purchase.Date = DateTime.Now;
                context.Purchases.Add(purchase);
                context.SaveChanges();
            }
        }

        public void DeletePurchase(int id)
        {
            using (var context = new SalesManagementDBContext())
            {
                var control = context.Purchases.FirstOrDefault(x => x.Id == id);
                if (control != null)
                {
                    context.Purchases.Remove(control);
                    context.SaveChanges();
                }
            }
        }

        public async Task<List<GetAllPurchaseDto>> GetAllPurchasesAsync()
        {
            using IDbConnection context = new SqlConnection(_configuration.GetConnectionString("SalesManagementConnection"));
            var result = await Task.FromResult(context.Query<GetAllPurchaseDto>("dbo.SPGetAllPurchases", commandType: CommandType.StoredProcedure).ToList());
            return result;
        }

        public async Task<GetAllPurchaseDto> GetPurchaseByIdAsync(int id)
        {
            using IDbConnection context = new SqlConnection(_configuration.GetConnectionString("SalesManagementConnection"));
            var result = await Task.FromResult(context.Query<GetAllPurchaseDto>("dbo.SPGetAllPurchases", commandType: CommandType.StoredProcedure).ToList());

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

        public void UpdatePurchase(UpdatePurchaseDto model)
        {
            using (var context=new SalesManagementDBContext())
            {
                var controlData=context.Purchases.FirstOrDefault(x=>x.Id==model.Id);
                if (controlData != null) 
                {
                    controlData.ProductId = model.ProductId;
                    controlData.CustomerId = model.CustomerId;
                    controlData.Quantity = model.Quantity;
                    controlData.Price = model.Price;
                    controlData.Amount = model.Quantity * model.Price;
                    controlData.Date = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
    }
}
