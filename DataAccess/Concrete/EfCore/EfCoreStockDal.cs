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
    }
}
