using Microsoft.AspNetCore.Mvc;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Models.API;
using scheapp.app.Models.Data.TableModels.Services;

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
        [HttpPost]
        public async Task<IActionResult> GetServicesByBusinessId([FromBody] RequestByBusinessId requestByBusinessId)
        {
            try
            {
                var result = await _servicesDataService.GetServices(requestByBusinessId.BusinessId);
                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveService([FromBody] Service rq)
        {
            try
            {
                await _servicesDataService.SaveServices(rq);
                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteService([FromBody] DeleteServiceByBusinessIdRQ rq)
        {
            try
            {
                var result = await _servicesDataService.GetServices(rq.BusinessId);
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
