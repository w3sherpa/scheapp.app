using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.Data;
using scheapp.app.Models.Data.Businesses;
using scheapp.app.Models.Data.Professionals;

namespace scheapp.app.Controllers.Data
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProfessionalsDataController : ControllerBase
    {
        private readonly ILogger<ProfessionalsDataController> _logger;
        private readonly IProfessionalDataService _professionalsDataService;

        public ProfessionalsDataController(
            Logger<ProfessionalsDataController> logger
            , IProfessionalDataService professionalsDataService)
        {
            _logger = logger;
            _professionalsDataService = professionalsDataService;
        }

        [HttpPost]
        public async Task<IActionResult> GetProfessionalsByBusinessId([FromBody] RequestByBusinessId professionalInfoRequest)
        {
            try
            {
                var professionals = ( await _professionalsDataService.GetProfessionals() ).Where(p=>p.BusinessId == professionalInfoRequest.BusinessId);
                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.LogError("{@Exception}", ex);
                return StatusCode(500, "Error Occured.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetProfessionalsByDate([FromBody] RequestByDate businessInfoRQ)
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
        public async Task<IActionResult> GetProfessionalsByServiceId([FromBody] RequestByServiceId businessInfoRQ)
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
        public async Task<IActionResult> GetServicesByProfessionalId([FromBody] RequestByProfessionalId businessInfoRQ)
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
