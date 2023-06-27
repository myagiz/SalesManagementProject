using Core.Repositories.Entity;
using DataAccess.Abstract;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreStockDal : EfCoreEntityRepository<Stock, SalesManagementDBContext>, IStockDal
    {
        public string AddStockForAddMethod(int? productId, double? addStockQuantity)
        {
            using (var context = new SalesManagementDBContext())
            {
                var getProduct = context.Stocks.FirstOrDefault(s => s.ProductId == productId);
                if (getProduct != null)
                {
                    getProduct.Quantity = getProduct.Quantity + addStockQuantity;
                    getProduct.Date = DateTime.Now;
                    context.SaveChanges();
                    return "success";
                }
                else
                {
                    Stock stock = new Stock();
                    stock.ProductId = productId;
                    stock.Quantity = addStockQuantity;
                    stock.Date = DateTime.Now;
                    context.Stocks.Add(stock);
                    context.SaveChanges();
                    return "success";
                }
            }
        }

        public string AddStockForDeleteMethod(int? productId, double? stockQuantity)
        {
            using (var context = new SalesManagementDBContext())
            {
                var getProduct = context.Stocks.FirstOrDefault(s => s.ProductId == productId);
                if (getProduct != null)
                {
                    if (getProduct.Quantity - stockQuantity >= 0)
                    {
                        getProduct.Quantity = getProduct.Quantity - stockQuantity;
                        getProduct.Date = DateTime.Now;
                        context.SaveChanges();
                        return "success";
                    }
                    return "noStock";
                }
                return "noProduct";

            }
        }

        public string AddStockForUpdateMethod(int? productId, double? addStockQuantity, double? addLastStockQuantity)
        {
            using (var context = new SalesManagementDBContext())
            {
                var getProduct = context.Stocks.FirstOrDefault(s => s.ProductId == productId);
                if (getProduct != null)
                {
                    if (getProduct.Quantity - addLastStockQuantity + addStockQuantity >= 0)
                    {
                        getProduct.Quantity = getProduct.Quantity - addLastStockQuantity + addStockQuantity;
                        getProduct.Date = DateTime.Now;
                        context.SaveChanges();
                        return "success";
                    }

                    return "noStock";
                }
                return "noProduct";

            }
        }

        public async Task<double> GetStockQuantityByProductIdAsync(int productId)
        {
            using (var context = new SalesManagementDBContext())
            {
                var control = context.Stocks.FirstOrDefault(x => x.ProductId == productId);
                if (control != null)
                {
                    return (double)control.Quantity;
                }
                return 0;
            }
        }

        public string UpdateStockForAddMethod(int? productId, double? exitStockQuantity)
        {
            using (var context = new SalesManagementDBContext())
            {
                var getProduct = context.Stocks.FirstOrDefault(s => s.ProductId == productId);
                if (getProduct != null)
                {
                    if (getProduct.Quantity - exitStockQuantity >= 0)
                    {
                        getProduct.Quantity = getProduct.Quantity - exitStockQuantity;
                        getProduct.Date = DateTime.Now;
                        context.SaveChanges();
                        return "success";
                    }
                    return "noStock";

                }
                return "noProduct";
            }
        }

        public string UpdateStockForDeleteMethod(int? productId, double? stockQuantity)
        {
            using (var context = new SalesManagementDBContext())
            {
                var getProduct = context.Stocks.FirstOrDefault(s => s.ProductId == productId);
                if (getProduct != null)
                {
                    getProduct.Quantity = getProduct.Quantity + stockQuantity;
                    getProduct.Date = DateTime.Now;
                    context.SaveChanges();
                    return "success";
                }
                return "noProduct";

            }
        }

        public string UpdateStockForUpdateMethod(int? productId, double? exitStockQuantity, double? exitLastStockQuantity)
        {
            using (var context = new SalesManagementDBContext())
            {
                var getProduct = context.Stocks.FirstOrDefault(s => s.ProductId == productId);
                if (getProduct != null)
                {
                    if (getProduct.Quantity + exitLastStockQuantity - exitStockQuantity >= 0)
                    {
                        getProduct.Quantity = getProduct.Quantity + exitLastStockQuantity - exitStockQuantity;
                        getProduct.Date = DateTime.Now;
                        context.SaveChanges();
                        return "success";
                    }

                    return "noStock";
                }
                return "noProduct";

            }
        }
    }
}
