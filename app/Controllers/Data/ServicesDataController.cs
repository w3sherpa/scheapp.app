using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.Data;

namespace scheapp.app.Controllers.Data
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class ServicesDataController : ControllerBase
    {
        private readonly ILogger<ServicesDataController> _logger;
        private readonly IServiceDataService _servicesDataService;

        public ServicesDataController(
            ILogger<ServicesDataController> logger
            , IServiceDataService servicesDataService)
        {
            _logger = logger;
            _servicesDataService = servicesDataService;
        }
        [HttpPost]
        public async Task<IActionResult> GetServicesByserviceId([FromBody] RequestByServiceId requestByServiceId)
        {
            try
            {

                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }
    }
}
