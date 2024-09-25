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
        public HomeController(ILogger<HomeController> logger,IContactsDataService contactsDataService)
        {
            _logger = logger;
            _contactsDataService = contactsDataService;
        }

        public async Task<IActionResult> Index()
        {
            var test = await _contactsDataService.GetContactTypes();
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
