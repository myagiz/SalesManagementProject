using Core.Utilities.Results.Abstract;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IResult AddCustomer(Customer entity);
        IResult UpdateCustomer(Customer entity);
        IResult DeleteCustomer(Customer entity);
        IDataResult<List<Customer>> GetAllCustomers();
        IDataResult<Customer> GetCustomerById(int id);
    }
}
