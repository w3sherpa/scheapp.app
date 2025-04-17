using app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using scheapp.app.Controllers.Data;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.View;
using System.Diagnostics;

namespace scheapp.app.Controllers.View
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProfessionalDataService _professionalsDataService;
        public HomeController(ILogger<HomeController> logger
            , IProfessionalDataService professionalsDataService
            )
        {
            _logger = logger;
            _professionalsDataService = professionalsDataService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TestEnvDocker = StaticClass.TestEnvDocker;
            return View();
        }

        public IActionResult CssExamples()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
