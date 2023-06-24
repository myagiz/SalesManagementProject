using Core.Utilities.Results.Abstract;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IResult AddCategory(Category entity);
        IResult UpdateCategory(Category entity);
        IResult DeleteCategory(Category entity);
        IDataResult<List<Category>> GetAllCategories();
        IDataResult<Category> GetCategoryById(int id);
    }
}
