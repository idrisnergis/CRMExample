using CRMExample.Models;
using CRMExample.Services;
using CRMExample.WebApp.Filters;
using CRMExpample.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CRMExpample.WebApp.Controllers
{
    [Auth]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
   
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard([FromServices] IDashboardService dashboardService)//Eklenen
        {
            DashboardModel model = dashboardService.GetDashboardModel();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //Fake Data 
        public string GenFakeData([FromServices] IMockService mockService)
        {
            mockService.RunFakeGenerator();
            return "ok";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
