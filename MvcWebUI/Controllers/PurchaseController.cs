using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebUI.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;

        private readonly ICustomerService _customerService;

        private readonly IProductService _productService;

        private readonly IStockService _stockService;

        public PurchaseController(IPurchaseService purchaseService, ICustomerService customerService, IProductService productService, IStockService stockService)
        {
            _purchaseService = purchaseService;
            _customerService = customerService;
            _productService = productService;
            _stockService = stockService;
        }

        public async Task<IActionResult> Index()
        {
            var getCustomerList = _customerService.GetAllCustomers();
            List<SelectListItem> getDropdownCustomerList = (from a in getCustomerList.Data
                                                            select new SelectListItem
                                                            {
                                                                Text = string.Concat(a.Customertitle, " ", a.Customernumber),
                                                                Value = a.Id.ToString()
                                                            }
                                                 ).ToList();
            ViewBag.Customers = getDropdownCustomerList;


            var getProductList = _productService.GetAllProducts();
            List<SelectListItem> getDropdownProductList = (from a in getProductList.Data
                                                           select new SelectListItem
                                                           {
                                                               Text = string.Concat(a.Name, " ", a.Salesprice, " ", "TL"),
                                                               Value = a.Id.ToString()
                                                           }
                                                 ).ToList();
            ViewBag.Products = getDropdownProductList;

            var result = await _purchaseService.GetAllPurchasesAsync();

            return View(new AllPurchaseDto
            {
                ListPurchases = result.Data

            });
        }

        [HttpPost]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _purchaseService.GetPurchaseByIdAsync(id);
            if (result.Success)
            {
                return Json(new AllPurchaseDto
                {
                    Purchase = result.Data
                });
            }
            TempData["Error"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var getData = _purchaseService.GetPurchaseByIdAsync(id).Result;
            if (getData.Success)
            {
                var deleteResult = _purchaseService.DeletePurchase(id);
                if (deleteResult.Success)
                {
                    return RedirectToAction("Index");

                }
                TempData["Error"] = deleteResult.Message;
                return RedirectToAction("Index");
            }
            TempData["Error"] = getData.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Add(AllPurchaseDto model)
        {
            var entity = new CreatePurchaseDto
            {
                ProductId = model.CreatePurchase.ProductId,
                Quantity = model.CreatePurchase.Quantity,
                Price = model.CreatePurchase.Price,
                CustomerId = model.CreatePurchase.CustomerId,
            };

            var result = _purchaseService.AddPurchase(entity);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            TempData["Error"] = result.Message;
            return RedirectToAction("Index");

        }

        public IActionResult Edit(AllPurchaseDto model)
        {
            var control = _purchaseService.GetPurchaseByIdAsync(model.UpdatePurchase.Id).Result;
            if (control.Success)
            {
                var entity = new UpdatePurchaseDto
                {
                    Id = model.UpdatePurchase.Id,
                    ProductId = model.UpdatePurchase.ProductId,
                    Quantity = model.UpdatePurchase.Quantity,
                    Price = model.UpdatePurchase.Price,
                    CustomerId = model.UpdatePurchase.CustomerId,
                };

                var updateMethod = _purchaseService.UpdatePurchase(entity);
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

        public async Task<IActionResult> GetStocks(int productId)
        {
            var result = await _stockService.GetStockQuantityByProductIdAsync(productId);
            if (result.Success)
            {
                return Content(result.Data.ToString());
            }
            return Content("");
        }

    }
}
