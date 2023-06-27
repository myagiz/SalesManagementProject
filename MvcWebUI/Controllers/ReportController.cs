using Business.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MvcWebUI.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IActionResult> Index()
        {
            var getAllStockDetailsAsync = await _reportService.GetAllStockDetailsAsync();
            ViewBag.GetAllStockDetailsAsync = getAllStockDetailsAsync.Data;

            var getAllSaleDetailsAsync = await _reportService.GetAllSalesDetailsAsync();
            ViewBag.GetAllSaleDetailsAsync = getAllSaleDetailsAsync.Data;

            return View();
        }


    }
}
