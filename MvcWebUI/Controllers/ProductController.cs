using Business.Abstract;
using Entities.DTOs;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var result = _productService.GetAllProducts();

            var getCategoryList = _categoryService.GetAllCategories();
            List<SelectListItem> getDropdownList = (from a in getCategoryList.Data
                                                    select new SelectListItem
                                                    {
                                                        Text = a.Name,
                                                        Value = a.Id.ToString()
                                                    }
                                                 ).ToList();
            ViewBag.Categories = getDropdownList;

            return View(new AllProductDto
            {
                ListProducts = result.Data

            });
        }


        [HttpPost]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetProductById(id);
            if (result != null)
            {
                return Json(new AllProductDto
                {
                    Product = result.Data
                });
            }
            TempData["Error"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var getData = _productService.GetProductById(id);
            if (getData.Success)
            {
                _productService.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            TempData["Error"] = getData.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AllProductDto model)
        {
            var uniqueFileName = "";
            if (model.CreateProduct.ImageFile != null && model.CreateProduct.ImageFile.Length > 0)
            {
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                uniqueFileName = string.Concat(Guid.NewGuid().ToString(), "_", model.CreateProduct.ImageFile.FileName);
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.CreateProduct.ImageFile.CopyTo(stream);
                }
            }

            var entity = new CreateProductDto
            {
                Name = model.CreateProduct.Name,
                CategoryId = model.CreateProduct.CategoryId,
                ImageSrc = uniqueFileName,
                Salesprice = model.CreateProduct.Salesprice,
            };

            var result = await _productService.AddProduct(entity);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            TempData["Error"] = result.Message;
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Edit(AllProductDto model)
        {
            var control = _productService.GetProductById(model.UpdateProduct.Id);
            if (control.Success)
            {
                var uniqueFileName = "";
                if (model.UpdateProduct.ImageFile != null && model.UpdateProduct.ImageFile.Length > 0)
                {
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    uniqueFileName = string.Concat(Guid.NewGuid().ToString(), "_", model.UpdateProduct.ImageFile.FileName);
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.UpdateProduct.ImageFile.CopyTo(stream);
                    }
                }

                var entity = new UpdateProductDto
                {
                    Id = model.UpdateProduct.Id,
                    Name = model.UpdateProduct.Name,
                    CategoryId = model.UpdateProduct.CategoryId,
                    ImageSrc = uniqueFileName == "" ? control.Data.ImageSrc : uniqueFileName,
                    Salesprice = model.UpdateProduct.Salesprice,
                };

                var updateMethod = await _productService.UpdateProduct(entity);
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
    }
}
