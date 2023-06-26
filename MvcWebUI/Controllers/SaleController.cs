using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Core.Utilities.Results.Concrete;

namespace MvcWebUI.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleService _saleService;

        private readonly ICustomerService _customerService;

        private readonly IProductService _productService;

        private readonly IStockService _stockService;

        public SaleController(ISaleService saleService, ICustomerService customerService, IProductService productService, IStockService stockService)
        {
            _saleService = saleService;
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
            //getDropdownCustomerList.Add(new SelectListItem { Text = "Müşteri seçiniz", Value = "", Selected = true ,Disabled=true});
            ViewBag.Customers = getDropdownCustomerList;


            var getProductList = _productService.GetAllProducts();
            List<SelectListItem> getDropdownProductList = (from a in getProductList.Data
                                                           select new SelectListItem
                                                           {
                                                               Text = string.Concat(a.Name, " ", a.Salesprice, " ", "TL"),
                                                               Value = a.Id.ToString()
                                                           }
                                                 ).ToList();
            //getDropdownProductList.Add(new SelectListItem { Text = "Ürün seçiniz", Value = "", Selected = true, Disabled = true });
            ViewBag.Products = getDropdownProductList;

            var result = await _saleService.GetAllSalesAsync();

            return View(new AllSaleDto
            {
                ListSales = result.Data

            });

        }

        [HttpPost]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _saleService.GetSalesByIdAsync(id);
            if (result != null)
            {
                return Json(new AllSaleDto
                {
                    Sale = result.Data
                });
            }
            TempData["Error"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var getData = _saleService.GetSalesByIdAsync(id).Result;
            if (getData.Success)
            {
                var deleteResult = _saleService.DeleteSale(id);
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
        public IActionResult Add(AllSaleDto model)
        {

            var entity = new CreateSaleDto
            {
                ProductId = model.CreateSale.ProductId,
                Quantity = model.CreateSale.Quantity,
                Salesprice = model.CreateSale.Salesprice,
                CustomerId = model.CreateSale.CustomerId,
            };

            var result = _saleService.AddSale(entity);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            TempData["Error"] = result.Message;
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult Edit(AllSaleDto model)
        {
            var control = _saleService.GetSalesByIdAsync(model.UpdateSale.Id).Result;
            if (control.Success)
            {
                var entity = new UpdateSaleDto
                {
                    Id = model.UpdateSale.Id,
                    ProductId = model.UpdateSale.ProductId,
                    Quantity = model.UpdateSale.Quantity,
                    Salesprice = model.UpdateSale.Salesprice,
                    CustomerId = model.UpdateSale.CustomerId,
                };

                var updateMethod = _saleService.UpdateSale(entity);
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
