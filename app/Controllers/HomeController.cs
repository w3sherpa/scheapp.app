using app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scheapp.app.DataServices.Interfaces;
using System.Diagnostics;
using System.Linq.Expressions;

namespace app.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactsDataService _contactsDataService;
        private readonly IBusinessDataService _businessesDataService;
        private readonly ICustomerDataService _customersDataService;
        private readonly IProfessionalDataService _professionalsDataService;
        private readonly ICommunicationDataService _communicationsDataService;
        private readonly IServiceDataService _servicesDataService;
        public HomeController(ILogger<HomeController> logger
            ,IContactsDataService contactsDataService
            , IBusinessDataService businessesDataService
            , ICustomerDataService customersDataService
            , IProfessionalDataService professionalsDataService
            , ICommunicationDataService communicationsDataService
            , IServiceDataService servicesDataService
            )
        {
            _logger = logger;
            _contactsDataService = contactsDataService;
            _businessesDataService = businessesDataService;
            _customersDataService= customersDataService;
            _professionalsDataService = professionalsDataService;
            _communicationsDataService = communicationsDataService;
            _servicesDataService = servicesDataService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ///api tests
                var contacts = await _contactsDataService.GetContactTypes();
                var businesses = await _businessesDataService.GetBusinesses();
                var customers = await _customersDataService.GetCustomers();
                var professionals = await _professionalsDataService.GetProfessionals();
                var customerCalls = await _communicationsDataService.GetCustomerCalls();
                var professionalCalls = await _communicationsDataService.GetProfessionalCalls();
                var serviceTypes = await _servicesDataService.GetServiceTypes();
                var serviceDurations = await _servicesDataService.GetServiceDurations();
            }
            catch ( Exception ex )
            {
                _logger.LogError("{@Exception}", ex);
            }
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
