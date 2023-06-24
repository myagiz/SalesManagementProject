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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IResult AddCategory(Category entity)
        {
            IResult result = BusinessRules.Run(CheckIfCategoryNameExists(entity.Name));
            if (result == null)
            {
                _categoryDal.Add(entity);
                return new SuccessResult(Messages.AddMethodSuccessfully);
            }

            return result;
        }

        public IResult DeleteCategory(Category entity)
        {
            var controlCategory = _categoryDal.Get(x => x.Id == entity.Id);
            if (controlCategory != null)
            {
                _categoryDal.Delete(entity);
                return new SuccessResult(Messages.DeleteMethodSuccessfully);
            }
            return new ErrorResult(Messages.NotFoundId);

        }

        public IDataResult<List<Category>> GetAllCategories()
        {
            var result = _categoryDal.GetAll();
            return new SuccessDataResult<List<Category>>(result,Messages.SuccessListed);
        }

        public IDataResult<Category> GetCategoryById(int id)
        {
            var controlCategory = _categoryDal.Get(x => x.Id == id);
            if (controlCategory != null)
            {
                var result=_categoryDal.GetById(id);
                return new SuccessDataResult<Category>(result,Messages.SuccessGetById);
            }
            return new ErrorDataResult<Category>(Messages.NotFoundId);
        }

        public IResult UpdateCategory(Category entity)
        {
            var controlCategory = _categoryDal.Get(x => x.Id == entity.Id);
            if (controlCategory != null)
            {
                _categoryDal.Update(entity);
                return new SuccessResult(Messages.UpdateMethodSuccessfully);
            }
            return new ErrorResult(Messages.NotFoundId);
        }

        private IResult CheckIfCategoryNameExists(string name)
        {
            var result = _categoryDal.GetAll(x => x.Name == name).Any();
            if (result)
            {
                return new ErrorResult(Messages.DataAlreadyExist);
            }
            return new SuccessResult();
        }
    }
}
