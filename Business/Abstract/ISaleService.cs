using Core.Utilities.Results.Abstract;
using Entities.DTOs;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISaleService
    {
        IResult AddSale(CreateSaleDto model);
        IResult UpdateSale(UpdateSaleDto model);
        IResult DeleteSale(int id);
        Task<IDataResult<List<GetAllSaleDto>>> GetAllSalesAsync();
        Task<IDataResult<GetAllSaleDto>> GetSalesByIdAsync(int id);
    }
}
