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
    public interface IPurchaseDal : IEntityRepository<Purchase>
    {
        public void AddPurchase(CreatePurchaseDto model);
        public void UpdatePurchase(UpdatePurchaseDto model);
        public void DeletePurchase(int id);
        public Task<List<GetAllPurchaseDto>> GetAllPurchasesAsync();
        public Task<GetAllPurchaseDto> GetPurchaseByIdAsync(int id);
    }
}
