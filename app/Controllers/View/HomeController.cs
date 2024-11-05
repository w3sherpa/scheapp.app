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
        private readonly IHubContext<ScheAppViewUpdateHub> _signalRScheAppHub;


        //private readonly IContactsDataService _contactsDataService;
        //private readonly IBusinessDataService _businessesDataService;
        //private readonly ICustomerDataService _customersDataService;
        private readonly IProfessionalDataService _professionalsDataService;
        //private readonly ICommunicationDataService _communicationsDataService;
        //private readonly IServiceDataService _servicesDataService;
        public HomeController(ILogger<HomeController> logger
            , IHubContext<ScheAppViewUpdateHub> signalRScheAppHub
            //, IContactsDataService contactsDataService
            //, IBusinessDataService businessesDataService
            //, ICustomerDataService customersDataService
            , IProfessionalDataService professionalsDataService
            //, ICommunicationDataService communicationsDataService
            //, IServiceDataService servicesDataService
            )
        {
            _logger = logger;
            _signalRScheAppHub = signalRScheAppHub;

            //_contactsDataService = contactsDataService;
            //_businessesDataService = businessesDataService;
            //_customersDataService = customersDataService;
            _professionalsDataService = professionalsDataService;
            //_communicationsDataService = communicationsDataService;
            //_servicesDataService = servicesDataService;
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
