using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.DTOs;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

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
            var result = _categoryService.GetAllCategories();
            return View(new AllCategoryDto
            {
                ListCategories = result.Data

            });
        }

        public IActionResult GetAllCategories()
        {
            var result = _categoryService.GetAllCategories();
            return PartialView(new AllCategoryDto
            {
                ListCategories = result.Data

            });
        }

        [HttpPost]
        public IActionResult GetById(int id)
        {
            var result = _categoryService.GetCategoryById(id);
            if (result != null)
            {
                return Json(new AllCategoryDto
                {
                    Category = result.Data
                });
            }
            TempData["Error"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var getData = _categoryService.GetCategoryById(id);
            if (getData.Success)
            {
                _categoryService.DeleteCategory(getData.Data);
                return RedirectToAction("Index");
            }
            TempData["Error"] = getData.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Add(AllCategoryDto model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category
                {
                    Name = model.Category.Name
                };

                var result = _categoryService.AddCategory(entity);
                if (result.Success)
                {
                    return RedirectToAction("Index");
                }
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(AllCategoryDto model)
        {
            if (ModelState.IsValid)
            {
                var control = _categoryService.GetCategoryById(model.Category.Id);
                if (control.Success)
                {
                    var entity = new Category
                    {
                        Id = model.Category.Id,
                        Name = model.Category.Name
                    };

                    var updateMethod = _categoryService.UpdateCategory(entity);
                    if (updateMethod.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    TempData["Error"] = control.Message;
                    return RedirectToAction("Index");
                }

                TempData["Error"] = control.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
