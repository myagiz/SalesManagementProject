using Core.Repositories.Entity;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IStockDal : IEntityRepository<Stock>
    {
        public string UpdateStockForAddMethod(int? productId, double? exitStockQuantity);
        public string UpdateStockForUpdateMethod(int? productId, double? exitStockQuantity, double? exitLastStockQuantity);
        public string UpdateStockForDeleteMethod(int? productId, double? stockQuantity);
        public Task<double> GetStockQuantityByProductIdAsync(int productId);
        public string AddStockForAddMethod(int? productId, double? addStockQuantity);
        public string AddStockForUpdateMethod(int? productId, double? addStockQuantity,double? addLastStockQuantity);
        public string AddStockForDeleteMethod(int? productId, double? stockQuantity);

    }
}
