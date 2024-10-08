using Microsoft.AspNetCore.Mvc;
using scheapp.app.DataServices.Interfaces;
using scheapp.app.Models.Data;

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
