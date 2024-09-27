using app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scheapp.app.DataServices.Interfaces;
using System.Diagnostics;

namespace app.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactsDataService _contactsDataService;
        private readonly IBusinessDataService _businessesDataService;
        public HomeController(ILogger<HomeController> logger
            ,IContactsDataService contactsDataService
            , IBusinessDataService businessesDataService
            )
        {
            _logger = logger;
            _contactsDataService = contactsDataService;
            _businessesDataService = businessesDataService;
        }

        public async Task<IActionResult> Index()
        {
            ///api tests
            var contacts = await _contactsDataService.GetContactTypes();
            var businesses = await _businessesDataService.GetBusinesses();
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
