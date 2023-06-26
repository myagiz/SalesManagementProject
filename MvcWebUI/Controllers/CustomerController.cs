using Business.Abstract;
using Entities.DTOs;
using Entities.Entity;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            var result = _customerService.GetAllCustomers();
            return View(new AllCustomerDto
            {
                ListCustomers = result.Data

            });
        }

        [HttpPost]
        public IActionResult GetById(int id)
        {
            var result = _customerService.GetCustomerById(id);
            if (result != null)
            {
                return Json(new AllCustomerDto
                {
                    Customer = result.Data
                });
            }
            TempData["Error"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var getData = _customerService.GetCustomerById(id);
            if (getData.Success)
            {
                _customerService.DeleteCustomer(getData.Data);
                return RedirectToAction("Index");
            }
            TempData["Error"] = getData.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Add(AllCustomerDto model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Customer
                {
                    Customertitle = model.Customer.Customertitle,
                    Customernumber = model.Customer.Customernumber,
                };

                var result = _customerService.AddCustomer(entity);
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
        public IActionResult Edit(AllCustomerDto model)
        {
            if (ModelState.IsValid)
            {
                var control = _customerService.GetCustomerById(model.Customer.Id);
                if (control.Success)
                {
                    var entity = new Customer
                    {
                        Id = model.Customer.Id,
                        Customertitle = model.Customer.Customertitle,
                        Customernumber = model.Customer.Customernumber,
                    };


                    var updateMethod = _customerService.UpdateCustomer(entity);
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
