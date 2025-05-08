using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.Data.DspModels;
using scheapp.app.Models.Data.TableModels.Customers;
using scheapp.app.Models.View;

namespace scheapp.app.Controllers.View
{
    [Authorize(Roles = "business_admin,scheapp_admin")]
    public class BusinessAdminController : Controller
    {
        private readonly ILogger _logger;
        private readonly IProfessionalDataService _professionalDataService;
        private readonly IBusinessDataService _businessDataService;
        private readonly IServiceDataService _servicesDataService;
        private readonly ICustomerDataService _customerDataService;
        private readonly IHubContext<ScheAppViewUpdateHub> _signalRScheAppHub;
        private readonly IMemoryCache _memoryCache;
        public BusinessAdminController(
            ILogger<BusinessAdminController> logger
            , IMemoryCache memoryCache
            , IProfessionalDataService professionalDataService
            , IBusinessDataService businessDataService
            , IServiceDataService servicesDataService
            , ICustomerDataService customerDataService
            , IHubContext<ScheAppViewUpdateHub> signalRScheAppHub)
        {
            _logger = logger;
            _professionalDataService = professionalDataService;
            _businessDataService = businessDataService;
            _servicesDataService = servicesDataService;
            _customerDataService = customerDataService;
            _memoryCache = memoryCache;
            _signalRScheAppHub = signalRScheAppHub;
        }
       
        public async Task<IActionResult> Index(int? businessId)
        {
            try
            {
                var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional == null)
                {
                    return Content("ACCESS DENIED!.");
                }
                else
                {
                    return Redirect($"/BusinessAdmin/Appointments?businessId={verifiedBusinessProfessional.BusinessId.GetValueOrDefault()}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }
        public async Task<IActionResult> Appointments(int? businessId)
        {
            try
            {
                var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional == null)
                {
                    return Content("ACCESS DENIED!.");
                }
                else
                {
                    AppointmentsVM appointmentsVM = new AppointmentsVM();
                    appointmentsVM.BusinessId = businessId.GetValueOrDefault();
                    ViewBag.BusinessProfessional = verifiedBusinessProfessional;
                    return View(appointmentsVM);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }
        public async Task<IActionResult> Professionals(int? businessId)
        {
            try 
            { 
                var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional == null)
                {
                    return Content("ACCESS DENIED!.");
                }
                else
                {
                    List<ProfessionalBusinessDetailDsp> allProfessionalBusinessDetails = await _professionalDataService.GetProfessionalBusinessDetailDsp(null, businessId);

                    ViewBag.BusinessProfessional = verifiedBusinessProfessional;
                    return View();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }
        public async Task<IActionResult> EdidProfessional(int? businessId, int? professinalId)
        {
            try
            {
                var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional == null)
                {
                    return Content("ACCESS DENIED!.");
                }
                else
                {
                    List<ProfessionalBusinessDetailDsp> allProfessionalBusinessDetails = await _professionalDataService.GetProfessionalBusinessDetailDsp(null, businessId);
                    ViewBag.BusinessProfessional = verifiedBusinessProfessional;
                    return View(allProfessionalBusinessDetails);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }
        public async Task<IActionResult> Customers(int businessId)
        {
            try
            {
                var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional == null)
                {
                    return Content("ACCESS DENIED!.");
                }
                ViewBag.BusinessProfessional = verifiedBusinessProfessional;

                List<Customer> customers = await _customerDataService.GetCustomers(businessId);

                return View(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }        
        public async Task<IActionResult> Services(int businessId)
        {
            try
            {
                var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional == null)
                {
                    return Content("ACCESS DENIED!.");
                }
                ViewBag.BusinessProfessional = verifiedBusinessProfessional;
                ServicesVM servicesVm = new ServicesVM();
                servicesVm.Services = await _servicesDataService.GetServices(businessId);
                servicesVm.BusinessId = businessId;
                return View(servicesVm);
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }
        public async Task<IActionResult> ServiceDurations(int businessId)
        {
            try
            {
                var verifiedBusinessProfessional = await GetLoggedInProfessionalBusinessDetails(businessId);
                //if id is still null that mean either user is scheapp admin who passed no id param or business admin who's permission is not set.
                if (verifiedBusinessProfessional == null)
                {
                    return Content("ACCESS DENIED!.");
                }
                ViewBag.BusinessProfessional = verifiedBusinessProfessional;
                ServiceDurationVM servicesVm = new ServiceDurationVM();
                servicesVm.ServiceDurations = await _servicesDataService.GetServiceDurations(businessId);
                servicesVm.BusinessId = businessId;
                return View(servicesVm);
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return Content("SORRY, ERROR OCCURED!.");
            }
        }

        private async Task<ProfessionalBusinessDetailDsp?> GetLoggedInProfessionalBusinessDetails(int? businessId)
        {
            List<ProfessionalBusinessDetailDsp> allProfessionalBusinessDetails = await _professionalDataService.GetProfessionalBusinessDetailDsp(null, null);
            ProfessionalBusinessDetailDsp? professionalBusinessDetailDsp = null;
            var emailUsername = User.Identity.Name;
            if (businessId == null)
            {
                //make sure logged in user has permission to the business profile
                professionalBusinessDetailDsp = allProfessionalBusinessDetails.Where(p => p.Email == emailUsername).First();
            }
            else
            {
                professionalBusinessDetailDsp = allProfessionalBusinessDetails.Where(p => p.Email == emailUsername && p.BusinessId == businessId).FirstOrDefault();
            }
            return professionalBusinessDetailDsp;
        }
    }
}
