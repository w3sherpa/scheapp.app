using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scheapp.app.Controllers.Data;
using scheapp.app.DataServices.Interfaces;

namespace scheapp.app.Controllers.View
{
    [Route("[controller]/[action]")]
    //[Authorize(Roles = "scheappadmin")]
    public class AdminController : Controller
    {
        private readonly ILogger _logger;
        private readonly IProfessionalDataService _professionalsDataService;

        public AdminController(ILogger<AdminController> logger,IProfessionalDataService professionalsDataService)
        {
            _logger = logger;
            _professionalsDataService = professionalsDataService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GetMessage(string name)
        {
            return Ok($"CommunicationController: Namaste {name}!");
        }
        [HttpPost]
        public async Task<IActionResult> GetProfessionalScheduleAppointmentRequestsDetails(BusinessAppointmentRQ requestByBusinesId)
        {
            try
            {
                var scheduledAppoitments = await _professionalsDataService.GetProfessionalScheduleAppointmentRequestsDetailsByBusinessId(2);
                return Ok(scheduledAppoitments);
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }

    }
}
