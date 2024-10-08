using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.Data;

namespace scheapp.app.Controllers.Data
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AdminDataController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IProfessionalDataService _professionalDataService;
        public AdminDataController(
            ILogger<AdminDataController> logger
            , IProfessionalDataService professionalDataService)
        {
            _logger = logger;
            _professionalDataService = professionalDataService;
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
                var scheduledAppoitments = await _professionalDataService.GetProfessionalScheduleAppointmentRequestsDetailsByBusinessId(2);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }

    }

    public class BusinessAppointmentRQ
    {
        public int BusinessId { get; set; }
        [JsonProperty("sortColumn")]
        public string SortColumn { get; set; }
        [JsonProperty("sortOrder")]
        public string SortOrder { get; set; }
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
        [JsonProperty("pageNumber")]
        public int PageNumber { get; set; } 

    }
}
