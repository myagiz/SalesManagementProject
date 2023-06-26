using Core.Repositories.Entity;
using Entities.DTOs;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISalesDal: IEntityRepository<Sale>
    {
        public void AddSale(CreateSaleDto model);
        public void UpdateSale(UpdateSaleDto model);
        public void DeleteSale(int id);
        public Task<List<GetAllSaleDto>>  GetAllSalesAsync();
        public Task<GetAllSaleDto> GetSaleByIdAsync(int id);
    }
}
