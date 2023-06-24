using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var result=_categoryService.GetAllCategories();
            return View(result);
        }
    }
}
