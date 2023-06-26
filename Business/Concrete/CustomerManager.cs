using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using Core.Utilities.Rules;
using DataAccess.Abstract;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult AddCustomer(Customer entity)
        {
            IResult result = BusinessRules.Run(CheckIfCustomerNameAndNumberExists(entity.Customertitle, entity.Customernumber));
            if (result == null)
            {
                _customerDal.Add(entity);
                return new SuccessResult(Messages.AddMethodSuccessfully);
            }

            return result;
        }

        public IResult DeleteCustomer(Customer entity)
        {
            var controlCategory = _customerDal.Get(x => x.Id == entity.Id);
            if (controlCategory != null)
            {
                _customerDal.Delete(entity);
                return new SuccessResult(Messages.DeleteMethodSuccessfully);
            }
            return new ErrorResult(Messages.NotFoundId);
        }

        public IDataResult<List<Customer>> GetAllCustomers()
        {
            var result = _customerDal.GetAll();
            return new SuccessDataResult<List<Customer>>(result, Messages.SuccessListed);
        }

        public IDataResult<Customer> GetCustomerById(int id)
        {
            var controlCategory = _customerDal.Get(x => x.Id == id);
            if (controlCategory != null)
            {
                var result = _customerDal.GetById(id);
                return new SuccessDataResult<Customer>(result, Messages.SuccessGetById);
            }
            return new ErrorDataResult<Customer>(Messages.NotFoundId);
        }

        public IResult UpdateCustomer(Customer entity)
        {
            var controlCategory = _customerDal.Get(x => x.Id == entity.Id);
            if (controlCategory != null)
            {
                _customerDal.Update(entity);
                return new SuccessResult(Messages.UpdateMethodSuccessfully);
            }
            return new ErrorResult(Messages.NotFoundId);
        }

        private IResult CheckIfCustomerNameAndNumberExists(string name, string cNumber)
        {
            var result = _customerDal.GetAll(x => x.Customertitle == name || x.Customernumber == cNumber).Any();
            if (result)
            {
                return new ErrorResult(Messages.DataAlreadyExist);
            }
            return new SuccessResult();
        }
    }
}
