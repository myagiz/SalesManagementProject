using Core.Utilities.Results.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPurchaseService
    {
        IResult AddPurchase(CreatePurchaseDto model);
        IResult UpdatePurchase(UpdatePurchaseDto model);
        IResult DeletePurchase(int id);
        Task<IDataResult<List<GetAllPurchaseDto>>> GetAllPurchasesAsync();
        Task<IDataResult<GetAllPurchaseDto>> GetPurchaseByIdAsync(int id);
    }
}
