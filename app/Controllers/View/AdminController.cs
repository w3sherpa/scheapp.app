using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scheapp.app.Controllers.Data;
using scheapp.app.DataServices.Interfaces;

namespace scheapp.app.Controllers.View
{
    [Route("[controller]/[action]")]
    [Authorize(Roles = "scheapp_admin")]
    public class AdminController : Controller
    {
        private readonly ILogger _logger;
        private readonly IProfessionalDataService _professionalsDataService;
        private readonly IBusinessDataService _businessDataService;
        public AdminController(ILogger<AdminController> logger
            ,IProfessionalDataService professionalsDataService
            , IBusinessDataService businessDataService
            )
        {
            _logger = logger;
            _professionalsDataService = professionalsDataService;

            _businessDataService = businessDataService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Businesses()
        {
            var businesses = _businessDataService.GetBusinesses().Result;
            return View(businesses);
        }

        [HttpGet]
        public IActionResult GetMessage(string name)
        {
            return Ok($"CommunicationController: Namaste {name}!");
        }
        //[HttpPost]
        //public async Task<IActionResult> GetProfessionalScheduleAppointmentRequestsDetails(BusinessAppointmentRQ requestByBusinesId)
        //{
        //    try
        //    {
        //        var scheduledAppoitments = await _professionalsDataService.GetProfessionalScheduleAppointmentRequestsDetailsByBusinessId(2);
        //        return Ok(scheduledAppoitments);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("{@Exception}", ex);
        //        return StatusCode(500, "Error Occured.");
        //    }
        //}

    }
}
